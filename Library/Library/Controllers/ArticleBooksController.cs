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
    public class ArticleBooksController : Controller
    {
        private readonly LibraryContext _context;

        public ArticleBooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: ArticleBooks
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.ArticleBook.Include(a => a.Article).Include(a => a.Book);
            return View(await libraryContext.ToListAsync());
        }

        // GET: ArticleBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleBook = await _context.ArticleBook
                .Include(a => a.Article)
                .Include(a => a.Book)
                .SingleOrDefaultAsync(m => m.ArticleId == id);
            if (articleBook == null)
            {
                return NotFound();
            }

            return View(articleBook);
        }

        // GET: ArticleBooks/Create
        public IActionResult Create()
        {
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name");
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name");
            return View();
        }

        // POST: ArticleBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleId,BookId")] ArticleBook articleBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articleBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name", articleBook.ArticleId);
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name", articleBook.BookId);
            return View(articleBook);
        }

        // GET: ArticleBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleBook = await _context.ArticleBook.SingleOrDefaultAsync(m => m.ArticleId == id);
            if (articleBook == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name", articleBook.ArticleId);
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name", articleBook.BookId);
            return View(articleBook);
        }

        // POST: ArticleBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,BookId")] ArticleBook articleBook)
        {
            if (id != articleBook.ArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articleBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleBookExists(articleBook.ArticleId))
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
            ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "Name", articleBook.ArticleId);
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name", articleBook.BookId);
            return View(articleBook);
        }

        // GET: ArticleBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleBook = await _context.ArticleBook
                .Include(a => a.Article)
                .Include(a => a.Book)
                .SingleOrDefaultAsync(m => m.ArticleId == id);
            if (articleBook == null)
            {
                return NotFound();
            }

            return View(articleBook);
        }

        // POST: ArticleBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articleBook = await _context.ArticleBook.SingleOrDefaultAsync(m => m.ArticleId == id);
            _context.ArticleBook.Remove(articleBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleBookExists(int id)
        {
            return _context.ArticleBook.Any(e => e.ArticleId == id);
        }
    }
}
