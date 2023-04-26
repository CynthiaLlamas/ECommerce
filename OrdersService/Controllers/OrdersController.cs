using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrderRepository _ordersRepository;
    public OrdersController(ILogger<OrdersController> logger, IOrderRepository orderRepository){
        _logger = logger;
        _ordersRepository = orderRepository;
    }
    [HttpPost]
    public async Task<IActionResult> Create(OrderDTOCreate orderDTOCreate){
        String userGuid = "E8E369C0-960B-4584-9A81-F9FF9F98DBD6";
        try{
            if(String.IsNullOrEmpty(userGuid)) throw new Exception("it was null.");
            var orderGuid = await _ordersRepository.Create(orderDTOCreate, new Guid(userGuid));
            return Ok(new
                {
                    Success = true,
                    Message = "Order Created.",
                    OrderGuid = orderGuid
                }
            );
        }catch(Exception ex){
            System.Console.WriteLine( ex.Message);
            return StatusCode(500,ex.Message);
        }
    }
    [HttpGet]
    [Route("withitems")]
    public async Task<IActionResult> GetAllWithItems(){
        try{
            var Data = _ordersRepository.GetAllWithItems();
            _ordersRepository.GetAllWithItems();
            return Ok(new
            {
                Success = true,
                Message = "All Order Items returned",
                Data
            });
        }catch(Exception ex){
            System.Console.WriteLine(ex.Message);
            return StatusCode(500,ex.Message);
        }
    }
}