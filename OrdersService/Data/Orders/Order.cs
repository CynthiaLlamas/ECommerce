using System.ComponentModel.DataAnnotations;
public class Order {
    [Key]
    public Guid OrderGuid { get; set; }
    [Required]
    public Guid UserGuid {get; set;}
    [Required]
    public Guid CartGuid {get;set;}
    [Required]
    public DateTime CreatedDate { get; set; }
    [Required]
    public List<Item>? Items {get; set;}
}