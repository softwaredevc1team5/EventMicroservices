FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ../EventTicketAPI/EventTicketAPI.csproj ../EventTicketAPI/
RUN dotnet restore ../EventTicketAPI/EventTicketAPI.csproj
COPY . .
WORKDIR /src/../EventTicketAPI
RUN dotnet build EventTicketAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish EventTicketAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EventTicketAPI.dll"]
