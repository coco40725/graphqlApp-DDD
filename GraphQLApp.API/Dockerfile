FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GraphQLApp_Item1/GraphQLApp_Item1.csproj", "GraphQLApp_Item1/"]
RUN dotnet restore "GraphQLApp_Item1/GraphQLApp_Item1.csproj"
COPY . .
WORKDIR "/src/GraphQLApp_Item1"
RUN dotnet build "GraphQLApp_Item1.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GraphQLApp_Item1.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GraphQLApp_Item1.dll"]
