#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["Tgs.Notification.Email/Tgs.Notification.Email.csproj", "Tgs.Notification.Email/"]
COPY ["Tgs.Notification.Email.Common/Tgs.Notification.Email.Common.csproj", "Tgs.Notification.Email.Common/"]
COPY ["Tgs.Notification.Email.Core/Tgs.Notification.Email.Core.csproj", "Tgs.Notification.Email.Core/"]
RUN dotnet restore "./Tgs.Notification.Email/./Tgs.Notification.Email.csproj"
COPY . .
WORKDIR "/src/Tgs.Notification.Email"
RUN dotnet build "./Tgs.Notification.Email.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Tgs.Notification.Email.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tgs.Notification.Email.dll"]