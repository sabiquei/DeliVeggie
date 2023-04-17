#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["DeliVeggie.MicroService/DeliVeggie.MicroService.csproj", "DeliVeggie.MicroService/"]
COPY ["DeliVeggie.Shared/DeliVeggie.Shared.csproj", "DeliVeggie.Shared/"]
COPY ["DeliVeggie.Persistance.MongoDb/DeliVeggie.Persistance.MongoDb.csproj", "DeliVeggie.Persistance.MongoDb/"]
RUN dotnet restore "DeliVeggie.MicroService/DeliVeggie.MicroService.csproj"
COPY . .
WORKDIR "/src/DeliVeggie.MicroService"
RUN dotnet build "DeliVeggie.MicroService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliVeggie.MicroService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliVeggie.MicroService.dll"]
