﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;
using Microsoft.AspNetCore.Http;

namespace MyApp.Controllers
{
    public class UtilisateursController : Controller
    {
        private readonly MyappContext _context;

        public UtilisateursController(MyappContext context)
        {
            _context = context;
            Console.WriteLine("UtilisateursController Constructor");
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
                return RedirectToAction("Login"); // Redirect to the login page with an error message
            }

            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.username == username && u.password == password);

            if (utilisateur != null)
            {
                // Successful login, store user information in a session or cookie
                // For simplicity, let's assume you have a session service to store user information
                

               
                HttpContext.Session.SetString("Username", utilisateur.username);
                HttpContext.Session.SetString("Userpwd", utilisateur.password.ToString());

                return RedirectToAction("Index", "Home"); // Redirect to the home page or another appropriate page
            }

            // Failed login
            // You may want to redirect back to the login page with an error message
            return RedirectToAction("Login");
        }

    }
}




