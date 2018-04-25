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
    public class FileResearchesController : Controller
    {
        private readonly LibraryContext _context;

        public FileResearchesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: FileResearches
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.FileResearch.Include(f => f.Research).Include(f => f.Type);
            return View(await libraryContext.ToListAsync());
        }

        // GET: FileResearches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileResearch = await _context.FileResearch
                .Include(f => f.Research)
                .Include(f => f.Type)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (fileResearch == null)
            {
                return NotFound();
            }

            return View(fileResearch);
        }

        // GET: FileResearches/Create
        public IActionResult Create()
        {
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name");
            ViewData["TypeId"] = new SelectList(_context.FileType, "Id", "Name");
            return View();
        }

        // POST: FileResearches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ContentType,Data,TypeId,Version,ResearchId")] FileResearch fileResearch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fileResearch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name", fileResearch.ResearchId);
            ViewData["TypeId"] = new SelectList(_context.FileType, "Id", "Name", fileResearch.TypeId);
            return View(fileResearch);
        }

        // GET: FileResearches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileResearch = await _context.FileResearch.SingleOrDefaultAsync(m => m.Id == id);
            if (fileResearch == null)
            {
                return NotFound();
            }
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name", fileResearch.ResearchId);
            ViewData["TypeId"] = new SelectList(_context.FileType, "Id", "Name", fileResearch.TypeId);
            return View(fileResearch);
        }

        // POST: FileResearches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ContentType,Data,TypeId,Version,ResearchId")] FileResearch fileResearch)
        {
            if (id != fileResearch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileResearch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileResearchExists(fileResearch.Id))
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
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name", fileResearch.ResearchId);
            ViewData["TypeId"] = new SelectList(_context.FileType, "Id", "Name", fileResearch.TypeId);
            return View(fileResearch);
        }

        // GET: FileResearches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileResearch = await _context.FileResearch
                .Include(f => f.Research)
                .Include(f => f.Type)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (fileResearch == null)
            {
                return NotFound();
            }

            return View(fileResearch);
        }

        // POST: FileResearches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fileResearch = await _context.FileResearch.SingleOrDefaultAsync(m => m.Id == id);
            _context.FileResearch.Remove(fileResearch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileResearchExists(int id)
        {
            return _context.FileResearch.Any(e => e.Id == id);
        }
    }
}
