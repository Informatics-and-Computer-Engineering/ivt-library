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
    public class ArticleArticlesController : Controller
    {
        private readonly LibraryContext _context;

        public ArticleArticlesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: ArticleArticles
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.ArticleArticle.Include(a => a.HostArticle).Include(a => a.ReferencedArticle);
            return View(await libraryContext.ToListAsync());
        }

        // GET: ArticleArticles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleArticle = await _context.ArticleArticle
                .Include(a => a.HostArticle)
                .Include(a => a.ReferencedArticle)
                .SingleOrDefaultAsync(m => m.HostArticleId == id);
            if (articleArticle == null)
            {
                return NotFound();
            }

            return View(articleArticle);
        }

        // GET: ArticleArticles/Create
        public IActionResult Create()
        {
            ViewData["HostArticleId"] = new SelectList(_context.Article, "Id", "Name");
            ViewData["ReferencedArticleId"] = new SelectList(_context.Article, "Id", "Name");
            return View();
        }

        // POST: ArticleArticles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HostArticleId,ReferencedArticleId")] ArticleArticle articleArticle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articleArticle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HostArticleId"] = new SelectList(_context.Article, "Id", "Name", articleArticle.HostArticleId);
            ViewData["ReferencedArticleId"] = new SelectList(_context.Article, "Id", "Name", articleArticle.ReferencedArticleId);
            return View(articleArticle);
        }

        // GET: ArticleArticles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleArticle = await _context.ArticleArticle.SingleOrDefaultAsync(m => m.HostArticleId == id);
            if (articleArticle == null)
            {
                return NotFound();
            }
            ViewData["HostArticleId"] = new SelectList(_context.Article, "Id", "Name", articleArticle.HostArticleId);
            ViewData["ReferencedArticleId"] = new SelectList(_context.Article, "Id", "Name", articleArticle.ReferencedArticleId);
            return View(articleArticle);
        }

        // POST: ArticleArticles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HostArticleId,ReferencedArticleId")] ArticleArticle articleArticle)
        {
            if (id != articleArticle.HostArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articleArticle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleArticleExists(articleArticle.HostArticleId))
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
            ViewData["HostArticleId"] = new SelectList(_context.Article, "Id", "Name", articleArticle.HostArticleId);
            ViewData["ReferencedArticleId"] = new SelectList(_context.Article, "Id", "Name", articleArticle.ReferencedArticleId);
            return View(articleArticle);
        }

        // GET: ArticleArticles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleArticle = await _context.ArticleArticle
                .Include(a => a.HostArticle)
                .Include(a => a.ReferencedArticle)
                .SingleOrDefaultAsync(m => m.HostArticleId == id);
            if (articleArticle == null)
            {
                return NotFound();
            }

            return View(articleArticle);
        }

        // POST: ArticleArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articleArticle = await _context.ArticleArticle.SingleOrDefaultAsync(m => m.HostArticleId == id);
            _context.ArticleArticle.Remove(articleArticle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleArticleExists(int id)
        {
            return _context.ArticleArticle.Any(e => e.HostArticleId == id);
        }
    }
}
