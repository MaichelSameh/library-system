using library_system.Business;
using library_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

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

        //public async Task<IActionResult> Create([Bind("Id,Company")] Pubblisher pubblisher)

        // POST: Pubblishers/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Company")] Pubblisher pubblisher)
        {
           var Pubblishers = new PubblisherBO(_context);
            Pubblishers.CreatePubblisher(pubblisher);
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

        //public async Task<IActionResult> Edit(int id, [Bind("Id,Company")] Pubblisher pubblisher)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Company")] Pubblisher pubblisher)
        {
            var Pubblishers = new PubblisherBO(_context);
            var check = Pubblishers.updatePubblisher(pubblisher);
            if (check)
                return RedirectToAction(nameof(Index));
            else
                return NotFound();
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

        //public async Task<IActionResult> DeleteConfirmed(int id)
        // POST: Pubblishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var Pubblishers = new PubblisherBO(_context);
            var check = Pubblishers.DeletePubblisher(id);
            if (check)
                return RedirectToAction(nameof(Index));
            else
                return NotFound();
        }

        // GET: Pubblishers/search?q=...
        [HttpGet("search")]
        public  IActionResult Search(string? q)
        {
            var bo = new PubblisherBO(_context);
            var list =  bo.SearchAsync(q);
            
            ViewData["q"] = q;
            return View("Index", list.ToList());
        }
    }
}
