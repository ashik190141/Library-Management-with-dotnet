using Library_Management.Data;
using Library_Management.Interfaces;
using Library_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_Management.Repositories;

public class BookRepository(LibraryContext context) : BaseRepository(context), IBookRepository
{
    
    public async Task<int> AddBookAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        int response = await SaveChangesAsync();
        if (response > 0)
        {
            return book.Id;
        }
        return 0;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task<Book> UpdateBookAsync(int id, UpdateBookDto book, Book existingBook)
    {
        existingBook.Name = book.Name ?? existingBook.Name;
        existingBook.Author = book.Author ?? existingBook.Author;
        existingBook.Position = book.Position ?? existingBook.Position;
        existingBook.BookCopies = book.BookCopies ?? existingBook.BookCopies;
        existingBook.Status = book.Status ?? existingBook.Status;
        existingBook.UpdatedBy = 1;
        existingBook.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

        _context.Books.Update(existingBook);
        await SaveChangesAsync();

        return existingBook;
    }

    public async Task<int> DeleteBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if(book != null)
        {
            _context.Books.Remove(book);
            return await SaveChangesAsync();
        }
        return 0;
    }
}
