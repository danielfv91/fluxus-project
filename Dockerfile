
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Fluxus.WebApi/Fluxus.WebApi.csproj", "src/Fluxus.WebApi/"]
COPY ["src/Fluxus.Application/Fluxus.Application.csproj", "src/Fluxus.Application/"]
COPY ["src/Fluxus.Common/Fluxus.Common.csproj", "src/Fluxus.Common/"]
COPY ["src/Fluxus.Domain/Fluxus.Domain.csproj", "src/Fluxus.Domain/"]
COPY ["src/Fluxus.IoC/Fluxus.IoC.csproj", "src/Fluxus.IoC/"]
COPY ["src/Fluxus.ORM/Fluxus.ORM.csproj", "src/Fluxus.ORM/"]
RUN dotnet restore "./src/Fluxus.WebApi/Fluxus.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Fluxus.WebApi"
RUN dotnet build "./Fluxus.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Fluxus.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fluxus.WebApi.dll"]
