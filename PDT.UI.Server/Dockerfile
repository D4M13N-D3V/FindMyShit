FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PDT.UI.Server/PDT.UI.Server.csproj", "PDT.UI.Server/"]
RUN dotnet restore "PDT.UI.Server/PDT.UI.Server.csproj"
COPY . .
WORKDIR "/src/PDT.UI.Server"
RUN dotnet build "PDT.UI.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PDT.UI.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PDT.UI.Server.dll"]
