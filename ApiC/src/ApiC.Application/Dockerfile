#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/ApiC.Application/ApiC.Application.csproj", "src/ApiC.Application/"]
COPY ["src/ApiC.Domain/ApiC.Domain.csproj", "src/ApiC.Domain/"]
COPY ["src/ApiC.Service/ApiC.Service.csproj", "src/ApiC.Service/"]
COPY ["src/ApiC.Data/ApiC.Data.csproj", "src/ApiC.Data/"]
COPY ["src/ApiC.CrossCutting/ApiC.CrossCutting.csproj", "src/ApiC.CrossCutting/"]
RUN dotnet restore "src/ApiC.Application/ApiC.Application.csproj"
COPY . .
WORKDIR "/src/src/ApiC.Application"
RUN dotnet build "ApiC.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiC.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiC.Application.dll"]
