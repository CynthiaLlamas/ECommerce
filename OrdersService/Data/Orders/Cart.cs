using System.ComponentModel.DataAnnotations;
public class Cart{
    [Key]
    public Guid cartId {get; set; }
    [Required]
    public List<ItemDTOCreate>? Items { get; set; }
}