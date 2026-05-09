# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project file and restore dependencies (cached layer)
COPY CoblentzContext.csproj .
RUN dotnet restore

# Copy everything else and publish
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

# Set environment to Production
ENV ASPNETCORE_ENVIRONMENT=Production

# Render sets PORT dynamically; expose a default for documentation
EXPOSE 10000

# Start the application
ENTRYPOINT ["dotnet", "CoblentzContext.dll"]
