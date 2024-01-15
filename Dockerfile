FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY PersonNummerValidationTool/ .

RUN dotnet restore
RUN dotnet build -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app

COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "PersonNummerValidationTool.dll"]
CMD ["--interactive"]
