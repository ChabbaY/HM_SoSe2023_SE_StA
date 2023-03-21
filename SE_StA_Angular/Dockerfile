#build
FROM node:latest AS build
WORKDIR /usr/local/app
COPY ./ /usr/local/app/
RUN npm install
RUN npm run build

#run
FROM nginx:latest
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /usr/local/app/dist/se-sta-angular /usr/share/nginx/html
EXPOSE 80
