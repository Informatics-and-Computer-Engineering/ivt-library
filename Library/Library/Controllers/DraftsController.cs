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
    public class DraftsController : Controller
    {
        private readonly LibraryContext _context;

        public DraftsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Drafts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Draft.ToListAsync());
        }

        // GET: Drafts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var draft = await _context.Draft
                .SingleOrDefaultAsync(m => m.Id == id);
            if (draft == null)
            {
                return NotFound();
            }

            return View(draft);
        }

        // GET: Drafts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drafts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,CreationDate")] Draft draft)
        {
            if (ModelState.IsValid)
            {
                _context.Add(draft);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(draft);
        }

        // GET: Drafts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var draft = await _context.Draft.SingleOrDefaultAsync(m => m.Id == id);
            if (draft == null)
            {
                return NotFound();
            }
            return View(draft);
        }

        // POST: Drafts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,Content,CreationDate")] Draft draft)
        {
            if (id != draft.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(draft);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DraftExists(draft.Id))
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
            return View(draft);
        }

        // GET: Drafts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var draft = await _context.Draft
                .SingleOrDefaultAsync(m => m.Id == id);
            if (draft == null)
            {
                return NotFound();
            }

            return View(draft);
        }

        // POST: Drafts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var draft = await _context.Draft.SingleOrDefaultAsync(m => m.Id == id);
            _context.Draft.Remove(draft);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DraftExists(long id)
        {
            return _context.Draft.Any(e => e.Id == id);
        }
    }
}
