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
    public class ThemeAuthorsController : Controller
    {
        private readonly LibraryContext _context;

        public ThemeAuthorsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: ThemeAuthors
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.ThemeAuthor.Include(t => t.Author).Include(t => t.Theme);
            return View(await libraryContext.ToListAsync());
        }

        // GET: ThemeAuthors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themeAuthor = await _context.ThemeAuthor
                .Include(t => t.Author)
                .Include(t => t.Theme)
                .SingleOrDefaultAsync(m => m.ThemeId == id);
            if (themeAuthor == null)
            {
                return NotFound();
            }

            return View(themeAuthor);
        }

        // GET: ThemeAuthors/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName");
            ViewData["ThemeId"] = new SelectList(_context.Theme, "Id", "Name");
            return View();
        }

        // POST: ThemeAuthors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThemeId,AuthorId")] ThemeAuthor themeAuthor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(themeAuthor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName", themeAuthor.AuthorId);
            ViewData["ThemeId"] = new SelectList(_context.Theme, "Id", "Name", themeAuthor.ThemeId);
            return View(themeAuthor);
        }

        // GET: ThemeAuthors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themeAuthor = await _context.ThemeAuthor.SingleOrDefaultAsync(m => m.ThemeId == id);
            if (themeAuthor == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName", themeAuthor.AuthorId);
            ViewData["ThemeId"] = new SelectList(_context.Theme, "Id", "Name", themeAuthor.ThemeId);
            return View(themeAuthor);
        }

        // POST: ThemeAuthors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThemeId,AuthorId")] ThemeAuthor themeAuthor)
        {
            if (id != themeAuthor.ThemeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(themeAuthor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThemeAuthorExists(themeAuthor.ThemeId))
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
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName", themeAuthor.AuthorId);
            ViewData["ThemeId"] = new SelectList(_context.Theme, "Id", "Name", themeAuthor.ThemeId);
            return View(themeAuthor);
        }

        // GET: ThemeAuthors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themeAuthor = await _context.ThemeAuthor
                .Include(t => t.Author)
                .Include(t => t.Theme)
                .SingleOrDefaultAsync(m => m.ThemeId == id);
            if (themeAuthor == null)
            {
                return NotFound();
            }

            return View(themeAuthor);
        }

        // POST: ThemeAuthors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var themeAuthor = await _context.ThemeAuthor.SingleOrDefaultAsync(m => m.ThemeId == id);
            _context.ThemeAuthor.Remove(themeAuthor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThemeAuthorExists(int id)
        {
            return _context.ThemeAuthor.Any(e => e.ThemeId == id);
        }
    }
}
