using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class ResponsesController : Controller
    {
        private readonly MyappContext _context;

        public ResponsesController(MyappContext context)
        {
            _context = context;
        }
        // GET: ResponsesController
        
        
            

            // GET: Responses
            public async Task<IActionResult> Index()
            {
                var postsWithResponses = await _context.Posts
                    .Include(p => p.Responses)
                    .ToListAsync();

                return View(postsWithResponses);
            }

        public IActionResult Create(int postId)
        {
            ViewData["PostId"] = postId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,contenu,auteurId,postId")] Response response)
        {
            if (ModelState.IsValid)
            {
                _context.Add(response);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(response);
        }



        // GET: ResponsesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ResponsesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResponsesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ResponsesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ResponsesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ResponsesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ResponsesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
