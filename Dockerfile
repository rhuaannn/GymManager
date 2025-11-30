# 1. Estágio de Build (Usa o SDK do .NET 10)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia e Restaura
COPY ["GymManager.csproj", "./"]
RUN dotnet restore "GymManager.csproj"

# Copia tudo e Compila
COPY . .
WORKDIR "/src/."
RUN dotnet build "GymManager.csproj" -c Release -o /app/build

# Publica
FROM build AS publish
RUN dotnet publish "GymManager.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 2. Estágio Final (Usa o Runtime do .NET 10 para rodar leve)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Define a porta padrão (O .NET 8/9/10 usa a porta 8080 por padrão no container)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "GymManager.dll"]