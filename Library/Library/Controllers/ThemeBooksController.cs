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
    public class ThemeBooksController : Controller
    {
        private readonly LibraryContext _context;

        public ThemeBooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: ThemeBooks
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.ThemeBook.Include(t => t.Book).Include(t => t.Theme);
            return View(await libraryContext.ToListAsync());
        }

        // GET: ThemeBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themeBook = await _context.ThemeBook
                .Include(t => t.Book)
                .Include(t => t.Theme)
                .SingleOrDefaultAsync(m => m.ThemeId == id);
            if (themeBook == null)
            {
                return NotFound();
            }

            return View(themeBook);
        }

        // GET: ThemeBooks/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name");
            ViewData["ThemeId"] = new SelectList(_context.Theme, "Id", "Name");
            return View();
        }

        // POST: ThemeBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThemeId,BookId")] ThemeBook themeBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(themeBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name", themeBook.BookId);
            ViewData["ThemeId"] = new SelectList(_context.Theme, "Id", "Name", themeBook.ThemeId);
            return View(themeBook);
        }

        // GET: ThemeBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themeBook = await _context.ThemeBook.SingleOrDefaultAsync(m => m.ThemeId == id);
            if (themeBook == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name", themeBook.BookId);
            ViewData["ThemeId"] = new SelectList(_context.Theme, "Id", "Name", themeBook.ThemeId);
            return View(themeBook);
        }

        // POST: ThemeBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThemeId,BookId")] ThemeBook themeBook)
        {
            if (id != themeBook.ThemeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(themeBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThemeBookExists(themeBook.ThemeId))
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
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name", themeBook.BookId);
            ViewData["ThemeId"] = new SelectList(_context.Theme, "Id", "Name", themeBook.ThemeId);
            return View(themeBook);
        }

        // GET: ThemeBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themeBook = await _context.ThemeBook
                .Include(t => t.Book)
                .Include(t => t.Theme)
                .SingleOrDefaultAsync(m => m.ThemeId == id);
            if (themeBook == null)
            {
                return NotFound();
            }

            return View(themeBook);
        }

        // POST: ThemeBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var themeBook = await _context.ThemeBook.SingleOrDefaultAsync(m => m.ThemeId == id);
            _context.ThemeBook.Remove(themeBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThemeBookExists(int id)
        {
            return _context.ThemeBook.Any(e => e.ThemeId == id);
        }
    }
}
