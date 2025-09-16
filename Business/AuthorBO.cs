using library_system.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace library_system.Business
{
    public class AuthorBO
    {
        private readonly AppDbContext _context;

        public AuthorBO(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Author> GetAuthors()
        {
            return _context.Authors.AsQueryable();
        }
        public IQueryable<Author> GetSingleAuthor(int id)
        {
            return _context.Authors
                .Where(a => a.Id == id)
                .AsQueryable();
        }

        public async Task AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            await SaveChange();

        }

        public async Task<Author> FindAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            return author;
        }
        public async Task UpdateAuthor(Author author)
        {
            _context.Authors.Update(author);
            await SaveChange();
        }

        public async Task RemoveAuthor(int id)
        {
            var author = await FindAuthor(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
            }
            await SaveChange();
        }

        public bool ExistsAuthor(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }
        public IQueryable<Author> SearchAuthor(string searchString, bool ShowAll = false)
        {
            var author = GetAuthors();
            
            if (!String.IsNullOrEmpty(searchString))
            {
                author = author.Where(s => s.FirstName!.ToUpper().Contains(searchString.ToUpper()) 
                                    || s.SecondName.ToUpper().Contains(searchString.ToUpper()));
            }
            return author;
        }

    }
}
