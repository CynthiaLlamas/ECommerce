using System.ComponentModel.DataAnnotations;
public class ItemDTOCreate{
    [Key]
    public Guid ItemGuid { get; set;}
    [Required]
    public Guid OrderGuid {get; set;}
    [Required]
    public string Name {get; set;}
    [Required]
    public string Description {get; set;}
    [Required]
    public double UnitPrice {get; set;}
}