#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TemplateWorker.Worker/TemplateWorker.Worker.csproj", "TemplateWorker.Worker/"]
COPY ["TemplateWorker.Extensions/TemplateWorker.Extensions.csproj", "TemplateWorker.Extensions/"]
COPY ["TemplateWorker.Shared/TemplateWorker.Shared.csproj", "TemplateWorker.Shared/"]
COPY ["TemplateWorker.Tests/TemplateWorker.Tests.csproj", "TemplateWorker.Tests/"]
COPY ["TemplateWorker.Domain/TemplateWorker.Domain.csproj", "TemplateWorker.Domain/"]
COPY ["TemplateWorker.Infra.Data/TemplateWorker.Infra.Data.csproj", "TemplateWorker.Infra.Data/"]
COPY ["TemplateWorker.ApplicationServices/TemplateWorker.ApplicationServices.csproj", "TemplateWorker.ApplicationServices/"]

RUN dotnet restore "./TemplateWorker.Worker/./TemplateWorker.Worker.csproj"

COPY . .
WORKDIR "/src/TemplateWorker.Worker"
RUN dotnet build "./TemplateWorker.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TemplateWorker.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TemplateWorker.Worker.dll"]
