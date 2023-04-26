package llamas.cynthia.Catalog;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

// eureka
import org.springframework.cloud.client.discovery.EnableDiscoveryClient;

// eureka
@EnableDiscoveryClient
@SpringBootApplication
public class CatalogApplication {

	public static void main(String[] args) {
		SpringApplication.run(CatalogApplication.class, args);
	}

}
