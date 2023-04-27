using Dapper;
using System.Data;

public class OrderRepository : IOrderRepository
{
    private readonly DapperContext _context;
    public OrderRepository(DapperContext context)
    {
        _context = context;
    }
    public async Task<Guid> Create(OrderDTOCreate orderDTOCreate, Guid userGuid)
    {
        string sql = "INSERT INTO Orders (OrderGuid, UserGuid, CartGuid, CreatedDate) VALUES (@OrderGuid, @UserGuid, @CartGuid, @CreatedDate)";
        var parameterOrders = new DynamicParameters();
        Guid orderGuid = Guid.NewGuid();
        parameterOrders.Add("OrderGuid", orderGuid, DbType.Guid);
        parameterOrders.Add("UserGuid", userGuid, DbType.Guid);
        parameterOrders.Add("CartGuid", orderDTOCreate.CartGuid, DbType.Guid);
        DateTime createdDate = DateTime.Now;
        parameterOrders.Add("CreatedDate", createdDate, DbType.DateTime);
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(sql, parameterOrders);
        }
        if (orderDTOCreate.ReadCart == true)
        {
            System.Console.WriteLine("Read cart");
            var client = new HttpClient();
            var cart = await client.GetFromJsonAsync<Cart>($"http://localhost:3030/cart/{orderDTOCreate.CartGuid}");
            if (cart != null && cart.Items != null)
            {
                System.Console.WriteLine(cart);
                orderDTOCreate.Items = cart.Items;
                string sqlItems = "INSERT INTO Items (ItemGuid, OrderGuid, Name, Description, UnitPrice) VALUES (@ItemGuid, @OrderGuid, @Name, @Description, @UnitPrice)";
                foreach (ItemDTOCreate item in orderDTOCreate.Items)
                {
                    var parametersItems = new DynamicParameters();
                    parametersItems.Add("ItemGuid", item.ItemGuid, DbType.Guid);
                    parametersItems.Add("OrderGuid", orderGuid, DbType.Guid);
                    parametersItems.Add("Name", item.Name, DbType.String);
                    parametersItems.Add("Description", item.Description, DbType.String);
                    parametersItems.Add("UnitPrice", item.UnitPrice, DbType.Currency);
                    using (var connection = _context.CreateConnection())
                    {
                        await connection.ExecuteAsync(sqlItems, parametersItems);
                    }
                }
            }
        }

        return orderGuid;
    }
    public IEnumerable<Order> GetAllWithItems()
    {
        string sql = "SELECT * FROM Orders AS O INNER JOIN Items AS I ON O.OrderGuid = I.OrderGuid;";
        using (var connection = _context.CreateConnection())
        {
            var orderDictionary = new Dictionary<Guid, Order>();
            var ordersList = connection.Query<Order, Item, Order>(
                sql,
                (order, orderItem) =>
                {
                    Order orderEntry;
                    if (!orderDictionary.TryGetValue(order.OrderGuid, out orderEntry))
                    {
                        orderEntry = order;
                        orderEntry.Items = orderEntry.Items ?? new List<Item>();
                        orderDictionary.Add(orderEntry.OrderGuid, orderEntry);
                    }
                    orderEntry.Items.Add(orderItem);
                    return orderEntry;
                },
                splitOn: "ItemGuid")
            .Distinct()
            .ToList();
            System.Console.WriteLine("Orders Counr: " + ordersList.Count);
            return ordersList;
        }
    }

    public async Task<IEnumerable<Order>> GetAllProducts()
    {
        string sql = "SELECT * FROM orders";

        using (var connection = _context.CreateConnection())
        {
            var orders = await connection.QueryAsync<Order>(sql);
            return orders;
        }
    }

}