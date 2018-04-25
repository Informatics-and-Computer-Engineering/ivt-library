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
    public class AuthorKeywordsController : Controller
    {
        private readonly LibraryContext _context;

        public AuthorKeywordsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: AuthorKeywords
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.AuthorKeyword.Include(a => a.Author).Include(a => a.Keyword);
            return View(await libraryContext.ToListAsync());
        }

        // GET: AuthorKeywords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorKeyword = await _context.AuthorKeyword
                .Include(a => a.Author)
                .Include(a => a.Keyword)
                .SingleOrDefaultAsync(m => m.AuthorId == id);
            if (authorKeyword == null)
            {
                return NotFound();
            }

            return View(authorKeyword);
        }

        // GET: AuthorKeywords/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName");
            ViewData["KeywordId"] = new SelectList(_context.Keyword, "Id", "Name");
            return View();
        }

        // POST: AuthorKeywords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,KeywordId")] AuthorKeyword authorKeyword)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authorKeyword);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName", authorKeyword.AuthorId);
            ViewData["KeywordId"] = new SelectList(_context.Keyword, "Id", "Name", authorKeyword.KeywordId);
            return View(authorKeyword);
        }

        // GET: AuthorKeywords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorKeyword = await _context.AuthorKeyword.SingleOrDefaultAsync(m => m.AuthorId == id);
            if (authorKeyword == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName", authorKeyword.AuthorId);
            ViewData["KeywordId"] = new SelectList(_context.Keyword, "Id", "Name", authorKeyword.KeywordId);
            return View(authorKeyword);
        }

        // POST: AuthorKeywords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorId,KeywordId")] AuthorKeyword authorKeyword)
        {
            if (id != authorKeyword.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorKeyword);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorKeywordExists(authorKeyword.AuthorId))
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
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName", authorKeyword.AuthorId);
            ViewData["KeywordId"] = new SelectList(_context.Keyword, "Id", "Name", authorKeyword.KeywordId);
            return View(authorKeyword);
        }

        // GET: AuthorKeywords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorKeyword = await _context.AuthorKeyword
                .Include(a => a.Author)
                .Include(a => a.Keyword)
                .SingleOrDefaultAsync(m => m.AuthorId == id);
            if (authorKeyword == null)
            {
                return NotFound();
            }

            return View(authorKeyword);
        }

        // POST: AuthorKeywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorKeyword = await _context.AuthorKeyword.SingleOrDefaultAsync(m => m.AuthorId == id);
            _context.AuthorKeyword.Remove(authorKeyword);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorKeywordExists(int id)
        {
            return _context.AuthorKeyword.Any(e => e.AuthorId == id);
        }
    }
}
