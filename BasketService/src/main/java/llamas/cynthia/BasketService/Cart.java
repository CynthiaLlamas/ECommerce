package llamas.cynthia.BasketService;

import org.springframework.data.annotation.Id;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;
public class Cart implements Serializable {

    private static final long serialVersionUID = 1L;
    
    @Id
    private String cartGuid;
    private List<Item> items = new ArrayList<>();

    public Cart() {
    }

    public Cart(String cartGuid) {
        this.cartGuid = cartGuid;
    }

    public String getCartGuid() {
        return cartGuid;
    }

    public void setCartGuid(String cartGuid) {
        this.cartGuid = cartGuid;
    }

    public List<Item> getItems() {
        return items;
    }

    public void setItems(List<Item> items) {
        this.items = items;
    }
}
