using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public Guid UserGuid { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public DateTime CreatedDate { get; set; }
    string toString()
    {
        return this.UserGuid.ToString();
    }
}