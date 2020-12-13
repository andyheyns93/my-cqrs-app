#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CarCatalog.Api/CarCatalog.Api.csproj", "CarCatalog.Api/"]
COPY ["CarCatalog.Api.Contracts/", "CarCatalog.Api.Contracts/"]
COPY ["CarCatalog.Business/", "CarCatalog.Business/"]
COPY ["CarCatalog.Core/", "CarCatalog.Core/"]
COPY ["CarCatalog.Infrastructure/", "CarCatalog.Infrastructure/"]
RUN dotnet restore "CarCatalog.Api/CarCatalog.Api.csproj"

COPY . .
WORKDIR "/src/CarCatalog.Api"
RUN dotnet build "CarCatalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarCatalog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarCatalog.Api.dll"]