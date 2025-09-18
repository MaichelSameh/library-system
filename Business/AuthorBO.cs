using library_system.Models;
using Microsoft.EntityFrameworkCore;

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
            return GetAuthors(false);
        }

        public IQueryable<Author> GetAuthors(bool withDeleted) // if false --> return all except deleted
        {
            var authorQuery = _context.Authors.AsQueryable();

            if (!withDeleted)
            {
                // Removing deleted records in case the [withDeleted] is false
                authorQuery = authorQuery.Where(author => author.DeletedAt == null);
            }

            return authorQuery;
        }

        public IQueryable<Author> GetSingleAuthor(int id)
        {
            return _context.Authors
                .Where(a => a.Id == id)
                .AsQueryable();
        }

        public IQueryable<Author> GetDeletedAuthors()
        {
            return _context.Authors.Where(author => author.DeletedAt != null).AsQueryable();
        }

        public async Task CleanTable()
        {
            IQueryable<Author> deleteQuery = from item in _context.Authors
                               where item.DeletedAt != null && item.DeletedAt < DateTime.Now.Subtract(TimeSpan.FromDays(30))
                               select new Author
                               {
                                   Id = item.Id,
                               }
                ;
            await deleteQuery.ExecuteDeleteAsync();
        }

        public async Task AddAuthor(Author author)
        {
            author.CreatedAt = DateTime.Now;
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task<Author?> FindAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            return author;
        }

        public async Task UpdateAuthor(Author author)
        {
            author.UpdatedAt = DateTime.Now;
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAuthor(int id)
        {
            var author = await FindAuthor(id);
            if (author != null)
            {
                author.DeletedAt = DateTime.Now;
                await UpdateAuthor(author);
            }

            await _context.SaveChangesAsync();
        }

        public async Task ForceDelete(int id)
        {
            var author = await FindAuthor(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
            }

            await _context.SaveChangesAsync();
        }

        public bool ExistsAuthor(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
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