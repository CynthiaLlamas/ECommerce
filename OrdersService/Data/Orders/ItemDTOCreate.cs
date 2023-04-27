using System.ComponentModel.DataAnnotations;
public class ItemDTOCreate
{
    [Key]
    public Guid ItemGuid { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public double UnitPrice { get; set; }
}