# Använd en officiell .NET SDK-bild som byggmiljö
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Kopiera projekt och restaurera beroenden
COPY *.csproj ./
RUN dotnet restore

# Kopiera övrig kod och bygg applikationen
COPY . ./
RUN dotnet publish -c Release -o out

# Använd en mindre .NET runtime-bild för den slutgiltiga behållaren
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Ange kommandot att köra när behållaren startas
ENTRYPOINT ["dotnet", "SwedishPersonalNumberValidator.dll"]
