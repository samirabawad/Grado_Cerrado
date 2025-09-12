# Imagen base con .NET 8
FROM mcr.microsoft.com/dotnet/sdk:8.0

# Instalar Node.js (elige 20 o 18)
RUN curl -fsSL https://deb.nodesource.com/setup_20.x | bash - \
    && apt-get install -y nodejs

# Instalar cliente de Postgres (psql) y dependencias
RUN apt-get update && apt-get install -y \
    postgresql-client \
    wget curl unzip git \
    && rm -rf /var/lib/apt/lists/*

# Instalar Qdrant client para .NET (NuGet ya lo trae integrado en tu backend)
# Si quieres probar conexión, puedes usar curl contra la API REST de Qdrant