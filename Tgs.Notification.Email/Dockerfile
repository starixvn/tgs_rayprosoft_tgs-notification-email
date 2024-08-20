﻿FROM hub.lumigame.com/base/dotnet/core/sdk:8.0 AS builder

WORKDIR /src
COPY ["nuget.config", "."]
COPY ["Tgs.Notification.Email/Tgs.Notification.Email.csproj", "Tgs.Notification.Email/"]
COPY ["Tgs.Notification.Email.Common/Tgs.Notification.Email.Common.csproj", "Tgs.Notification.Email.Common/"]
COPY ["Tgs.Notification.Email.Core/Tgs.Notification.Email.Core.csproj", "Tgs.Notification.Email.Core/"]
RUN dotnet restore -v n -r linux-x64 "./Tgs.Notification.Email/Tgs.Notification.Email.csproj"

COPY . .
WORKDIR "/src/Tgs.Notification.Email"
RUN dotnet publish --no-restore -r linux-x64 --self-contained false -o /app

FROM hub.lumigame.com/base/dotnet/core/aspnet:8.0-alpine-24h1.1 AS base

RUN adduser --disabled-password --gecos '' dotnetuser \
    && chown -R dotnetuser /tmp

USER dotnetuser

WORKDIR /app

RUN chown -R dotnetuser /app

COPY --from=builder /app .

ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "Tgs.Notification.Email.dll"]