FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore FleetManagement.csproj
RUN dotnet build FleetManagement.csproj -c Release --no-restore
RUN dotnet publish FleetManagement.csproj -c Release --no-build -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "FleetManagement.dll"]
