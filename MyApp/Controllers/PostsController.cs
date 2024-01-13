using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private readonly MyappContext _context;

        public PostsController(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager,MyappContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
              return _context.Posts != null ? 
                          View(await _context.Posts.ToListAsync()) :
                          Problem("Entity set 'MyappContext.Posts'  is null.");
        }
        
        /*public async Task<IActionResult> Index()
        {
            var posts = await _context.Posts.ToListAsync();
            return View(posts);
        }
        */

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,sujet,auteurid")] Post post)
        {
            
            if (ModelState.IsValid)
            {
                
                    // Retrieve the current user's information
                    var currentUser = await _userManager.GetUserAsync(User);

                    // Check if the user is authenticated
                    if (currentUser == null)
                    {
                        // Handle the case where the user is not authenticated
                        return RedirectToAction("Login", "Utilisateurs"); // Redirect to the login page or handle as needed
                    }

                // Set the auteurid property
                //post.auteurid = currentUser.id;
                post.auteurid = 1;

                    // Add the post to the context
                    _context.Add(post);

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                
            

            return View(post);
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,sujet,auteurid")] Post post)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null)
                {
                    post.auteurid = currentUser.id;
                }
                post.auteurid = 1;

                _context.Add(post);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(post);
        }



        /*public IActionResult Repondre(int id)
        {
            // Assuming you have a service or repository to get the post details
            var post = _context.Posts.Find(id);

            if (post == null)
            {
                // Handle the case where the post is not found, for example, redirect to an error page.
                return RedirectToAction("Error");
            }

            // You can create a new instance of ResponseViewModel or another model that suits your needs
            var responseModel = new Response
            {
                Postid = id
                // Add any other properties you need for creating a response
            };

            return View(responseModel);
        }*/

        public IActionResult Repondre(int postId)
        {
            // Récupérez le post associé à l'ID postId depuis la base de données
            // Post post = _postService.GetPostById(postId);
            var post = _context.Posts.Find(postId);
            if (post != null)
            {
                return View(post);
            }
            // Créez une instance de Response, si nécessaire
            Response response = new Response();

            // Assurez-vous de passer le modèle correct à la vue
            return View();
        }


        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,sujet,reponse")] Post post)
        {
            if (id != post.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.id))
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
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            
            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
       

        /* public IActionResult Repondre(string id)
         {
             // Récupérez le sujet auquel répondre (par exemple, depuis la base de données)
             var sujet = _context.Posts.Include(p => p.reponses).FirstOrDefault(p => p.id == id);

             // Créez une nouvelle réponse et ajoutez-la au sujet
             var nouvelleReponse = new Reponse { contenu = "Votre réponse", auteurid = 1 };
             sujet.reponses.Add(nouvelleReponse);

             // Sauvegardez les changements dans la base de données
             _context.SaveChanges();

             // Redirigez vers l'action "Index" ou toute autre action appropriée
             return RedirectToAction("Index");
         }*/


        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'MyappContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
          return (_context.Posts?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
