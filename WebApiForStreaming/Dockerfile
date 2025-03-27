# Этап 1: Сборка приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем csproj и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем остальной код и собираем приложение
COPY . ./
RUN dotnet publish -c Release -o out

# Этап 2: Создание runtime-образа
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Указываем порт, который будет использоваться
EXPOSE 8080

# Запускаем приложение
ENTRYPOINT ["dotnet", "WebApiForStreaming.dll"]