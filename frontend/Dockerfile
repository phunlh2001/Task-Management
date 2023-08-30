FROM node:16

WORKDIR /app

COPY package*.json ./

RUN yarn install

COPY . .

RUN yarn build

EXPOSE 3000

ENV PATH="/app/node_modules/.bin:$PATH"

CMD ["yarn", "start"]
