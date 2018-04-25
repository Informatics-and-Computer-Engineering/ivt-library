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
    public class FileArticlesController : Controller
    {
        private readonly LibraryContext _context;

        public FileArticlesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: FileArticles
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.FileArticle.Include(f => f.Article).Include(f => f.Type);
            return View(await libraryContext.ToListAsync());
        }

        // GET: FileArticles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileArticle = await _context.FileArticle
                .Include(f => f.Article)
                .Include(f => f.Type)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (fileArticle == null)
            {
                return NotFound();
            }

            return View(fileArticle);
        }

        // GET: FileArticles/Create
        public IActionResult Create()
        {
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name");
            ViewData["TypeId"] = new SelectList(_context.FileType, "Id", "Name");
            return View();
        }

        // POST: FileArticles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ContentType,Data,TypeId,Version,ArticleId")] FileArticle fileArticle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fileArticle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name", fileArticle.ArticleId);
            ViewData["TypeId"] = new SelectList(_context.FileType, "Id", "Name", fileArticle.TypeId);
            return View(fileArticle);
        }

        // GET: FileArticles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileArticle = await _context.FileArticle.SingleOrDefaultAsync(m => m.Id == id);
            if (fileArticle == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name", fileArticle.ArticleId);
            ViewData["TypeId"] = new SelectList(_context.FileType, "Id", "Name", fileArticle.TypeId);
            return View(fileArticle);
        }

        // POST: FileArticles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ContentType,Data,TypeId,Version,ArticleId")] FileArticle fileArticle)
        {
            if (id != fileArticle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileArticle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileArticleExists(fileArticle.Id))
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
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name", fileArticle.ArticleId);
            ViewData["TypeId"] = new SelectList(_context.FileType, "Id", "Name", fileArticle.TypeId);
            return View(fileArticle);
        }

        // GET: FileArticles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileArticle = await _context.FileArticle
                .Include(f => f.Article)
                .Include(f => f.Type)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (fileArticle == null)
            {
                return NotFound();
            }

            return View(fileArticle);
        }

        // POST: FileArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fileArticle = await _context.FileArticle.SingleOrDefaultAsync(m => m.Id == id);
            _context.FileArticle.Remove(fileArticle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileArticleExists(int id)
        {
            return _context.FileArticle.Any(e => e.Id == id);
        }
    }
}
