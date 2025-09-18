using library_system.Business;
using library_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using NuGet.Protocol;


namespace library_system.Controllers
{
    public class ClientsController : Controller
    {
        private readonly AuthenticationBO _authBO;
        private readonly ClientBO _clientBO;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;


        public ClientsController(AuthenticationBO authBO, ClientBO clientBO, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _authBO = authBO;
            _clientBO = clientBO;
            _signInManager = signInManager;
            _userManager = userManager;
        }





        public IActionResult Indexlog()
        {
            // Check if token exists in session
            var token = HttpContext.Session.GetString("UserToken");

            if (!string.IsNullOrEmpty(token))
            {
                // Token exists → redirect to Home
                return RedirectToAction("Index", "Clients");
            }

            // No token → show login page
            return View();
        }        

        public IActionResult SignUp()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Indexlog(Client client)
        {
            var created = _authBO.CheckCredentials(client);
            var t = Encrypt(client.Password);
            var result = await _signInManager.PasswordSignInAsync(client.Username, client.Password, client.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded && created)
            {

                // Create a token
                string token = Guid.NewGuid().ToString();

                // Store in session
                HttpContext.Session.SetString("UserToken", token);

                // Optionally store username in session too
                HttpContext.Session.SetString("Username", client.Username);

                return RedirectToAction("Index", "Clients");

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return RedirectToAction("Indexlog");
            }

            //if (created)
            //    return RedirectToAction("Index");

            //else
            //    return RedirectToAction("Indexlog");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SignUp( Client client)
        {
            var created = _clientBO.CreateClient(client);


                var user = new IdentityUser { UserName = client.Username, Email = client.FirstName };

            // Use the UserManager to create a new user with the given password
            //client.Password = Encrypt(client.Password);
                var result = await _userManager.CreateAsync(user, client.Password);

            if (result.Succeeded)
            {
                // Optional: Automatically sign in the user after they register
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index");
            }

                // If creation fails, add errors to the ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);

                }
                    return NotFound();
            
            


        }


        // GET: Clients
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("UserToken");

            if (string.IsNullOrEmpty(token))
            {
                // Not logged in, redirect to login
                return RedirectToAction("Indexlog", "Clients");
            }

            // Otherwise, user is logged in

            return View(await _clientBO.getAllClients().ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientBO.getClientByID((int) id)
                .FirstOrDefaultAsync();
            if (client == null)
            {
                return NotFound();
            }
            client.Password = Decrypt(client.Password);


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
            _clientBO.CreateClient(client);
            return RedirectToAction(nameof(Index));
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var client = await _clientBO.getClientByID((int) id).FirstOrDefaultAsync();
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
            check = _clientBO.updateClient(client);

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

            var client = await _clientBO.getClientByID((int) id)
                .FirstOrDefaultAsync();
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
            check = _clientBO.DeleteClient(id);

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
            bool check  = _authBO.CheckCredentials(client);

            if (check)
                return RedirectToAction("Index", "Books");

            else
                return NotFound();

        }

        private bool ClientExists(int id)
        {
            return _clientBO.getClientByID(id).FirstOrDefault() != null;
        }

        public static class Global
        {
            // set password
            public const string strPassword = "LetMeIn99$";

            // set permutations
            public const String strPermutation = "ouiveyxaqtd";
            public const Int32 bytePermutation1 = 0x19;
            public const Int32 bytePermutation2 = 0x59;
            public const Int32 bytePermutation3 = 0x17;
            public const Int32 bytePermutation4 = 0x41;
        }

        public static string Encrypt(string strData)
        {

            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(strData)));
            // reference https://msdn.microsoft.com/en-us/library/ds4kkd55(v=vs.110).aspx

        }




        // decoding
        public static string Decrypt(string strData)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(strData)));
            // reference https://msdn.microsoft.com/en-us/library/system.convert.frombase64string(v=vs.110).aspx

        }

        // encrypt
        public static byte[] Encrypt(byte[] strData)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(Global.strPermutation,
            new byte[] { Global.bytePermutation1,
                         Global.bytePermutation2,
                         Global.bytePermutation3,
                         Global.bytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);


            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }

        // decrypt
        public static byte[] Decrypt(byte[] strData)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(Global.strPermutation,
            new byte[] { Global.bytePermutation1,
                         Global.bytePermutation2,
                         Global.bytePermutation3,
                         Global.bytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }
    }
}
