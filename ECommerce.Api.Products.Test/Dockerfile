#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ECommerce.Api.Products.Test/ECommerce.Api.Products.Test.csproj", "ECommerce.Api.Products.Test/"]
COPY ["ECommerce.Api.Products/ECommerce.Api.Products.csproj", "ECommerce.Api.Products/"]
RUN dotnet restore "./ECommerce.Api.Products.Test/ECommerce.Api.Products.Test.csproj"
COPY . .
WORKDIR "/src/ECommerce.Api.Products.Test"
RUN dotnet build "./ECommerce.Api.Products.Test.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ECommerce.Api.Products.Test.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Api.Products.Test.dll"]