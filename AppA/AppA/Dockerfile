FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AppA/AppA.csproj", "AppA/"]
RUN dotnet restore "AppA/AppA.csproj"
COPY . .
WORKDIR "/src/AppA"
RUN dotnet build "AppA.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AppA.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppA.dll"]
