using library_system.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Threading.Tasks;

namespace library_system.Business
{
    public class BookBO
    {
        private readonly AppDbContext _context;

        public BookBO(AppDbContext context)
        {
            _context = context;
        }

        public bool CreateNewBook(Book book)
        {
            if (book == null)
            {

                return false;
            }
            else
            {
                _context.Add(book);
                 _context.SaveChanges();
                return true;
            }

        }
        public bool GetEdited(int id, Book book)
        {
            if (id != book.Id)
            {
                return false;
            }

            try
            {
                _context.Update(book);
                 _context.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

        }

        public bool GetDeleted(int id) 
        {
            var book =  _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }
       
    }
}
