#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM harbor.restoque.com.br/restoque/dotnet/runtime:8.0-alpine AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VesteTemplateWorker.Worker/VesteTemplateWorker.Worker.csproj", "VesteTemplateWorker.Worker/"]
COPY ["VesteTemplateWorker.Extensions/VesteTemplateWorker.Extensions.csproj", "VesteTemplateWorker.Extensions/"]
COPY ["VesteTemplateWorker.Shared/VesteTemplateWorker.Shared.csproj", "VesteTemplateWorker.Shared/"]
COPY ["VesteTemplateWorker.Tests/VesteTemplateWorker.Tests.csproj", "VesteTemplateWorker.Tests/"]
COPY ["VesteTemplateWorker.Domain/VesteTemplateWorker.Domain.csproj", "VesteTemplateWorker.Domain/"]
COPY ["VesteTemplateWorker.Infra.Data/VesteTemplateWorker.Infra.Data.csproj", "VesteTemplateWorker.Infra.Data/"]
COPY ["VesteTemplateWorker.ApplicationServices/VesteTemplateWorker.ApplicationServices.csproj", "VesteTemplateWorker.ApplicationServices/"]

RUN dotnet restore "./VesteTemplateWorker.Worker/./VesteTemplateWorker.Worker.csproj"

COPY . .
WORKDIR "/src/VesteTemplateWorker.Worker"
RUN dotnet build "./VesteTemplateWorker.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VesteTemplateWorker.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VesteTemplateWorker.Worker.dll"]