# Använd den officiella .NET SDK-bilden som byggmiljö
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Ange arbetskatalogen
WORKDIR /app

# Kopiera hela projektinnehållet
COPY . ./

# Återställ beroenden och bygg hela projektet utan att köra testerna
RUN dotnet restore
RUN dotnet build
