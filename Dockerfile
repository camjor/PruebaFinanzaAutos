# Dockerfile en raíz
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:5000;http://+:8080
ENV DOTNET_RUNNING_IN_CONTAINER=true

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src

# Copia el .csproj desde la carpeta Asisya
COPY ["Asisya/Asisya.csproj", "Asisya/"]
RUN dotnet restore "Asisya/Asisya.csproj"

# Copia todo el proyecto
COPY Asisya/ Asisya/
WORKDIR "/src/Asisya"

RUN dotnet build "Asisya.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Asisya.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Asisya.dll"]