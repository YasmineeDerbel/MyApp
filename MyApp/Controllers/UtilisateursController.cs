using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using IAuthenticationService = MyApp.Models.IAuthenticationService;

namespace MyApp.Controllers
{
    public class UtilisateursController : Controller
    {
        private readonly MyappContext _context;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IAuthenticationService _authenticationService;


        public UtilisateursController(MyappContext context, SignInManager<ApplicationUser> signInManager, IAuthenticationService authenticationService)
        {
            _context = context;
            Console.WriteLine("UtilisateursController Constructor");
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }

        // GET: Utilisateurs
        public async Task<IActionResult> Index()
        {
            var utilisateurs = await _context.Utilisateurs.ToListAsync();

            if (utilisateurs == null || utilisateurs.Count == 0)
            {
                return View(new List<Utilisateur>()); // Return an empty list or handle as needed
            }

            return View(utilisateurs);
        }



        // GET: Utilisateurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Utilisateurs == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(m => m.id == id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // GET: Utilisateurs/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Login()
        {

            return View();

        }

         /*[HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Login(Utilisateur model, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 // Check user credentials against your authentication service
                 bool isValidUser = _authenticationService.ValidateUser(model.username, model.password);


                 if (isValidUser)
                 {
                     // Successful login logic

                     // Check if there is a returnUrl, and redirect there if so
                     if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                     {
                         return Redirect(returnUrl);
                     }

                     // If no returnUrl or returnUrl is not a local URL, redirect to a default action
                     return RedirectToAction("Index", "Home");
                 }

                 ModelState.AddModelError(string.Empty, "Invalid login attempt");
             }

             // If the model is not valid or login fails, return to the login view
             return View(model);
         }*/
        





        // POST: Utilisateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,username,password,email,valide,role")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utilisateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utilisateur);
        }

        // GET: Utilisateurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Utilisateurs == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }
            return View(utilisateur);
        }

        // POST: Utilisateurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,username,password,email,valide,role")] Utilisateur utilisateur)
        {
            if (id != utilisateur.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilisateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilisateurExists(utilisateur.id))
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
            return View(utilisateur);
        }

        // GET: Utilisateurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Utilisateurs == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(m => m.id == id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Utilisateurs == null)
            {
                return Problem("Entity set 'MyAppContext.Utilisateurs'  is null.");
            }
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur != null)
            {
                _context.Utilisateurs.Remove(utilisateur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilisateurExists(int id)
        {
            return (_context.Utilisateurs?.Any(e => e.id == id)).GetValueOrDefault();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                // Handle invalid input
                Console.WriteLine("failed login");
                return RedirectToAction("Login","Utilisateurs"); // Redirect to the login page with an error message
            }

            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.username == username && u.password == password);

            if (utilisateur != null)
            {
                // Successful login, store user information in a session or cookie
                // For simplicity, let's assume you have a session service to store user information

                Console.WriteLine("successful login");

                HttpContext.Session.SetString("Username", utilisateur.username);
                HttpContext.Session.SetString("Userpwd", utilisateur.password.ToString());

                return RedirectToAction("Create", "Posts"); // Redirect to the home page or another appropriate page
            }

            // Failed login
            // You may want to redirect back to the login page with an error message
            Console.WriteLine("failed login");
            return RedirectToAction("Login","Utilisateurs");
        }


        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Utilisateur model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                // Check user credentials against your authentication service
                bool isValidUser = _authenticationService.ValidateUser(model.username, model.password);

                if (isValidUser)
                {
                    // Successful login logic

                    // Check if there is a returnUrl, and redirect there if so
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    // If no returnUrl or returnUrl is not a local URL, redirect to the "Create" action in the "Posts" controller
                    return RedirectToAction("Create", "Posts");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }

            // If the model is not valid or login fails, return to the login view
            return View(model);
        }
        */

    }
}





