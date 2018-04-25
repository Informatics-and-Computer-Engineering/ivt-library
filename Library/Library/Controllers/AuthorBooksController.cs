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
    public class AuthorBooksController : Controller
    {
        private readonly LibraryContext _context;

        public AuthorBooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: AuthorBooks
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.AuthorBook.Include(a => a.Author).Include(a => a.Book);
            return View(await libraryContext.ToListAsync());
        }

        // GET: AuthorBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorBook = await _context.AuthorBook
                .Include(a => a.Author)
                .Include(a => a.Book)
                .SingleOrDefaultAsync(m => m.AuthorId == id);
            if (authorBook == null)
            {
                return NotFound();
            }

            return View(authorBook);
        }

        // GET: AuthorBooks/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName");
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name");
            return View();
        }

        // POST: AuthorBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,BookId")] AuthorBook authorBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authorBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName", authorBook.AuthorId);
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name", authorBook.BookId);
            return View(authorBook);
        }

        // GET: AuthorBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorBook = await _context.AuthorBook.SingleOrDefaultAsync(m => m.AuthorId == id);
            if (authorBook == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName", authorBook.AuthorId);
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name", authorBook.BookId);
            return View(authorBook);
        }

        // POST: AuthorBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorId,BookId")] AuthorBook authorBook)
        {
            if (id != authorBook.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorBookExists(authorBook.AuthorId))
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
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "LastName", authorBook.AuthorId);
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name", authorBook.BookId);
            return View(authorBook);
        }

        // GET: AuthorBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorBook = await _context.AuthorBook
                .Include(a => a.Author)
                .Include(a => a.Book)
                .SingleOrDefaultAsync(m => m.AuthorId == id);
            if (authorBook == null)
            {
                return NotFound();
            }

            return View(authorBook);
        }

        // POST: AuthorBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorBook = await _context.AuthorBook.SingleOrDefaultAsync(m => m.AuthorId == id);
            _context.AuthorBook.Remove(authorBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorBookExists(int id)
        {
            return _context.AuthorBook.Any(e => e.AuthorId == id);
        }
    }
}
