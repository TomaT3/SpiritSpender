FROM node:18.12.1 AS build
WORKDIR /usr/src/app
COPY package.json ./
RUN npm install
COPY . .
RUN npm run build:prod

FROM arm64v8/nginx:1.18-alpine AS prod
COPY --from=build /usr/src/app/dist/SpiritSpenderClient /usr/share/nginx/html
