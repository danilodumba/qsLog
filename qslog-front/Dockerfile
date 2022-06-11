FROM node:alpine AS my-app-build
WORKDIR /app
COPY qslog-front/. .

RUN npm ci --force
ARG env=prod
RUN npm run build -- --prod

FROM nginx:alpine
COPY --from=my-app-build /app/dist/qslog /usr/share/nginx/html