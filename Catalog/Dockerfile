#Takes existing  source code preparing and building a war file
FROM maven:3.9-sapmachine-17 AS build
COPY src /home/app/src
COPY pom.xml /home/app
RUN mvn -f /home/app/pom.xml clean package

FROM tomcat:11.0.0-jdk17
COPY --from=build /home/app/target/ECOMCatalogServiceAPI.war /usr/local/tomcat/webapps/ROOT.war

ENV SERVER_PORT=8080
ENV MONGO_HOST=localhost
ENV MONGO_PORT=2717
ENV MONGO_DB=Catalog
ENV EUREKA_HOST=ECOMEurekaRegistry