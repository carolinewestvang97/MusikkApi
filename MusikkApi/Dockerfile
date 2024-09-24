# Use ASP.NET Core runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
 
# Use SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MusikkApi.csproj", "./"]
RUN dotnet restore "./MusikkApi.csproj"
 
# Copy the rest of the source code and build
COPY . .
RUN dotnet build "./MusikkApi.csproj" -c Release -o /app/build
 
# Publish the application
FROM build AS publish
RUN dotnet publish "./MusikkApi.csproj" -c Release -o /app/publish /p:UseAppHost=false
 
# Final stage: Use runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusikkApi.dll"]