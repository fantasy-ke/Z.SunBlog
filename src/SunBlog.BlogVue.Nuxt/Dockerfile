# 使用Node.js 基础镜像
FROM node:18-alpine as build
WORKDIR /src

COPY . .
#设置镜像源
RUN  yarn config set registry https://registry.npmmirror.com && yarn && yarn run build

FROM node:18-alpine as final
WORKDIR /app
EXPOSE 80
COPY --from=build /src/.output .
CMD ["node","./server/index.mjs"]
