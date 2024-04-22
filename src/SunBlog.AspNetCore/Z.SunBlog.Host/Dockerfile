FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN apt-get update && apt-get install -y libgdiplus
WORKDIR /app
# 暴露80端口
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Z.SunBlog.Host/Z.SunBlog.Host.csproj", "Z.SunBlog.Host/"]
COPY ["src/Z.Foundation.Core/Z.Foundation.Core.csproj", "src/Z.Foundation.Core/"]
COPY ["src/Z.DynamicProxy/Z.DynamicProxy.csproj", "src/Z.DynamicProxy/"]
COPY ["src/efcore/Z.EntityFrameworkCore.SqlServer/Z.EntityFrameworkCore.SqlServer.csproj", "src/efcore/Z.EntityFrameworkCore.SqlServer/"]
COPY ["src/efcore/Z.EntityFrameworkCore/Z.EntityFrameworkCore.csproj", "src/efcore/Z.EntityFrameworkCore/"]
COPY ["src/Z.Fantasy.Core/Z.Fantasy.Core.csproj", "src/Z.Fantasy.Core/"]
COPY ["src/Z.FreeRedis/Z.FreeRedis.csproj", "src/Z.FreeRedis/"]
COPY ["src/Z.HangFire/Z.HangFire.csproj", "src/Z.HangFire/"]
COPY ["src/efcore/Z.EntityFrameworkCore.Mysql/Z.EntityFrameworkCore.Mysql.csproj", "src/efcore/Z.EntityFrameworkCore.Mysql/"]
COPY ["Z.SunBlog.Application/Z.SunBlog.Application.csproj", "Z.SunBlog.Application/"]
COPY ["src/Z.Fantasy.Application/Z.Fantasy.Application.csproj", "src/Z.Fantasy.Application/"]
COPY ["Z.SunBlog.Core/Z.SunBlog.Core.csproj", "Z.SunBlog.Core/"]
COPY ["Z.SunBlog.EntityFrameworkCore/Z.SunBlog.EntityFrameworkCore.csproj", "Z.SunBlog.EntityFrameworkCore/"]

RUN dotnet restore "Z.SunBlog.Host/Z.SunBlog.Host.csproj"
COPY . .
WORKDIR "/src/Z.SunBlog.Host"

RUN dotnet build "Z.SunBlog.Host.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "Z.SunBlog.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#安装fontconfig 验证码图片使用
RUN apt-get clean
RUN apt-get update && apt-get install -y fontconfig

#定义时区参数
ENV TZ=Asia/Shanghai
#设置时区
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo '$TZ' > /etc/timezone

ENTRYPOINT ["dotnet", "Z.SunBlog.Host.dll"]