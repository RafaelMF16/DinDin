FROM node:22.13.1

RUN apt-get update && apt-get install -y curl gnupg2

WORKDIR /workspace

RUN npm install -g typescript@5.7.2 @angular/cli@18

ARG USER=node
ARG GROUP=node
USER $USER