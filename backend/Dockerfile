# Use the official .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Set working directory
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY backend/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY backend/. ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "backend.dll"]
