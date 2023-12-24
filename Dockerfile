# Use the Microsoft .NET Core SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the entire solution to the container
COPY . ./

# Restore and build the entire solution
RUN dotnet restore "Core.Queuing.sln"
RUN dotnet publish "Core.Queuing.Sample.Crm.Api/Core.Queuing.Sample.Crm.Api.csproj" -c Release -o out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Set the entry point of the application
ENTRYPOINT ["dotnet", "Core.Queuing.Sample.Crm.Api.dll"]