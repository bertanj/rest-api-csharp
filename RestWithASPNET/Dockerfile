# Estágio 1: Base - Para a imagem final, mais leve
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
# EXPOSE 8081 # Se você também usa HTTPS dentro do container

# Estágio 2: Build - Para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia o arquivo .csproj e restaura as dependências primeiro.
# Isso aproveita o cache do Docker. Se as dependências não mudarem,
# esta camada não será executada novamente.
COPY ["RestWithASPNET.csproj", "."]
RUN dotnet restore "./RestWithASPNET.csproj"

# Copia todo o resto do código-fonte
COPY . .
WORKDIR "/src"
RUN dotnet build "RestWithASPNET.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Estágio 3: Publish - Cria a versão de publicação da aplicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RestWithASPNET.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Estágio 4: Final - Monta a imagem final a partir da base
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestWithASPNET.dll"]