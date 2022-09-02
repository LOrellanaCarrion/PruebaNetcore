#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Cliente.Api/Cliente.Api.csproj", "Cliente.Api/"]
RUN dotnet restore "Cliente.Api/Cliente.Api.csproj"
COPY . .
WORKDIR "/src/Cliente.Api"
RUN dotnet build "Cliente.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cliente.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cliente.Api.dll"]