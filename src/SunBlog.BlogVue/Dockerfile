FROM node:16.20.2-alpine as builder

# 设置项目目录
WORKDIR /app

COPY package*.json ./
RUN echo "COPY is success"

RUN yarn
RUN echo "install is success"

RUN ls -la

COPY . .

# 编译项目
RUN yarn run build
RUN echo "build is success"

FROM nginx

RUN ls -la 

# 复制 nginx.conf 配置文件到镜像中
COPY ["./_nginx/default.conf", "/etc/nginx/nginx.conf"]


# 从编译镜像复制编译结果到此镜像
COPY --from=builder /app/dist /usr/share/nginx/html
