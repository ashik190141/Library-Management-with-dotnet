using Library_Management.Data;

namespace Library_Management.Repositories
{
    public class BaseRepository
    {
        protected readonly LibraryContext _context;

        protected BaseRepository(LibraryContext context)
        {
            _context = context;
        }

        protected async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}