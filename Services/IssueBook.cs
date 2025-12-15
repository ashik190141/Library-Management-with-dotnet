using Library_Management.Interfaces;
using Library_Management.Models;
using Library_Management.Helper;
using Library_Management.Data;

namespace Library_Management.Services;

public class IssueBookService : IIssueBookService
{
    private readonly IIssueBookRepository _issueBookRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly LibraryContext _context;

    public IssueBookService(IIssueBookRepository issueBookRepository, IBookRepository bookRepository, IUserRepository userRepository, LibraryContext context)
    {
        _issueBookRepository = issueBookRepository;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _context = context;
    }

    public async Task<StandardApiResponse<IssueBook>> CreateIssueBookAsync(IssueBook issueBook)
    {
        issueBook.CreatedBy = 0;
        issueBook.UpdatedBy = 0;
        issueBook.IssueDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        issueBook.ExpireDate = issueBook.IssueDate.AddDays(7);
        issueBook.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var targetUser = await _userRepository.GetUserByIdAsync(issueBook.UserId);
            if (targetUser == null)
            {
                return new StandardApiResponse<IssueBook>(false, "User not found", null);
            }

            var issuer = await _userRepository.GetUserByIdAsync(issueBook.IssuedBy);
            if (issuer == null)
            {
                return new StandardApiResponse<IssueBook>(false, "Issuer (IssuedBy) not found", null);
            }

            var issuedBook = await _bookRepository.GetBookByIdAsync(issueBook.BookId);
            if (issuedBook?.BookCopies <= 0)
            {
                return new StandardApiResponse<IssueBook>(false, "Book Copies is not available", null);
            }

            if (issuedBook != null && issuedBook.BookCopies > 0)
            {
                var updateBookDto = new UpdateBookDto
                {
                    BookCopies = issuedBook.BookCopies - 1,
                    Status = BookStatus.Issued
                };
                await _bookRepository.UpdateBookAsync(issueBook.BookId, updateBookDto, issuedBook);
            }

            int res = await _issueBookRepository.CreateIssueBookAsync(issueBook);
            if (res > 0)
            {
                await transaction.CommitAsync();
                return new StandardApiResponse<IssueBook>(true, "Book Issued successfully", issueBook);
            }
            else
            {
                await transaction.RollbackAsync();
                return new StandardApiResponse<IssueBook>(false, "Failed to Issue book", null);
            }
        }
        catch
        {
            await transaction.RollbackAsync();
            return new StandardApiResponse<IssueBook>(false, "Internal Server Error", null);
        }
    }

    public async Task<StandardApiResponse<IEnumerable<IssueBook>>> GetAllIssueBooksAsync()
    {
        return new StandardApiResponse<IEnumerable<IssueBook>>(true, "Issue Books fetched successfully", await _issueBookRepository.GetAllIssueBooksAsync());
    }

    public async Task<StandardApiResponse<IssueBook>> ReturnBookAsync(ReturnBookDto returnBookDto)
    {
        var issueBook = await _issueBookRepository.GetIssueBookAsync(returnBookDto.UserId, returnBookDto.BookId);
        if (issueBook == null)
        {
            return new StandardApiResponse<IssueBook>(false, "No issued book found for this user and book", null);
        }

        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            returnBookDto.UpdatedBy = 1;
            returnBookDto.Status = IssueBookStatus.Returned;

            if(issueBook.ExpireDate < returnBookDto.ReturnDate)
            {
                int daysLate = (returnBookDto.ReturnDate - issueBook.ExpireDate).Days;
                returnBookDto.Penalty = daysLate * 5;
            }

            var issuedBook = await _bookRepository.GetBookByIdAsync(returnBookDto.BookId);
            if (issuedBook != null)
            {
                var updateBookDto = new UpdateBookDto
                {
                    BookCopies = issuedBook.BookCopies + 1,
                    Status = BookStatus.Available
                };
                await _bookRepository.UpdateBookAsync(issueBook.BookId, updateBookDto, issuedBook);
            }

            var updatedIssueBook = await _issueBookRepository.ReturnBookAsync(returnBookDto, issueBook);
            if (updatedIssueBook != null)
            {
                await transaction.CommitAsync();
                return new StandardApiResponse<IssueBook>(true, "Book Return successfully", issueBook);
            }
            else
            {
                await transaction.RollbackAsync();
                return new StandardApiResponse<IssueBook>(false, "Failed to Return book", null);
            }
        }
        catch
        {
            await transaction.RollbackAsync();
            return new StandardApiResponse<IssueBook>(false, "Internal Server Error", null);
        }
    }
}