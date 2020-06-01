FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim-arm32v7 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
#COPY ["SpiritSpenderServer/SpiritSpenderServer.csproj", "SpiritSpenderServer/"]
#RUN dotnet restore "SpiritSpenderServer/SpiritSpenderServer.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "SpiritSpenderServer.csproj" -c Release -r linux-arm -o /app/build

FROM build AS publish
RUN dotnet publish "SpiritSpenderServer.csproj" -c Release -r linux-arm -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpiritSpenderServer.dll"]