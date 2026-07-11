FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["IdentityReportService.csproj", "./"]
RUN dotnet restore "IdentityReportService.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "IdentityReportService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityReportService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityReportService.dll"]
