# Stage 1: Base runtime setup
FROM ://microsoft.com AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: Compilation environment
FROM ://microsoft.com AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApp5BySujit/WebApp5BySujit.csproj", "WebApp5BySujit/"]
RUN dotnet restore "./WebApp5BySujit/WebApp5BySujit.csproj"
COPY . .
WORKDIR "/src/WebApp5BySujit"
RUN dotnet build "./WebApp5BySujit.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApp5BySujit.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Execution assembly
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp5BySujit.dll"]
