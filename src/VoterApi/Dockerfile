# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Restore first so this layer caches when only source changes.
COPY VoterApi.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish

# migrations, with no SDK, source clone, or extra tooling needed at deploy time.
RUN dotnet tool install --global dotnet-ef --version 10.* \
 && export PATH="$PATH:/root/.dotnet/tools" \
 && dotnet ef migrations bundle -c VoterDbContext --configuration Release \
      --self-contained -r linux-x64 -o /app/publish/efbundle --force

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

# The .NET runtime image ships a predefined non-root user named "app".
USER app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "VoterApi.dll"]
