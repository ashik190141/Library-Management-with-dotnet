using Library_Management.Interfaces;
using Library_Management.Models;
using Library_Management.Helper;

namespace Library_Management.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<StandardApiResponse<Book>> AddBookAsync(Book book)
        {
            book.CreatedBy = 0;
            book.UpdatedBy = 0;
            int res = await _bookRepository.AddBookAsync(book);
            if(res > 0){
                return new StandardApiResponse<Book>(true, "Book added successfully", book);
            }
            else
            {
                return new StandardApiResponse<Book>(false, "Failed to add book", null);
            }
        }

        public async Task<StandardApiResponse<IEnumerable<Book>>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return new StandardApiResponse<IEnumerable<Book>>(true, "Books retrieved successfully", books);
        }

        public async Task<StandardApiResponse<Book>> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if(book == null){
                return new StandardApiResponse<Book>(false, "Book not found", null);
            }
            return new StandardApiResponse<Book>(true, "Books retrieved successfully", book);
        }

        public async Task<StandardApiResponse<Book>> UpdateBookAsync(int id, UpdateBookDto updateBookDto)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if(book == null){
                return new StandardApiResponse<Book>(true, "Book Update successfully", null);
            }
            var updatedBook = await _bookRepository.UpdateBookAsync(id, updateBookDto, book);
            return new StandardApiResponse<Book>(true, "Book Update successfully", updatedBook);
        }

        public async Task<StandardApiResponse<Book>> DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            var removedBook = await _bookRepository.DeleteBookAsync(id);
            if(removedBook == 0)
            {
                return new StandardApiResponse<Book>(false, "Failed to delete book", null);
            }
            return new StandardApiResponse<Book>(true, "Book Delete successfully", book);
        }
    }
}