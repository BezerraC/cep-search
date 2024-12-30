# Imagem base do Node.js para construir o frontend
FROM node:18 AS build

# Define o diretório de trabalho
WORKDIR /app

# Copia os arquivos de configuração e dependências
COPY package*.json ./
RUN npm install

# Copia o restante do código-fonte
COPY . .

# Compila o projeto Vue.js
RUN npm run build

# Imagem final para servir o frontend
FROM nginx:alpine

# Copia os arquivos compilados para o diretório do Nginx
COPY --from=build /app/dist /usr/share/nginx/html

# Exponha a porta que o Nginx usará
EXPOSE 80

# Comando para iniciar o Nginx
CMD ["nginx", "-g", "daemon off;"]
