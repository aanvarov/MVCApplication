FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MVCWebApplication/MVCWebApplication.csproj", "MVCWebApplication/"]
RUN dotnet restore "MVCWebApplication/MVCWebApplication.csproj"
COPY . .
WORKDIR "/src/MVCWebApplication"
RUN dotnet build "MVCWebApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MVCWebApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MVCWebApplication.dll"]
