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
    public class ScalesController : Controller
    {
        private readonly LibraryContext _context;

        public ScalesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Scales
        public async Task<IActionResult> Index()
        {
            return View(await _context.Scale.ToListAsync());
        }

        // GET: Scales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scale = await _context.Scale
                .SingleOrDefaultAsync(m => m.Id == id);
            if (scale == null)
            {
                return NotFound();
            }

            return View(scale);
        }

        // GET: Scales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Scales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Scale scale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scale);
        }

        // GET: Scales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scale = await _context.Scale.SingleOrDefaultAsync(m => m.Id == id);
            if (scale == null)
            {
                return NotFound();
            }
            return View(scale);
        }

        // POST: Scales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Scale scale)
        {
            if (id != scale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScaleExists(scale.Id))
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
            return View(scale);
        }

        // GET: Scales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scale = await _context.Scale
                .SingleOrDefaultAsync(m => m.Id == id);
            if (scale == null)
            {
                return NotFound();
            }

            return View(scale);
        }

        // POST: Scales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scale = await _context.Scale.SingleOrDefaultAsync(m => m.Id == id);
            _context.Scale.Remove(scale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScaleExists(int id)
        {
            return _context.Scale.Any(e => e.Id == id);
        }
    }
}
