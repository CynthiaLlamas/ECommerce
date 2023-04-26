docker network create netECOM
docker run -d -p 2717:27017 --name mongoCatalog --net netECOM mongo:latest