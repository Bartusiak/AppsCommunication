FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ApplicationB/ApplicationB.csproj", "ApplicationB/"]
RUN dotnet restore "ApplicationB/ApplicationB.csproj"
COPY . .
WORKDIR "/src/ApplicationB"
FROM mcr.microsoft.com/dotnet/core/sdk:3.1
RUN dotnet tool install -g dotnet-ef
ENTRYPOINT ["dotnet-ef"]
RUN dotnet ef migrations InitialCreate
RUN dotnet ef database update
RUN dotnet build "ApplicationB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApplicationB.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApplicationB.dll"]
