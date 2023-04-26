using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
public class PaymentDTOCreate{
    [Key]
    public Guid CardGuid {get; set;}
    //[Required]
    public Guid UserGuid {get; set; }
    //[Required]
    public string? StreetAddress { get; set; }
    //[Required]
    public string? CardNumber {get; set;}
    //[Required]
    public String? ExpirationDateString {get; set;}
    public int CVV {get; set;}
    [JsonConstructor]
    public PaymentDTOCreate(){}
}