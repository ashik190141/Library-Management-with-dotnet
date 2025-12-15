using System.ComponentModel.DataAnnotations;
namespace Library_Management.Models;

public class Book
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(150)]
    public required string Name { get; set; }
    [Required]
    [MaxLength(150)]
    public required string Author { get; set; }
    [Required]
    [MaxLength(200)]
    public required string Position { get; set; } 
    [Required]
    public int BookCopies { get; set; }
    public BookStatus Status {get; set;} = BookStatus.Available;
    public int CreatedBy { get; set; } 
    public int UpdatedBy { get; set; }
    [Timestamp]
    public DateTime CreatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    [Timestamp]
    public DateTime UpdatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
}

public class UpdateBookDto
{
    public string? Name { get; set; }
    public string? Author { get; set; }
    public string? Position { get; set; }
    public int? BookCopies { get; set; }
    public BookStatus? Status {get; set;}
    public DateTime UpdatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
}

public enum BookStatus
{
    Issued,
    Available
}
