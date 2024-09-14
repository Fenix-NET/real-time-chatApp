# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["server/src/ChatApp.Api/ChatApp.Api.csproj", "server/src/ChatApp.Api/"]
COPY ["server/src/ChatApp.Application/ChatApp.Application.csproj", "server/src/ChatApp.Application/"]
COPY ["server/src/ChatApp.Core/ChatApp.Core.csproj", "server/src/ChatApp.Core/"]
COPY ["server/src/ChatApp.Infrastructure/ChatApp.Infrastructure.csproj", "server/src/ChatApp.Infrastructure/"]
RUN dotnet restore "./server/src/ChatApp.Api/ChatApp.Api.csproj"
COPY . .
WORKDIR "/src/server/src/ChatApp.Api"
RUN dotnet build "./ChatApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ChatApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatApp.Api.dll"]