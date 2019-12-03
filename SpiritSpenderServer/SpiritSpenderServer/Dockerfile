FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["SpiritSpenderServer/SpiritSpenderServer.csproj", "SpiritSpenderServer/"]
RUN dotnet restore "SpiritSpenderServer/SpiritSpenderServer.csproj"
COPY . .
WORKDIR "/src/SpiritSpenderServer"
RUN dotnet build "SpiritSpenderServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpiritSpenderServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpiritSpenderServer.dll"]