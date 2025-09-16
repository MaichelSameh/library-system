using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using library_system.Models;
using library_system.Business;

namespace library_system.Controllers
{
    public class AuthorsController : Controller
    {
        public readonly AuthorBO _authorBO;

        public AuthorsController(AuthorBO authorBO)
        {
            _authorBO = authorBO;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Authors.ToListAsync());
            var authors = await _authorBO.GetAuthors().ToListAsync();
            return View(authors);
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var author = await _authorBO.GetSingleAuthor((int)id).FirstOrDefaultAsync();
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }


        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,SecondName,BirthDate,DeadDate")] Author author)
        {
<<<<<<< HEAD
            await _authorBO.AddAuthor(author);
            return RedirectToAction(nameof(Index));
=======
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
>>>>>>> e64fd15b3d86ba5763869c43aa84577514db512a
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var author = await _authorBO.FindAuthor((int)id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,SecondName,BirthDate,DeadDate")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }
<<<<<<< HEAD
            try
            {
                _authorBO?.UpdateAuthor(author);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(author.Id))
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
=======

                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
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
>>>>>>> e64fd15b3d86ba5763869c43aa84577514db512a


        // GET: Authors/Delete/5

        //----------------------
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var author = _authorBO.GetSingleAuthor((int) id).FirstOrDefault();
        //    if (author == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(author);
        //}

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _authorBO.RemoveAuthor(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _authorBO.ExistsAuthor(id);
        }
    }
}
