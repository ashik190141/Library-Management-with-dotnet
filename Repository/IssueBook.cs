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

        public async Task<IssueBook?> GetIssueBookAsync(int userId, int bookId)
        {
            return await _context.IssueBooks.Where(i => i.Status == 0 && i.UserId == userId && i.BookId == bookId)
            .Include(i => i.Book)
            .Include(i => i.IssuedUser)
            .Include(i => i.IssuedByUser)
            .FirstOrDefaultAsync();
        }

        public async Task<IssueBook?> ReturnBookAsync(ReturnBookDto returnBookDto, IssueBook issueBook)
        {
            issueBook.ReturnDate = returnBookDto.ReturnDate;
            issueBook.Status = returnBookDto.Status;
            issueBook.Penalty = returnBookDto.Penalty ?? issueBook.Penalty;
            issueBook.UpdatedAt = returnBookDto.UpdatedAt;
            issueBook.UpdatedBy = returnBookDto.UpdatedBy;

            _context.IssueBooks.Update(issueBook);
            int response = await SaveChangesAsync();
            
            if(response <= 0)
            {
                return null;
            }
            return issueBook;
        }
    }
}
