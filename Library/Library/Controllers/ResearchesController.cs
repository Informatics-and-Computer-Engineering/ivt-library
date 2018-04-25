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
    public class ResearchesController : Controller
    {
        private readonly LibraryContext _context;

        public ResearchesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Researches
        public async Task<IActionResult> Index()
        {
            return View(await _context.Research.ToListAsync());
        }

        // GET: Researches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var research = await _context.Research
                .SingleOrDefaultAsync(m => m.Id == id);
            if (research == null)
            {
                return NotFound();
            }

            return View(research);
        }

        // GET: Researches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Researches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Goal,Tasks,Progress")] Research research)
        {
            if (ModelState.IsValid)
            {
                _context.Add(research);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(research);
        }

        // GET: Researches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var research = await _context.Research.SingleOrDefaultAsync(m => m.Id == id);
            if (research == null)
            {
                return NotFound();
            }
            return View(research);
        }

        // POST: Researches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Goal,Tasks,Progress")] Research research)
        {
            if (id != research.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(research);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResearchExists(research.Id))
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
            return View(research);
        }

        // GET: Researches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var research = await _context.Research
                .SingleOrDefaultAsync(m => m.Id == id);
            if (research == null)
            {
                return NotFound();
            }

            return View(research);
        }

        // POST: Researches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var research = await _context.Research.SingleOrDefaultAsync(m => m.Id == id);
            _context.Research.Remove(research);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResearchExists(int id)
        {
            return _context.Research.Any(e => e.Id == id);
        }
    }
}
