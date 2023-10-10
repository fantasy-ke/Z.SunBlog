#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /
COPY ["Z.NetWiki.Host/Z.NetWiki.Host.csproj", "Z.NetWiki.Host/"]
COPY ["Z.NetWiki.Application/Z.NetWiki.Application.csproj", "Z.NetWiki.Application/"]
COPY ["src/Z.Ddd.Application/Z.Ddd.Application.csproj", "src/Z.Ddd.Application/"]
COPY ["src/Z.Ddd.Common/Z.Ddd.Common.csproj", "src/Z.Ddd.Common/"]
COPY ["src/Z.Module/Z.Module.csproj", "src/Z.Module/"]
COPY ["Z.NetWiki.Domain/Z.NetWiki.Domain.csproj", "Z.NetWiki.Domain/"]
COPY ["Z.NetWiki.Common/Z.NetWiki.Common.csproj", "Z.NetWiki.Common/"]
COPY ["src/efcore/Z.EntityFrameworkCore.SqlServer/Z.EntityFrameworkCore.SqlServer.csproj", "src/efcore/Z.EntityFrameworkCore.SqlServer/"]
COPY ["src/efcore/Z.EntityFrameworkCore/Z.EntityFrameworkCore.csproj", "src/efcore/Z.EntityFrameworkCore/"]
COPY ["Z.NetWiki.EntityFrameworkCore/Z.NetWiki.EntityFrameworkCore.csproj", "Z.NetWiki.EntityFrameworkCore/"]
RUN dotnet restore "Z.NetWiki.Host/Z.NetWiki.Host.csproj"
COPY . .
WORKDIR "/src/Z.NetWiki.Host"
RUN dotnet build "Z.NetWiki.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Z.NetWiki.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Z.NetWiki.Host.dll"]