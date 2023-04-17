#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["DeliVeggie.GateWay/DeliVeggie.GateWay.csproj", "DeliVeggie.GateWay/"]
COPY ["DeliVeggie.Shared/DeliVeggie.Shared.csproj", "DeliVeggie.Shared/"]
RUN dotnet restore "DeliVeggie.GateWay/DeliVeggie.GateWay.csproj"
COPY . .
WORKDIR "/src/DeliVeggie.GateWay"
RUN dotnet build "DeliVeggie.GateWay.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliVeggie.GateWay.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliVeggie.GateWay.dll"]
