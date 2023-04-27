package llamas.cynthia.BasketService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;

import java.util.*;
import java.util.UUID;
import java.util.stream.Collectors;

@RestController
@RequestMapping("/cart")
public class CartRestController {
    @Autowired
    private RedisTemplate<String, Cart> redisTemplate;

    @PostMapping("/{cartId}")
    @ResponseStatus(HttpStatus.CREATED)
    public Cart addSingleItemToCart(@PathVariable String cartId, @RequestBody Item item) {
        Cart cart;
        if ("new".equals(cartId))
            cart = new Cart(UUID.randomUUID().toString());
        else
            cart = redisTemplate.opsForValue().get(cartId);
        cart.getItems().add(item);
        redisTemplate.opsForValue().set(cart.getCartGuid(), cart);
        return cart;
    }

    @PostMapping("/addItems/{cartId}")
    @ResponseStatus(HttpStatus.CREATED)
    public Cart addManyItemsToCart(@PathVariable String cartId, @RequestBody List<Item> items) {
        Cart cart = null;
        if ("new".equals(cartId))
            cart = new Cart(UUID.randomUUID().toString());
        else
            cart = redisTemplate.opsForValue().get(cartId);
        cart.getItems().addAll(items);
        redisTemplate.opsForValue().set(cart.getCartGuid(), cart);
        return cart;
    }

    @GetMapping("/{cartId}")
    @ResponseStatus(HttpStatus.OK)
    public Cart getCartById(@PathVariable String cartId) {
        Cart cart = redisTemplate.opsForValue().get(cartId);
        return cart;
    }

    @DeleteMapping("/{cartId}/{itemId}")
    @ResponseStatus(HttpStatus.GONE)
    public void deleteItemInCart(@PathVariable String cartId, @PathVariable UUID itemId) {
        Cart cart = redisTemplate.opsForValue().get(cartId);
        cart.setItems(cart
                .getItems()
                .stream()
                .filter(c -> !c.getItemGuid().equals(itemId))
                .collect(Collectors.toList()));
        redisTemplate.opsForValue().set(cartId, cart);
    }

    @DeleteMapping("/{cartId}")
    @ResponseStatus(HttpStatus.GONE)
    public void removeCart(@PathVariable String basketId) {
        redisTemplate.delete(basketId);
    }

}
