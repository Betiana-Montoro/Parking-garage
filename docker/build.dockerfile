FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Parking-garage/Parking-garage.csproj", "Parking-garage/"]
COPY ["Parking-garage.Application/Parking-garage.Application.csproj", "Parking-garage.Application/"]
COPY ["Parking-garage.Model/Parking-garage.Model.csproj", "Parking-garage.Model/"]
COPY ["Parking-garage.MsSQL/Parking-garage.MsSQL.csproj", "Parking-garage.MsSQL/"]
RUN dotnet restore "Parking-garage/Parking-garage.csproj"
COPY . .
WORKDIR "/src/Parking-garage"
RUN dotnet build "Parking-garage.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Parking-garage.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Parking-garage.dll"]
