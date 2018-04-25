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
    public class ResearchThemesController : Controller
    {
        private readonly LibraryContext _context;

        public ResearchThemesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: ResearchThemes
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.ResearchTheme.Include(r => r.Research).Include(r => r.Theme);
            return View(await libraryContext.ToListAsync());
        }

        // GET: ResearchThemes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchTheme = await _context.ResearchTheme
                .Include(r => r.Research)
                .Include(r => r.Theme)
                .SingleOrDefaultAsync(m => m.ResearchId == id);
            if (researchTheme == null)
            {
                return NotFound();
            }

            return View(researchTheme);
        }

        // GET: ResearchThemes/Create
        public IActionResult Create()
        {
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name");
            ViewData["ThemeId"] = new SelectList(_context.Theme, "Id", "Name");
            return View();
        }

        // POST: ResearchThemes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResearchId,ThemeId")] ResearchTheme researchTheme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(researchTheme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name", researchTheme.ResearchId);
            ViewData["ThemeId"] = new SelectList(_context.Theme, "Id", "Name", researchTheme.ThemeId);
            return View(researchTheme);
        }

        // GET: ResearchThemes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchTheme = await _context.ResearchTheme.SingleOrDefaultAsync(m => m.ResearchId == id);
            if (researchTheme == null)
            {
                return NotFound();
            }
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name", researchTheme.ResearchId);
            ViewData["ThemeId"] = new SelectList(_context.Theme, "Id", "Name", researchTheme.ThemeId);
            return View(researchTheme);
        }

        // POST: ResearchThemes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResearchId,ThemeId")] ResearchTheme researchTheme)
        {
            if (id != researchTheme.ResearchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(researchTheme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResearchThemeExists(researchTheme.ResearchId))
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
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name", researchTheme.ResearchId);
            ViewData["ThemeId"] = new SelectList(_context.Theme, "Id", "Name", researchTheme.ThemeId);
            return View(researchTheme);
        }

        // GET: ResearchThemes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchTheme = await _context.ResearchTheme
                .Include(r => r.Research)
                .Include(r => r.Theme)
                .SingleOrDefaultAsync(m => m.ResearchId == id);
            if (researchTheme == null)
            {
                return NotFound();
            }

            return View(researchTheme);
        }

        // POST: ResearchThemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var researchTheme = await _context.ResearchTheme.SingleOrDefaultAsync(m => m.ResearchId == id);
            _context.ResearchTheme.Remove(researchTheme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResearchThemeExists(int id)
        {
            return _context.ResearchTheme.Any(e => e.ResearchId == id);
        }
    }
}
