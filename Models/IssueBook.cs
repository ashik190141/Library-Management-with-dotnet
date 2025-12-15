using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models;

public class IssueBook
{
    [Key]
    public int Id {get; set;}
    [Required]
    public int UserId {get; set;}
    public User? IssuedUser {get; set;}
    [Required]
    public int IssuedBy {get; set;}
    public User? IssuedByUser {get; set;}
    [Required]
    public int BookId {get; set;}
    public Book? Book {get; set;}
    public DateTime IssueDate {get; set;}
    public DateTime? ReturnDate {get; set;}
    public DateTime ExpireDate {get; set;}
    public IssueBookStatus Status {get; set;} = IssueBookStatus.Issued;
    public int Penalty {get; set;} = 0;
    public int CreatedBy { get; set; } 
    public int UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    public DateTime UpdatedAt { get; set; }
}

public class ReturnBookDto
{
    public required int UserId {get; set;}
    public required int BookId {get; set;}
    public DateTime ReturnDate { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    public IssueBookStatus Status {get; set;}
    public int? Penalty {get; set;}
    public DateTime UpdatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    public int UpdatedBy { get; set; }
}

public enum IssueBookStatus
{
    Issued,
    Returned
}