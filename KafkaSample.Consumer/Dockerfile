#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["KafkaSample.Consumer/KafkaSample.Consumer.csproj", "KafkaSample.Consumer/"]
RUN dotnet restore "KafkaSample.Consumer/KafkaSample.Consumer.csproj"
COPY . .
WORKDIR "/src/KafkaSample.Consumer"
RUN dotnet build "KafkaSample.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KafkaSample.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KafkaSample.Consumer.dll"]