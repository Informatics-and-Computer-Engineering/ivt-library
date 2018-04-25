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
    public class DisciplineAuthorsController : Controller
    {
        private readonly LibraryContext _context;

        public DisciplineAuthorsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: DisciplineAuthors
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.DisciplineAuthor.Include(d => d.Author).Include(d => d.Discipline);
            return View(await libraryContext.ToListAsync());
        }

        // GET: DisciplineAuthors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplineAuthor = await _context.DisciplineAuthor
                .Include(d => d.Author)
                .Include(d => d.Discipline)
                .SingleOrDefaultAsync(m => m.DisciplineId == id);
            if (disciplineAuthor == null)
            {
                return NotFound();
            }

            return View(disciplineAuthor);
        }

        // GET: DisciplineAuthors/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName");
            ViewData["DisciplineId"] = new SelectList(_context.Discipline, "Id", "Name");
            return View();
        }

        // POST: DisciplineAuthors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisciplineId,AuthorId")] DisciplineAuthor disciplineAuthor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disciplineAuthor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName", disciplineAuthor.AuthorId);
            ViewData["DisciplineId"] = new SelectList(_context.Discipline, "Id", "Name", disciplineAuthor.DisciplineId);
            return View(disciplineAuthor);
        }

        // GET: DisciplineAuthors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplineAuthor = await _context.DisciplineAuthor.SingleOrDefaultAsync(m => m.DisciplineId == id);
            if (disciplineAuthor == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName", disciplineAuthor.AuthorId);
            ViewData["DisciplineId"] = new SelectList(_context.Discipline, "Id", "Name", disciplineAuthor.DisciplineId);
            return View(disciplineAuthor);
        }

        // POST: DisciplineAuthors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DisciplineId,AuthorId")] DisciplineAuthor disciplineAuthor)
        {
            if (id != disciplineAuthor.DisciplineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disciplineAuthor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplineAuthorExists(disciplineAuthor.DisciplineId))
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
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName", disciplineAuthor.AuthorId);
            ViewData["DisciplineId"] = new SelectList(_context.Discipline, "Id", "Name", disciplineAuthor.DisciplineId);
            return View(disciplineAuthor);
        }

        // GET: DisciplineAuthors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplineAuthor = await _context.DisciplineAuthor
                .Include(d => d.Author)
                .Include(d => d.Discipline)
                .SingleOrDefaultAsync(m => m.DisciplineId == id);
            if (disciplineAuthor == null)
            {
                return NotFound();
            }

            return View(disciplineAuthor);
        }

        // POST: DisciplineAuthors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplineAuthor = await _context.DisciplineAuthor.SingleOrDefaultAsync(m => m.DisciplineId == id);
            _context.DisciplineAuthor.Remove(disciplineAuthor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisciplineAuthorExists(int id)
        {
            return _context.DisciplineAuthor.Any(e => e.DisciplineId == id);
        }
    }
}
