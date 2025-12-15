using Library_Management.Models;
using Library_Management.Helper;

namespace Library_Management.Interfaces
{
    public interface IIssueBookRepository
    {
        Task<int> CreateIssueBookAsync(IssueBook issueBook);
        Task<IEnumerable<IssueBook>> GetAllIssueBooksAsync();
        Task<IssueBook?> GetIssueBookAsync(int userId, int bookId);
        Task<IssueBook?> ReturnBookAsync(ReturnBookDto returnBookDto, IssueBook issueBook);
    }

    public interface IIssueBookService
    {
        Task<StandardApiResponse<IssueBook>> CreateIssueBookAsync(IssueBook issueBook);
        Task<StandardApiResponse<IEnumerable<IssueBook>>> GetAllIssueBooksAsync();
        Task<StandardApiResponse<IssueBook>> ReturnBookAsync(ReturnBookDto returnBookDto);
    }
}
