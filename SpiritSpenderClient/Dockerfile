FROM node:13.12.0 AS build
WORKDIR /usr/src/app
COPY package.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:1.17.1-alpine AS prod
COPY --from=build /usr/src/app/dist/SpiritSpenderClient /usr/share/nginx/html
