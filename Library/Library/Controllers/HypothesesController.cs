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
    public class HypothesesController : Controller
    {
        private readonly LibraryContext _context;

        public HypothesesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Hypotheses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hypothesis.ToListAsync());
        }

        // GET: Hypotheses/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hypothesis = await _context.Hypothesis
                .SingleOrDefaultAsync(m => m.Id == id);
            if (hypothesis == null)
            {
                return NotFound();
            }

            return View(hypothesis);
        }

        // GET: Hypotheses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hypotheses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Content,Explanation")] Hypothesis hypothesis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hypothesis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hypothesis);
        }

        // GET: Hypotheses/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hypothesis = await _context.Hypothesis.SingleOrDefaultAsync(m => m.Id == id);
            if (hypothesis == null)
            {
                return NotFound();
            }
            return View(hypothesis);
        }

        // POST: Hypotheses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Content,Explanation")] Hypothesis hypothesis)
        {
            if (id != hypothesis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hypothesis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HypothesisExists(hypothesis.Id))
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
            return View(hypothesis);
        }

        // GET: Hypotheses/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hypothesis = await _context.Hypothesis
                .SingleOrDefaultAsync(m => m.Id == id);
            if (hypothesis == null)
            {
                return NotFound();
            }

            return View(hypothesis);
        }

        // POST: Hypotheses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var hypothesis = await _context.Hypothesis.SingleOrDefaultAsync(m => m.Id == id);
            _context.Hypothesis.Remove(hypothesis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HypothesisExists(long id)
        {
            return _context.Hypothesis.Any(e => e.Id == id);
        }
    }
}
