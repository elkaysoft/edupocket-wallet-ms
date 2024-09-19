# Use the official .NET image as the base image for building the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Edupocket.Api/Edupocket.Api.csproj", "Edupocket.Api/"]
COPY ["Edupocket.Application/Edupocket.Application.csproj", "Edupocket.Application/"]
COPY ["Edupocket.Domain/Edupocket.Domain.csproj", "Edupocket.Domain/"]
COPY ["Edupocket.DAL/Edupocket.DAL.csproj", "Edupocket.DAL/"] 
COPY ["Edupocket.Infrastructure/Edupocket.Infrastructure.csproj", "Edupocket.Infrastructure/"] 
RUN dotnet restore "Edupocket.Api/Edupocket.Api.csproj"

# Copy the rest of the application code and build it
COPY . .
WORKDIR "/src/Edupocket.Api"
RUN dotnet build "Edupocket.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the app
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Edupocket.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Final stage: build a runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Edupocket.Api.dll" ]
