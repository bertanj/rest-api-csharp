using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Models;

namespace RestWithASPNET.Repository
{
    public class BookRepository: IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> FindByAuthorAsync(string author)
        { 
            return await _context.Books
                .Where(b => b.Author.Contains(author))
                .ToListAsync();
        }

        public async Task<List<Book>> FindByTitleAsync(string title)
        { 
            return await _context.Books
                .Where(b => b.Title.Contains(title))
                .ToListAsync();
        }

        public async Task<List<Book>> FindByLaunchDateAsync(DateTime date)
        { 
            return await _context.Books
                .Where(b => b.LaunchDate.Date == date.Date)
                .ToListAsync();
        }

        public async Task<List<Book>> FindAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> FindByIdAsync(long id)
        {
            return await _context.Books.SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> CreateAsync(Book book)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            var result = await _context.Books.SingleOrDefaultAsync(b => b.Id == book.Id);
            if (result == null) return null;

            _context.Entry(result).CurrentValues.SetValues(book);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task DeleteAsync(long Id)
        {
            var result = await _context.Books.SingleOrDefaultAsync(b => b.Id == Id);
            if (result != null) 
            {
                _context.Books.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

    }
}
