package llamas.cynthia.Catalog;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;

import java.util.NoSuchElementException;
import java.util.UUID;
import java.util.List;

@RestController
@RequestMapping("/catalog")
public class ItemRestController {

    @Autowired
    ItemRepository itemRepo;

    // Posts
    @PostMapping("")
    @ResponseStatus(HttpStatus.CREATED)
    public void createItem(@RequestBody Item item) {
        item.setItemGuid(UUID.randomUUID());
        itemRepo.save(item);
    }

    @PostMapping("/addMany")
    @ResponseStatus(HttpStatus.CREATED)
    public void createItems(@RequestBody List<Item> items) {
        for (Item item : items) {
            item.setItemGuid(UUID.randomUUID());
        }
        itemRepo.saveAll(items);
    }

    // Gets
    @GetMapping("")
    @ResponseStatus(HttpStatus.OK)
    public List<Item> getAllItems() {
        return itemRepo.findAll();
    }

    @GetMapping("/{itemUUID}")
    @ResponseStatus(HttpStatus.OK)
    public Item getItemByUUID(@PathVariable UUID itemUUID) {
        return itemRepo.findById(itemUUID).orElseThrow(NoSuchElementException::new);
    }

    @GetMapping("/search/{txtSearch}")
    @ResponseStatus(HttpStatus.OK)
    public List<Item> searchItem(@PathVariable String txtSearch) {
        return itemRepo.findItemsByNameContainingIgnoreCase(txtSearch);
    }

    @GetMapping("/price/{maxPrice}")
    @ResponseStatus(HttpStatus.OK)
    public List<Item> searchItem(@PathVariable double maxPrice) {
        return itemRepo.findByUnitPriceLessThanEqual(maxPrice);
    }

    // PUTS/PATCHES
    @PutMapping("/{itemUUID}")
    @ResponseStatus(HttpStatus.OK)
    public void updateItem(@PathVariable UUID itemUUID, @RequestBody Item item) {
        if (!itemUUID.equals(item.getItemGuid())) {
            throw new RuntimeException("The IDS don't match");
        }
        itemRepo.save(item);
    }
    // Deletes
}
