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
    public class FileBooksController : Controller
    {
        private readonly LibraryContext _context;

        public FileBooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: FileBooks
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.FileBook.Include(f => f.Book).Include(f => f.Type);
            return View(await libraryContext.ToListAsync());
        }

        // GET: FileBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileBook = await _context.FileBook
                .Include(f => f.Book)
                .Include(f => f.Type)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (fileBook == null)
            {
                return NotFound();
            }

            return View(fileBook);
        }

        // GET: FileBooks/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name");
            ViewData["TypeId"] = new SelectList(_context.FileType, "Id", "Name");
            return View();
        }

        // POST: FileBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ContentType,Data,TypeId,Version,BookId")] FileBook fileBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fileBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name", fileBook.BookId);
            ViewData["TypeId"] = new SelectList(_context.FileType, "Id", "Name", fileBook.TypeId);
            return View(fileBook);
        }

        // GET: FileBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileBook = await _context.FileBook.SingleOrDefaultAsync(m => m.Id == id);
            if (fileBook == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name", fileBook.BookId);
            ViewData["TypeId"] = new SelectList(_context.FileType, "Id", "Name", fileBook.TypeId);
            return View(fileBook);
        }

        // POST: FileBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ContentType,Data,TypeId,Version,BookId")] FileBook fileBook)
        {
            if (id != fileBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileBookExists(fileBook.Id))
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
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name", fileBook.BookId);
            ViewData["TypeId"] = new SelectList(_context.FileType, "Id", "Name", fileBook.TypeId);
            return View(fileBook);
        }

        // GET: FileBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileBook = await _context.FileBook
                .Include(f => f.Book)
                .Include(f => f.Type)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (fileBook == null)
            {
                return NotFound();
            }

            return View(fileBook);
        }

        // POST: FileBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fileBook = await _context.FileBook.SingleOrDefaultAsync(m => m.Id == id);
            _context.FileBook.Remove(fileBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileBookExists(int id)
        {
            return _context.FileBook.Any(e => e.Id == id);
        }
    }
}
