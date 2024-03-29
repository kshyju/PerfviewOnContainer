# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-ltsc2022 AS build

WORKDIR /app

# Copy both "src" and "tools" directories
COPY . .

# Change to the directory containing the .csproj file
WORKDIR /app/src/App

# Build the project
RUN dotnet publish -c Release -o ../../out

WORKDIR /app/out

# Set the entry point of the application
ENTRYPOINT ["dotnet", "App.dll"]