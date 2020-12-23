FROM mcr.microsoft.com/dotnet/runtime:5.0.1-buster-slim-arm64v8 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY . .
WORKDIR "/src"
RUN dotnet publish "SpiritSpenderServer/SpiritSpenderServer.csproj" -c Release /p:Version=$versionString -r linux-arm -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SpiritSpenderServer.dll"]