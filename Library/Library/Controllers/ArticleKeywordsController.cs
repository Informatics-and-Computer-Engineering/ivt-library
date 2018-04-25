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
    public class ArticleKeywordsController : Controller
    {
        private readonly LibraryContext _context;

        public ArticleKeywordsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: ArticleKeywords
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.ArticleKeyword.Include(a => a.Article).Include(a => a.Keyword);
            return View(await libraryContext.ToListAsync());
        }

        // GET: ArticleKeywords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleKeyword = await _context.ArticleKeyword
                .Include(a => a.Article)
                .Include(a => a.Keyword)
                .SingleOrDefaultAsync(m => m.ArticleId == id);
            if (articleKeyword == null)
            {
                return NotFound();
            }

            return View(articleKeyword);
        }

        // GET: ArticleKeywords/Create
        public IActionResult Create()
        {
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name");
            ViewData["KeywordId"] = new SelectList(_context.Keyword, "Id", "Name");
            return View();
        }

        // POST: ArticleKeywords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleId,KeywordId")] ArticleKeyword articleKeyword)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articleKeyword);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name", articleKeyword.ArticleId);
            ViewData["KeywordId"] = new SelectList(_context.Keyword, "Id", "Name", articleKeyword.KeywordId);
            return View(articleKeyword);
        }

        // GET: ArticleKeywords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleKeyword = await _context.ArticleKeyword.SingleOrDefaultAsync(m => m.ArticleId == id);
            if (articleKeyword == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name", articleKeyword.ArticleId);
            ViewData["KeywordId"] = new SelectList(_context.Keyword, "Id", "Name", articleKeyword.KeywordId);
            return View(articleKeyword);
        }

        // POST: ArticleKeywords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,KeywordId")] ArticleKeyword articleKeyword)
        {
            if (id != articleKeyword.ArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articleKeyword);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleKeywordExists(articleKeyword.ArticleId))
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
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name", articleKeyword.ArticleId);
            ViewData["KeywordId"] = new SelectList(_context.Keyword, "Id", "Name", articleKeyword.KeywordId);
            return View(articleKeyword);
        }

        // GET: ArticleKeywords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleKeyword = await _context.ArticleKeyword
                .Include(a => a.Article)
                .Include(a => a.Keyword)
                .SingleOrDefaultAsync(m => m.ArticleId == id);
            if (articleKeyword == null)
            {
                return NotFound();
            }

            return View(articleKeyword);
        }

        // POST: ArticleKeywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articleKeyword = await _context.ArticleKeyword.SingleOrDefaultAsync(m => m.ArticleId == id);
            _context.ArticleKeyword.Remove(articleKeyword);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleKeywordExists(int id)
        {
            return _context.ArticleKeyword.Any(e => e.ArticleId == id);
        }
    }
}
