FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 58150
EXPOSE 44384

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY TokenServiceApi/TokenServiceApi.csproj TokenServiceApi/
RUN dotnet restore TokenServiceApi/TokenServiceApi.csproj
COPY . .
WORKDIR /src/TokenServiceApi
RUN dotnet build TokenServiceApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TokenServiceApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TokenServiceApi.dll"]
