using Library_Management.Models;
using Library_Management.Helper;

namespace Library_Management.Interfaces
{
    public interface IBookRepository
    {
        Task<int> AddBookAsync(Book book);
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book> UpdateBookAsync(int id, UpdateBookDto book, Book existingBook);
        Task<int> DeleteBookAsync(int id);
    }

    public interface IBookService
    {
        Task<StandardApiResponse<Book>> AddBookAsync(Book book);
        Task<StandardApiResponse<IEnumerable<Book>>> GetAllBooksAsync();
        Task<StandardApiResponse<Book>> GetBookByIdAsync(int id);
        Task<StandardApiResponse<Book>> UpdateBookAsync(int id, UpdateBookDto book);
        Task<StandardApiResponse<Book>> DeleteBookAsync(int id);
    }
}
