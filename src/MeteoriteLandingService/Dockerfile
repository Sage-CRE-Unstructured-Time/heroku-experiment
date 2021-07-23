FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["MeteoriteLandingService.csproj", ""]
RUN dotnet restore "MeteoriteLandingService.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "MeteoriteLandingService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeteoriteLandingService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeteoriteLandingService.dll"]
