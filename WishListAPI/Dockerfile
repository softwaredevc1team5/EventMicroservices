FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ../WishListAPI/WishListAPI.csproj ../WishListAPI/
RUN dotnet restore ../WishListAPI/WishListAPI.csproj
COPY . .
WORKDIR /src/../WishListAPI
RUN dotnet build WishListAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WishListAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WishListAPI.dll"]
