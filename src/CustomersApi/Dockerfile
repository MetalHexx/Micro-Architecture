#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/CustomersApi/CustomersApi.csproj", "src/CustomersApi/"]
RUN dotnet restore "src/CustomersApi/CustomersApi.csproj"
COPY . .
WORKDIR "/src/src/CustomersApi"
RUN dotnet build "CustomersApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomersApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomersApi.dll"]