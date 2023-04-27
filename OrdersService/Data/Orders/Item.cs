using System.ComponentModel.DataAnnotations;
public class Item
{
    [Key]
    public Guid ItemGuid { get; set; }

    [Required]
    public Guid OrderGuid { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public double UnitPrice { get; set; }
}