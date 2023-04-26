package llamas.cynthia.Catalog;

import org.springframework.data.mongodb.repository.MongoRepository;

import java.util.UUID;
import java.util.List;

public interface ItemRepository extends MongoRepository<Item, UUID> {
    public List<Item> findItemByNameOrDescription(String txt1,String txt2);
    public List<Item> findItemsByNameContainingIgnoreCase(String txt1);
    public List<Item> findByUnitPriceLessThanEqual(double maxPrice);
}
