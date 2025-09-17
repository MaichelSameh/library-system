using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using library_system.Models;
using library_system.Business;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace library_system.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Books.Include(b => b.Authors).Include(b => b.Pubblisher).Include(b => b.Tipology);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Pubblisher)
                .Include(b => b.Tipology)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "FirstName");
            ViewData["PubblisherId"] = new SelectList(_context.Pubblishers, "Id", "Company");
            ViewData["TipologyId"] = new SelectList(_context.Tipologys, "Id", "Description");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,TipologyId,ISBN,AuthorId,PubblisherId,PubblicDate")] Book book)
        {

            var bookBO = new BookBO(_context);
            var check = bookBO.CreateNewBook(book);
            if (check)
                return RedirectToAction(nameof(Index));

            else
                return NotFound();


        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "FirstName", book.AuthorId);
            ViewData["PubblisherId"] = new SelectList(_context.Pubblishers, "Id", "Company", book.PubblisherId);
            ViewData["TipologyId"] = new SelectList(_context.Tipologys, "Id", "Description", book.TipologyId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,TipologyId,ISBN,AuthorId,PubblisherId,PubblicDate")] Book book)
        {
            var bookBO = new BookBO(_context);
            var check = bookBO.GetEdited(id, book);

            if (check)
                return RedirectToAction(nameof(Index));

            else
                return NotFound();

        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Pubblisher)
                .Include(b => b.Tipology)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var BookBO = new BookBO(_context);
            var check = BookBO.GetDeleted(id);
            if (check)
                return RedirectToAction(nameof(Index));

            else 
                return NotFound();
        }


        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
