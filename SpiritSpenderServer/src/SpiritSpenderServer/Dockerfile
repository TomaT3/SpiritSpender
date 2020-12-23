FROM mcr.microsoft.com/dotnet/aspnet:5.0.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . .
WORKDIR "/src/SpiritSpenderServer"
RUN dotnet publish "SpiritSpenderServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SpiritSpenderServer.dll"]