FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

COPY src/. ./
WORKDIR /app/qsLog.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0  AS runtime
WORKDIR /app
COPY --from=build /app/qsLog.Api/out ./
ENTRYPOINT ["dotnet", "qsLog.Api.dll"]