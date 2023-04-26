using System;
using System.ComponentModel.DataAnnotations;
public class OrderDTOCreate{
    [Required]
    public Guid CartGuid {get; set; }
    [Required]
    public Boolean ReadCart {get; set; }
    public List<ItemDTOCreate>? Items {get; set;}
}