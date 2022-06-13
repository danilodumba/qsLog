FROM node:alpine AS my-app-build
WORKDIR /app
#COPY qslog-front/. .
#COPY . .
RUN npm install --force
ARG env=prod
RUN npm run build --configuration production

FROM nginx:alpine
COPY --from=my-app-build /app/dist/qslog /usr/share/nginx/html