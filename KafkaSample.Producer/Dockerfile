#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["KafkaSample.Producer/KafkaSample.Producer.csproj", "KafkaSample.Producer/"]
RUN dotnet restore "KafkaSample.Producer/KafkaSample.Producer.csproj"
COPY . .
WORKDIR "/src/KafkaSample.Producer"
RUN dotnet build "KafkaSample.Producer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KafkaSample.Producer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KafkaSample.Producer.dll"]