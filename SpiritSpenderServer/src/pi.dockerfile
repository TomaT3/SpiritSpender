FROM mcr.microsoft.com/dotnet/aspnet:7.0.2-bullseye-slim-arm64v8 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG versionString
WORKDIR /src
COPY . .
WORKDIR "/src"
RUN dotnet publish "SpiritSpenderServer/SpiritSpenderServer.csproj" -c Release /p:Version=${versionString} -r linux-arm64 -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SpiritSpenderServer.dll"]
