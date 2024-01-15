# Använd den officiella .NET SDK-bilden som byggmiljö
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Ange arbetskatalogen
WORKDIR /app

# Kopiera hela projektinnehållet
COPY . ./

# Återställ beroenden och bygg hela projektet
RUN dotnet restore
RUN dotnet build

# Kör enhetstester
RUN dotnet test

# Ange hur din applikation ska startas
# Om du vill använda CMD:
# CMD ["dotnet", "run"]

# Om du vill använda ENTRYPOINT (rekommenderat för produktionsmiljöer):
ENTRYPOINT ["dotnet", "SwedishPersonalNumberValidationTool.dll"]
