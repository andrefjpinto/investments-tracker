FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["investments-tracker/investments-tracker.csproj", "investments-tracker/"]
RUN dotnet restore "investments-tracker/investments-tracker.csproj"
COPY . .
WORKDIR "/src/investments-tracker"
RUN dotnet build "investments-tracker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "investments-tracker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "investments-tracker.dll"]
