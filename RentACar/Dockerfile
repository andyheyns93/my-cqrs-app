#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CarCatalog.Service/CarCatalog.Api/CarCatalog.Api.csproj", "CarCatalog.Service/CarCatalog.Api/"]
COPY ["CarCatalog.Service/CarCatalog.Api.Contracts/", "CarCatalog.Service/CarCatalog.Api.Contracts/"]
COPY ["CarCatalog.Service/CarCatalog.Business/", "CarCatalog.Service/CarCatalog.Business/"]
COPY ["CarCatalog.Service/CarCatalog.Core/", "CarCatalog.Service/CarCatalog.Core/"]
COPY ["CarCatalog.Service/CarCatalog.Infrastructure/", "CarCatalog.Service/CarCatalog.Infrastructure/"]
RUN dotnet restore "CarCatalog.Service/CarCatalog.Api/CarCatalog.Api.csproj"

COPY . .
WORKDIR "/src/CarCatalog.Service/CarCatalog.Api"
RUN dotnet build "CarCatalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarCatalog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarCatalog.Api.dll"]