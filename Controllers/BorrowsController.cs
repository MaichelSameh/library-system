using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using library_system.Models;

namespace library_system.Controllers
{
    public class BorrowsController : Controller
    {
        private readonly AppDbContext _context;

        public BorrowsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Borrows
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Borrows.Include(b => b.book).Include(b => b.Client).Where(b=> b.ReturnedAt == null);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Borrows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrow = await _context.Borrows
                .Include(b => b.book)
                .Include(b => b.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrow == null)
            {
                return NotFound();
            }

            return View(borrow);
        }

        // GET: Borrows/Create
        public IActionResult Create()
        {
         

            var availableBooks = _context.Books
            .Where(b => !b.borrows.Any(br => br.ReturnedAt == null))
            .ToList();

            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title");
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "FirstName");
            return View();
        }

        // POST: Borrows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,BookId,BorrowDate,BorrowDays")] Borrow borrow)
        {
            // Guard: prevent double-borrowing
            bool isOut = await _context.Borrows.AnyAsync(br =>
                br.BookId == borrow.BookId && br.ReturnedAt == null);
            bool check = false;
            check = _context.Books.Where(b => b.Id == borrow.BookId).Any();

            if (isOut && check == true)
            {
                ModelState.AddModelError("BookId", "This book is currently borrowed and cannot be borrowed again.");
                return RedirectToAction(nameof(Create));
            }


            else { 
            _context.Add(borrow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


            // Re-populate select lists (respecting availability again)
            var availableBooks = _context.Books
                .Where(b => !b.borrows.Any(br => br.ReturnedAt == null))
                .ToList();

            ViewData["BookId"] = new SelectList(availableBooks, "Id", "Title", borrow.BookId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "FirstName", borrow.ClientId);
        }
        // GET: Borrows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrow = await _context.Borrows.FindAsync(id);
            if (borrow == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", borrow.BookId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "FirstName", borrow.ClientId);
            return View(borrow);
        }

        // POST: Borrows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,BookId,BorrowDate,BorrowDays")] Borrow borrow)
        {
            if (id != borrow.Id)
            {
                return NotFound();
            }


            try
            {
                _context.Update(borrow);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowExists(borrow.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", borrow.BookId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "FirstName", borrow.ClientId);
        }

        // GET: Borrows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrow = await _context.Borrows
                .Include(b => b.book)
                .Include(b => b.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrow == null)
            {
                return NotFound();
            }

            return View(borrow);
        }

        // POST: Borrows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrow = await _context.Borrows.FindAsync(id);
            if (borrow != null)
            {
                _context.Borrows.Remove(borrow);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowExists(int id)
        {
            return _context.Borrows.Any(e => e.Id == id);
        }

        // BorrowsController.cs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id)
        {
            var borrow = await _context.Borrows.FindAsync(id);
            if (borrow == null) return NotFound();

            if (borrow.ReturnedAt == null)
            {
                borrow.ReturnedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
