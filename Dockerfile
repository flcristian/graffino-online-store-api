FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["graffino-online-store-api/graffino-online-store-api.csproj", "graffino-online-store-api/"]
RUN dotnet restore "graffino-online-store-api/graffino-online-store-api.csproj" --verbosity detailed
COPY . .
WORKDIR "/src/graffino-online-store-api"
RUN dotnet build "graffino-online-store-api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "graffino-online-store-api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "graffino-online-store-api.dll"]