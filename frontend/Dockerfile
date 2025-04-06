FROM node:21-alpine as build

ARG API_URL

WORKDIR /app

COPY ./package*.json ./
RUN npm install
RUN npm install -g cross-env

COPY . .

RUN cross-env VITE_API_URL=${API_URL} npm run build

FROM nginx:latest
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]