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
    public class ResearchBooksController : Controller
    {
        private readonly LibraryContext _context;

        public ResearchBooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: ResearchBooks
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.ResearchBook.Include(r => r.Book).Include(r => r.Research);
            return View(await libraryContext.ToListAsync());
        }

        // GET: ResearchBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchBook = await _context.ResearchBook
                .Include(r => r.Book)
                .Include(r => r.Research)
                .SingleOrDefaultAsync(m => m.ResearchId == id);
            if (researchBook == null)
            {
                return NotFound();
            }

            return View(researchBook);
        }

        // GET: ResearchBooks/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name");
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name");
            return View();
        }

        // POST: ResearchBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResearchId,BookId")] ResearchBook researchBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(researchBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name", researchBook.BookId);
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name", researchBook.ResearchId);
            return View(researchBook);
        }

        // GET: ResearchBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchBook = await _context.ResearchBook.SingleOrDefaultAsync(m => m.ResearchId == id);
            if (researchBook == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name", researchBook.BookId);
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name", researchBook.ResearchId);
            return View(researchBook);
        }

        // POST: ResearchBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResearchId,BookId")] ResearchBook researchBook)
        {
            if (id != researchBook.ResearchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(researchBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResearchBookExists(researchBook.ResearchId))
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
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name", researchBook.BookId);
            ViewData["ResearchId"] = new SelectList(_context.Research, "Id", "Name", researchBook.ResearchId);
            return View(researchBook);
        }

        // GET: ResearchBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchBook = await _context.ResearchBook
                .Include(r => r.Book)
                .Include(r => r.Research)
                .SingleOrDefaultAsync(m => m.ResearchId == id);
            if (researchBook == null)
            {
                return NotFound();
            }

            return View(researchBook);
        }

        // POST: ResearchBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var researchBook = await _context.ResearchBook.SingleOrDefaultAsync(m => m.ResearchId == id);
            _context.ResearchBook.Remove(researchBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResearchBookExists(int id)
        {
            return _context.ResearchBook.Any(e => e.ResearchId == id);
        }
    }
}
