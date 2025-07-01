# Imagen base para el entorno de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Imagen para compilar la app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 👇 Cambios aquí
COPY ["Uttt.Micro.Libro/Uttt.Micro.Libro.csproj", "Uttt.Micro.Libro/"]
WORKDIR /src/Uttt.Micro.Libro
RUN dotnet restore "Uttt.Micro.Libro.csproj"

COPY . .
RUN dotnet publish "Uttt.Micro.Libro.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagen final con los artefactos publicados
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Uttt.Micro.Libro.dll"]
