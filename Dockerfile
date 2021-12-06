FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY . ./
RUN dotnet build "./Christmas.Secret.Gifter.API/Christmas.Secret.Gifter.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Christmas.Secret.Gifter.API/Christmas.Secret.Gifter.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Christmas.Secret.Gifter.API.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Christmas.Secret.Gifter.API.dll