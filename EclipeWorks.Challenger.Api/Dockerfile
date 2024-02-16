#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5005
ENV ASPNETCORE_URLS=http://+:5005
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EclipeWorks.Challenger.Api/EclipeWorks.Challenger.Api.csproj", "EclipeWorks.Challenger.Api/"]
COPY ["EclipseWorks.Challenger.Application/EclipseWorks.Challenger.Application.csproj", "EclipseWorks.Challenger.Application/"]
COPY ["EclipseWorks.Challenger.Domain/EclipseWorks.Challenger.Domain.csproj", "EclipseWorks.Challenger.Domain/"]
COPY ["EclipseWorks.Challenger.InfraStructure/EclipseWorks.Challenger.InfraStructure.csproj", "EclipseWorks.Challenger.InfraStructure/"]
RUN dotnet restore "./EclipeWorks.Challenger.Api/./EclipeWorks.Challenger.Api.csproj"
COPY . .
WORKDIR "/src/EclipeWorks.Challenger.Api"
RUN dotnet build "./EclipeWorks.Challenger.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EclipeWorks.Challenger.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EclipeWorks.Challenger.Api.dll"]

