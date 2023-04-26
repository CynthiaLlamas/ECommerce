using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase{
    private readonly ILogger<PaymentController> _logger;
    private readonly IPaymentRepository _paymentRepository;
    public PaymentController(ILogger<PaymentController> logger,IPaymentRepository paymentRepository){
        _logger= logger;
        _paymentRepository = paymentRepository;
    }
    [HttpPost]
    public async Task<IActionResult> Create(){
        Response.Clear();
        Stream stream = Request.Body;
        PaymentDTOCreate paymentDTO =await  JsonSerializer.DeserializeAsync<PaymentDTOCreate>(stream);
        Payment payment = new Payment();
        DateTime exp= DateTime.Parse(paymentDTO.ExpirationDateString); 
        if(DateTime.Now.CompareTo(exp)>=0) throw new Exception("The expiration date has passed");
        payment.ExpirationDate = exp;
        string checkFirst = paymentDTO.CardNumber.Substring(0,1);
        if(checkFirst!="5"&&checkFirst!="4") throw new Exception("Invalid card number");
        payment.CardNumber = paymentDTO.CardNumber;
        payment.StreetAddress = paymentDTO.StreetAddress;
        payment.UserGuid = paymentDTO.UserGuid;
        try{
            var cardGuid = await _paymentRepository.Create(payment);
            return Ok(new
                {
                    Success = true,
                    Message = "Order Created.",
                    CardGuid = cardGuid
                }
            );
        }catch(Exception ex){
            System.Console.WriteLine(ex.Message);
            return StatusCode(500,ex.Message);
        }
    }

    
    }