FROM node:13.12.0 AS build
WORKDIR /usr/src/app
COPY package.json ./
RUN npm install
COPY . .
RUN ls -al src/environments
RUN cat src/environments/environment.prod.ts
RUN npm run build:prod

FROM arm64v8/nginx:1.18-alpine AS prod
COPY --from=build /usr/src/app/dist/SpiritSpenderClient /usr/share/nginx/html
