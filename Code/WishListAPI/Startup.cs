using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MassTransit;
using MassTransit.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Swagger;
using WishListApi.Messaging.Consumers;
using WishListAPI;
using WishListAPI.Infrastructure.Filters;
using WishListAPI.Model;

namespace WishListAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.    
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore(
                options => options.Filters.Add(typeof(HttpGlobalExceptionFilter))
                )
                .AddJsonFormatters()
                .AddApiExplorer();
            services.Configure<WishlistSetting>(Configuration);
            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<WishlistSetting>>().Value;
                var configuration = ConfigurationOptions.Parse(settings.ConnectionString, true);

                configuration.ResolveDns = true;
                configuration.AbortOnConnectFail = false;
                //connection to redis
                return ConnectionMultiplexer.Connect(configuration);
            });
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Basket HTTP API",
                    Version = "v1",
                    Description = "The Basket Service HTTP API",
                    TermsOfService = "Terms Of Service"
                });


            });



            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    poll => poll.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IWishlistRepository, RedisWishlistRepository>();
            // services.AddTransient<IIdentityService, IdentityService>();
            var builder = new ContainerBuilder();

            // register a specific consumer
            builder.RegisterType<OrderCompletedEventConsumer>();

            builder.Register(context =>
            {
                var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
                {


                    var host = cfg.Host(new Uri("rabbitmq://rabbitmq/"), "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });


                    // https://stackoverflow.com/questions/39573721/disable-round-robin-pattern-and-use-fanout-on-masstransit
                    cfg.ReceiveEndpoint(host, "EventMicroservices" + Guid.NewGuid().ToString(), e =>
                    {
                        
                        e.LoadFrom(context);

                    });
                });

                return busControl;
            })
                .SingleInstance()
                .As<IBusControl>()
                .As<IBus>();

            builder.Populate(services);
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }

            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseMvcWithDefaultRoute();

            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "Basket.API V1");
                   //c.ConfigureOAuth2("basketswaggerui", "", "", "Basket Swagger UI");
               });
            var bus = ApplicationContainer.Resolve<IBusControl>();
            var busHandle = TaskUtil.Await(() => bus.StartAsync());
            lifetime.ApplicationStopping.Register(() => busHandle.Stop());
        }
    }
}
