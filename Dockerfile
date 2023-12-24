# Use the official Microsoft .NET Core SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the CSPROJ file and restore any dependencies (via NUGET)
COPY ["Core.Queuing.Sample.Crm.Api.csproj", "./"]
RUN dotnet restore

# Copy the project files and build the release
COPY . ./
RUN dotnet publish "Core.Queuing.Sample.Crm.Api.csproj" -c Release -o out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Set the entry point of the application
ENTRYPOINT ["dotnet", "Core.Queuing.Sample.Crm.Api.dll"]