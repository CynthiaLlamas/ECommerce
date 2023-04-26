package llamas.cynthia.Catalog;

import org.springframework.data.annotation.Id;

import java.io.Serializable;
import java.util.UUID;

public class Item {
    @Id
    private UUID itemGuid;
    private String name;
    private String description;
    private double unitPrice;

    public UUID getItemGuid() {
        return itemGuid;
    }

    public void setItemGuid(UUID itemGuid) {
        this.itemGuid = itemGuid;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public double getUnitPrice() {
        return unitPrice;
    }

    public void setUnitPrice(double unitPrice) {
        this.unitPrice = unitPrice;
    }
}
