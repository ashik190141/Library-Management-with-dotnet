using Library_Management.Data;
using Library_Management.Interfaces;
using Library_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_Management.Repositories
{
    public class IssueBookRepository(LibraryContext context) : BaseRepository(context), IIssueBookRepository
    {
        
        public async Task<int> CreateIssueBookAsync(IssueBook issueBook)
        {
            await _context.IssueBooks.AddAsync(issueBook);
            int response = await SaveChangesAsync();
            if (response > 0)
            {
                return issueBook.Id;
            }
            return 0;
        }

        public async Task<IEnumerable<IssueBook>> GetAllIssueBooksAsync()
        {
            return await _context.IssueBooks.Where(i => i.Status == 0)
            .Include(i => i.Book)
            .Include(i => i.IssuedUser)
            .Include(i => i.IssuedByUser)
            .ToListAsync();
        }
    }
}
