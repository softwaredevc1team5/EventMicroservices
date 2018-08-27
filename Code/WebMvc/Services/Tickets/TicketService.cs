using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using WebMvc.Infrastructure;
using WebMvc.Models.Ticket;

namespace WebMvc.Services.Tickets
{
    public class TicketService : ITicketService
    {

        private readonly IOptionsSnapshot<AppSettings> _settings;
        private readonly IHttpClient _apiClient;
        private readonly string _remoteServiceBaseUrl;

        public TicketService(IOptionsSnapshot<AppSettings> settings, IHttpClient httpClient) {
            _settings = settings;
            _apiClient = httpClient;
            _remoteServiceBaseUrl = $"{_settings.Value.EventTicketUrl}/api/ticket/";
        }

        public async Task<IEnumerable<SelectListItem>> GetAllTicketTypes()
        {
            var items = new List<SelectListItem>();
            var eventTicketUri = ApiPaths.EventTicket.GetAllTicketTypes(_remoteServiceBaseUrl);
            var dataString = await _apiClient.GetStringAsync(eventTicketUri);
            var ticketTypes = JArray.Parse(dataString);
            foreach (var ticketType in ticketTypes.Children<JObject>())
            {
                items.Add(new SelectListItem()
                {
                    Value = ticketType.Value<string>("id"),
                    Text = ticketType.Value<string>("name")
                });
            }
            return items;

        }

        public Task<Ticket> GetTicketById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SelectListItem>> GetTicketsByEventId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SelectListItem>> GetTicketsByEventTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}
