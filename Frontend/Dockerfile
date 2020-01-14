FROM node:12 AS build

WORKDIR /build

COPY ./Frontend ./

RUN npm ci

RUN npm run build

FROM nginx AS run

COPY ./Frontend/nginx.conf /etc/nginx/nginx.conf

COPY --from=build /build/public /usr/share/nginx/html/