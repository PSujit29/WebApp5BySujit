FROM ://microsoft.com AS base
WORKDIR /app
EXPOSE 8080

FROM ://microsoft.com AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Notice the folder prefix here so Docker finds it from the parent directory
COPY ["WebApp5BySujit/WebApp5BySujit.csproj", "WebApp5BySujit/"]
RUN dotnet restore "./WebApp5BySujit/WebApp5BySujit.csproj"
COPY . .
WORKDIR "/src/WebApp5BySujit"
RUN dotnet build "./WebApp5BySujit.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApp5BySujit.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp5BySujit.dll"]
