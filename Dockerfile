# Första steg: Bygga huvudapplikationen
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Kopiera huvudapplikationskoden
COPY PersonNummerValidationTool/ .

# Återställ beroenden och bygg projektet
RUN dotnet restore
RUN dotnet build -c Release -o /app/out

# Tredje steg: Kör huvudapplikationen
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app

# Kopiera resultatet från det andra steget
COPY --from=build-env /app/out .

# Ange entrypoint
CMD ["dotnet", "PersonNummerValidationTool.dll"]
