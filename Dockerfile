#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#WORKDIR /app
ENV ASPNETCORE_URLS=http://+:9001
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["weatherapp.csproj", ""]
RUN dotnet restore "./weatherapp.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "weatherapp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "weatherapp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "weatherapp.dll"]