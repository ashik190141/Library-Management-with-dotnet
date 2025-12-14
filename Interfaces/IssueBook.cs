using Library_Management.Models;
using Library_Management.Helper;

namespace Library_Management.Interfaces
{
    public interface IIssueBookRepository
    {
        Task<int> CreateIssueBookAsync(IssueBook issueBook);
        Task<IEnumerable<IssueBook>> GetAllIssueBooksAsync();
    }

    public interface IIssueBookService
    {
        Task<StandardApiResponse<IssueBook>> CreateIssueBookAsync(IssueBook issueBook);
        Task<StandardApiResponse<IEnumerable<IssueBook>>> GetAllIssueBooksAsync();
    }
}
