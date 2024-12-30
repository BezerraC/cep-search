# Usa a imagem oficial do .NET 6 SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Define o diretório de trabalho
WORKDIR /src

# Copia o arquivo de projeto .csproj para o diretório de trabalho correto
COPY CepApi.csproj ./  

# Restaura dependências
RUN dotnet restore "CepApi.csproj"

# Copia o restante dos arquivos e compila o projeto
COPY . .  
WORKDIR "/src"
RUN dotnet build "CepApi.csproj" -c Release -o /app/build

# Publica o projeto para a pasta /app
RUN dotnet publish "CepApi.csproj" -c Release -o /app/publish

# Imagem final com a aplicação publicada
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Define a porta que o aplicativo vai escutar
EXPOSE 80

# Comando para iniciar o aplicativo
ENTRYPOINT ["dotnet", "CepApi.dll"]
