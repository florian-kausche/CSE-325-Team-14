# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy csproj and restore dependencies
COPY src/StudentProjectPlanner/*.csproj ./src/StudentProjectPlanner/
RUN dotnet restore ./src/StudentProjectPlanner/StudentProjectPlanner.csproj

# Copy everything else and build
COPY src/StudentProjectPlanner/ ./src/StudentProjectPlanner/
WORKDIR /source/src/StudentProjectPlanner
RUN dotnet publish -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy published app
COPY --from=build /app/publish .

# Create directory for SQLite database
RUN mkdir -p /home/data

# Set environment variables
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose port
EXPOSE 5000

# Run the application
ENTRYPOINT ["dotnet", "StudentProjectPlanner.dll"]
