docker stop ECOMCatalogServiceAPI
docker rm ECOMCatalogServiceAPI
docker rmi ecomcatalogserviceapi

docker build -t ecomcatalogserviceapi:1 .
docker run -d -p 8080:8080 --name ECOMCatalogServiceAPI --net netECOM ecomcatalogserviceapi:1