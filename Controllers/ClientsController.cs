using library_system.Business;
using library_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace library_system.Controllers
{
    public class ClientsController : Controller
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Indexlog()
        {


            return View();
        }

        public IActionResult SignUp()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Indexlog(Client client)
        {
            var clientBO = new ClientBO(_context);
            var created = clientBO.CreateClient(client);

            return RedirectToAction("Index");
        }




        [HttpPost]
        public IActionResult SignUp( Client client)
        {
            var clientBO = new ClientBO(_context);
            var created = clientBO.CreateClient(client);

            return RedirectToAction("Index");
        }


        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,SecondName,Username,Password,Address,FiscalCode,BadgeCode")] Client client)
        {
            var clientsBO = new ClientBO(_context);
            clientsBO.CreateClient(client);
            return RedirectToAction(nameof(Index));
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,SecondName,Username,Password,Address,FiscalCode,BadgeCode")] Client client)
        {
            bool check = false;
            var clientsBO = new ClientBO(_context);
            check = clientsBO.updateClient(client);

            if (check)
            {
                return View(client);
            }
            else
            {
                return NotFound();
            }

        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool check = false;
            var clientsBO = new ClientBO(_context);
            check = clientsBO.DeleteClient(id);

            if (check)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }

        }
        public async Task<IActionResult> Login(Client client)
        {
            var AuthBO = new AuthenticationBO(_context);
            bool check  = AuthBO.CheckCredentials(client);

            if (check)
                return RedirectToAction("Index", "Books");

            else
                return NotFound();

        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
