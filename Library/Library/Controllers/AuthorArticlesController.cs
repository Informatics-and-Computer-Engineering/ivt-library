using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Controllers
{
    public class AuthorArticlesController : Controller
    {
        private readonly LibraryContext _context;

        public AuthorArticlesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: AuthorArticles
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.AuthorArticle.Include(a => a.Article).Include(a => a.Author);
            return View(await libraryContext.ToListAsync());
        }

        // GET: AuthorArticles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorArticle = await _context.AuthorArticle
                .Include(a => a.Article)
                .Include(a => a.Author)
                .SingleOrDefaultAsync(m => m.AuthorId == id);
            if (authorArticle == null)
            {
                return NotFound();
            }

            return View(authorArticle);
        }

        // GET: AuthorArticles/Create
        public IActionResult Create()
        {
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name");
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName");
            return View();
        }

        // POST: AuthorArticles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,ArticleId")] AuthorArticle authorArticle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authorArticle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name", authorArticle.ArticleId);
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName", authorArticle.AuthorId);
            return View(authorArticle);
        }

        // GET: AuthorArticles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorArticle = await _context.AuthorArticle.SingleOrDefaultAsync(m => m.AuthorId == id);
            if (authorArticle == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name", authorArticle.ArticleId);
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName", authorArticle.AuthorId);
            return View(authorArticle);
        }

        // POST: AuthorArticles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorId,ArticleId")] AuthorArticle authorArticle)
        {
            if (id != authorArticle.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorArticle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorArticleExists(authorArticle.AuthorId))
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
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name", authorArticle.ArticleId);
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName", authorArticle.AuthorId);
            return View(authorArticle);
        }

        // GET: AuthorArticles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorArticle = await _context.AuthorArticle
                .Include(a => a.Article)
                .Include(a => a.Author)
                .SingleOrDefaultAsync(m => m.AuthorId == id);
            if (authorArticle == null)
            {
                return NotFound();
            }

            return View(authorArticle);
        }

        // POST: AuthorArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorArticle = await _context.AuthorArticle.SingleOrDefaultAsync(m => m.AuthorId == id);
            _context.AuthorArticle.Remove(authorArticle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorArticleExists(int id)
        {
            return _context.AuthorArticle.Any(e => e.AuthorId == id);
        }
    }
}
