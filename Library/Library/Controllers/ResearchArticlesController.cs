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
    public class ResearchArticlesController : Controller
    {
        private readonly LibraryContext _context;

        public ResearchArticlesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: ResearchArticles
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.ResearchArticle.Include(r => r.Article).Include(r => r.Research);
            return View(await libraryContext.ToListAsync());
        }

        // GET: ResearchArticles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchArticle = await _context.ResearchArticle
                .Include(r => r.Article)
                .Include(r => r.Research)
                .SingleOrDefaultAsync(m => m.ResearchId == id);
            if (researchArticle == null)
            {
                return NotFound();
            }

            return View(researchArticle);
        }

        // GET: ResearchArticles/Create
        public IActionResult Create()
        {
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name");
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name");
            return View();
        }

        // POST: ResearchArticles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResearchId,ArticleId")] ResearchArticle researchArticle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(researchArticle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name", researchArticle.ArticleId);
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name", researchArticle.ResearchId);
            return View(researchArticle);
        }

        // GET: ResearchArticles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchArticle = await _context.ResearchArticle.SingleOrDefaultAsync(m => m.ResearchId == id);
            if (researchArticle == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name", researchArticle.ArticleId);
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name", researchArticle.ResearchId);
            return View(researchArticle);
        }

        // POST: ResearchArticles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResearchId,ArticleId")] ResearchArticle researchArticle)
        {
            if (id != researchArticle.ResearchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(researchArticle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResearchArticleExists(researchArticle.ResearchId))
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
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name", researchArticle.ArticleId);
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name", researchArticle.ResearchId);
            return View(researchArticle);
        }

        // GET: ResearchArticles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchArticle = await _context.ResearchArticle
                .Include(r => r.Article)
                .Include(r => r.Research)
                .SingleOrDefaultAsync(m => m.ResearchId == id);
            if (researchArticle == null)
            {
                return NotFound();
            }

            return View(researchArticle);
        }

        // POST: ResearchArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var researchArticle = await _context.ResearchArticle.SingleOrDefaultAsync(m => m.ResearchId == id);
            _context.ResearchArticle.Remove(researchArticle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResearchArticleExists(int id)
        {
            return _context.ResearchArticle.Any(e => e.ResearchId == id);
        }
    }
}
