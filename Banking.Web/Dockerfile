FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Banking.Web/Banking.Web.csproj", "Banking.Web/"]
RUN dotnet restore "Banking.Web/Banking.Web.csproj"
COPY . .
WORKDIR "/src/Banking.Web"
RUN dotnet build "Banking.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Banking.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Banking.Web.dll"]
