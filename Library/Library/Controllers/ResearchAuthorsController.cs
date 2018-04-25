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
    public class ResearchAuthorsController : Controller
    {
        private readonly LibraryContext _context;

        public ResearchAuthorsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: ResearchAuthors
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.ResearchAuthor.Include(r => r.Author).Include(r => r.Research);
            return View(await libraryContext.ToListAsync());
        }

        // GET: ResearchAuthors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchAuthor = await _context.ResearchAuthor
                .Include(r => r.Author)
                .Include(r => r.Research)
                .SingleOrDefaultAsync(m => m.AuthorId == id);
            if (researchAuthor == null)
            {
                return NotFound();
            }

            return View(researchAuthor);
        }

        // GET: ResearchAuthors/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName");
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name");
            return View();
        }

        // POST: ResearchAuthors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,ResearchId")] ResearchAuthor researchAuthor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(researchAuthor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName", researchAuthor.AuthorId);
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name", researchAuthor.ResearchId);
            return View(researchAuthor);
        }

        // GET: ResearchAuthors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchAuthor = await _context.ResearchAuthor.SingleOrDefaultAsync(m => m.AuthorId == id);
            if (researchAuthor == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName", researchAuthor.AuthorId);
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name", researchAuthor.ResearchId);
            return View(researchAuthor);
        }

        // POST: ResearchAuthors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorId,ResearchId")] ResearchAuthor researchAuthor)
        {
            if (id != researchAuthor.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(researchAuthor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResearchAuthorExists(researchAuthor.AuthorId))
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
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName", researchAuthor.AuthorId);
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name", researchAuthor.ResearchId);
            return View(researchAuthor);
        }

        // GET: ResearchAuthors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchAuthor = await _context.ResearchAuthor
                .Include(r => r.Author)
                .Include(r => r.Research)
                .SingleOrDefaultAsync(m => m.AuthorId == id);
            if (researchAuthor == null)
            {
                return NotFound();
            }

            return View(researchAuthor);
        }

        // POST: ResearchAuthors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var researchAuthor = await _context.ResearchAuthor.SingleOrDefaultAsync(m => m.AuthorId == id);
            _context.ResearchAuthor.Remove(researchAuthor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResearchAuthorExists(int id)
        {
            return _context.ResearchAuthor.Any(e => e.AuthorId == id);
        }
    }
}
