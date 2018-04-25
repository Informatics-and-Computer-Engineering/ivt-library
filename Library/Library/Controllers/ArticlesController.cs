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
    public class ArticlesController : Controller
    {
        private readonly LibraryContext _context;

        public ArticlesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Article.Include(a => a.City).Include(a => a.Conference).Include(a => a.Supervisor);
            return View(await libraryContext.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.City)
                .Include(a => a.Conference)
                .Include(a => a.Supervisor)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name");
            ViewData["ConferenceId"] = new SelectList(_context.Conference, "Id", "Name");
            ViewData["SupervisorId"] = new SelectList(_context.Author, "Id", "LastName");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CityId,ConferenceId,Year,Bibliography,SupervisorId,ConferenceStartDate,ConferenceEndDate,PublicationDate,Pages,Page,Volume,ConferenceNumber")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", article.CityId);
            ViewData["ConferenceId"] = new SelectList(_context.Conference, "Id", "Name", article.ConferenceId);
            ViewData["SupervisorId"] = new SelectList(_context.Author, "Id", "LastName", article.SupervisorId);
            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article.SingleOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", article.CityId);
            ViewData["ConferenceId"] = new SelectList(_context.Conference, "Id", "Name", article.ConferenceId);
            ViewData["SupervisorId"] = new SelectList(_context.Author, "Id", "LastName", article.SupervisorId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CityId,ConferenceId,Year,Bibliography,SupervisorId,ConferenceStartDate,ConferenceEndDate,PublicationDate,Pages,Page,Volume,ConferenceNumber")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
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
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", article.CityId);
            ViewData["ConferenceId"] = new SelectList(_context.Conference, "Id", "Name", article.ConferenceId);
            ViewData["SupervisorId"] = new SelectList(_context.Author, "Id", "LastName", article.SupervisorId);
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.City)
                .Include(a => a.Conference)
                .Include(a => a.Supervisor)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Article.SingleOrDefaultAsync(m => m.Id == id);
            _context.Article.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.Id == id);
        }
    }
}
