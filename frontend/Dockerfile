# Use the official Node.js image as the base image
FROM node:16

# Set working directory
WORKDIR /src

# Copy package.json and package-lock.json
COPY package*.json ./

# Install dependencies
RUN yarn install

# Copy all files to the container
COPY . .

# Build the React app
RUN yarn build

# Set environment variables if needed

# Expose the port
EXPOSE 3000

# Command to start the app
CMD ["yarn", "start"]
