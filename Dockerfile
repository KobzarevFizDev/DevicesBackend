FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY DevicesBackend.sln .
COPY DevicesBackend/DevicesBackend.csproj DevicesBackend/
RUN dotnet restore

COPY DevicesBackend/. DevicesBackend/
WORKDIR /source/DevicesBackend
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
COPY --from=build /source/DevicesBackend/DevicesBackend.http ./
COPY --from=build /source/DevicesBackend/appsettings.json ./
ENTRYPOINT ["dotnet", "DevicesBackend.dll"]