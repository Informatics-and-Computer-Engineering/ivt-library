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
    public class ThemeArticlesController : Controller
    {
        private readonly LibraryContext _context;

        public ThemeArticlesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: ThemeArticles
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.ThemeArticle.Include(t => t.Article).Include(t => t.Theme);
            return View(await libraryContext.ToListAsync());
        }

        // GET: ThemeArticles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themeArticle = await _context.ThemeArticle
                .Include(t => t.Article)
                .Include(t => t.Theme)
                .SingleOrDefaultAsync(m => m.ThemeId == id);
            if (themeArticle == null)
            {
                return NotFound();
            }

            return View(themeArticle);
        }

        // GET: ThemeArticles/Create
        public IActionResult Create()
        {
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name");
            ViewData["ThemeId"] = new SelectList(_context.Theme, "Id", "Name");
            return View();
        }

        // POST: ThemeArticles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThemeId,ArticleId")] ThemeArticle themeArticle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(themeArticle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name", themeArticle.ArticleId);
            ViewData["ThemeId"] = new SelectList(_context.Theme, "Id", "Name", themeArticle.ThemeId);
            return View(themeArticle);
        }

        // GET: ThemeArticles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themeArticle = await _context.ThemeArticle.SingleOrDefaultAsync(m => m.ThemeId == id);
            if (themeArticle == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name", themeArticle.ArticleId);
            ViewData["ThemeId"] = new SelectList(_context.Theme, "Id", "Name", themeArticle.ThemeId);
            return View(themeArticle);
        }

        // POST: ThemeArticles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThemeId,ArticleId")] ThemeArticle themeArticle)
        {
            if (id != themeArticle.ThemeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(themeArticle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThemeArticleExists(themeArticle.ThemeId))
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
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name", themeArticle.ArticleId);
            ViewData["ThemeId"] = new SelectList(_context.Theme, "Id", "Name", themeArticle.ThemeId);
            return View(themeArticle);
        }

        // GET: ThemeArticles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themeArticle = await _context.ThemeArticle
                .Include(t => t.Article)
                .Include(t => t.Theme)
                .SingleOrDefaultAsync(m => m.ThemeId == id);
            if (themeArticle == null)
            {
                return NotFound();
            }

            return View(themeArticle);
        }

        // POST: ThemeArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var themeArticle = await _context.ThemeArticle.SingleOrDefaultAsync(m => m.ThemeId == id);
            _context.ThemeArticle.Remove(themeArticle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThemeArticleExists(int id)
        {
            return _context.ThemeArticle.Any(e => e.ThemeId == id);
        }
    }
}
