# Etapa base: Runtime para producción y desarrollo
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar y restaurar dependencias (todas las bibliotecas)
COPY ["src/Hotel.API/Hotel.API.csproj", "Hotel.API/"]
COPY ["src/Aplicacion/Aplicacion.csproj", "Aplicacion/"]
COPY ["src/Infraestructura/Infraestructura.csproj", "Infraestructura/"]
COPY ["src/Dominio/Dominio.csproj", "Dominio/"]

RUN dotnet restore "Hotel.API/Hotel.API.csproj"


# Copiar el resto del código fuente y compilar
COPY ./src .
WORKDIR "/src/Hotel.API"
RUN dotnet build "Hotel.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Hotel.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final: ejecutando la aplicación
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hotel.API.dll"]