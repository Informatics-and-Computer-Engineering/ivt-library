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
    public class KeywordsController : Controller
    {
        private readonly LibraryContext _context;

        public KeywordsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Keywords
        public async Task<IActionResult> Index()
        {
            return View(await _context.Keyword.ToListAsync());
        }

        // GET: Keywords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keyword = await _context.Keyword
                .SingleOrDefaultAsync(m => m.Id == id);
            if (keyword == null)
            {
                return NotFound();
            }

            return View(keyword);
        }

        // GET: Keywords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Keywords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Keyword keyword)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keyword);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(keyword);
        }

        // GET: Keywords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keyword = await _context.Keyword.SingleOrDefaultAsync(m => m.Id == id);
            if (keyword == null)
            {
                return NotFound();
            }
            return View(keyword);
        }

        // POST: Keywords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Keyword keyword)
        {
            if (id != keyword.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keyword);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeywordExists(keyword.Id))
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
            return View(keyword);
        }

        // GET: Keywords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keyword = await _context.Keyword
                .SingleOrDefaultAsync(m => m.Id == id);
            if (keyword == null)
            {
                return NotFound();
            }

            return View(keyword);
        }

        // POST: Keywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var keyword = await _context.Keyword.SingleOrDefaultAsync(m => m.Id == id);
            _context.Keyword.Remove(keyword);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeywordExists(int id)
        {
            return _context.Keyword.Any(e => e.Id == id);
        }
    }
}
