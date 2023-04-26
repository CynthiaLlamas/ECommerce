using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
[JsonObject]
public class Payment{
    [Key]
    public Guid CardGuid {get; set;}
    //[Required]
    public Guid UserGuid {get; set; }
    //[Required]
    public string? StreetAddress { get; set; }
    //[Required]
    public string? CardNumber {get; set;}
    //[Required]
    public DateTime ExpirationDate {get; set;}
    public int CVV {get; set;}
}