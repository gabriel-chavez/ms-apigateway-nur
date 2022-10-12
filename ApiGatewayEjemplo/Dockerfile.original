#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiGatewayEjemplo/ApiGatewayEjemplo.csproj", "ApiGatewayEjemplo/"]
RUN dotnet restore "ApiGatewayEjemplo/ApiGatewayEjemplo.csproj"
COPY . .
WORKDIR "/src/ApiGatewayEjemplo"
RUN dotnet build "ApiGatewayEjemplo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiGatewayEjemplo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGatewayEjemplo.dll"]