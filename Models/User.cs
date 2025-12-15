using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models;

public class User
{
    [Key]
    public int Id {get; set;}
    public required string Name {get; set;}
    public required string Email {get; set;}
    public required string Password {get; set;}
    public int CreatedBy { get; set; } 
    public int UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    public DateTime UpdatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
}

public class UpdateUserDto
{
    public string? Name {get; set;}
    public string? Email {get; set;}
}