# 1️⃣ Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# csproj kopyala ve restore
COPY *.csproj ./
RUN dotnet restore

# tüm kodu kopyala ve publish et
COPY . .
RUN dotnet publish -c Release -o out

# 2️⃣ Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# publish çıktısını al
COPY --from=build /app/out .

# ASP.NET Core production default port
EXPOSE 80

ENTRYPOINT ["dotnet", "NetCoreAPI.dll"]
