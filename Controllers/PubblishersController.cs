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
    public class PubblishersController : Controller
    {
        private readonly AppDbContext _context;

        public PubblishersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Pubblishers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pubblishers.ToListAsync());
        }

        // GET: Pubblishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pubblisher = await _context.Pubblishers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pubblisher == null)
            {
                return NotFound();
            }

            return View(pubblisher);
        }

        // GET: Pubblishers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pubblishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Company")] Pubblisher pubblisher)
        {
                _context.Add(pubblisher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Pubblishers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pubblisher = await _context.Pubblishers.FindAsync(id);
            if (pubblisher == null)
            {
                return NotFound();
            }
            return View(pubblisher);
        }

        // POST: Pubblishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Company")] Pubblisher pubblisher)
        {
            if (id != pubblisher.Id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(pubblisher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PubblisherExists(pubblisher.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
        }

        // GET: Pubblishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pubblisher = await _context.Pubblishers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pubblisher == null)
            {
                return NotFound();
            }

            return View(pubblisher);
        }

        // POST: Pubblishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pubblisher = await _context.Pubblishers.FindAsync(id);
            if (pubblisher != null)
            {
                _context.Pubblishers.Remove(pubblisher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PubblisherExists(int id)
        {
            return _context.Pubblishers.Any(e => e.Id == id);
        }
    }
}
