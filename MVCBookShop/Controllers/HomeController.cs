using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCBookShop.Models;
using System.Diagnostics;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace MVCBookShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookStoreContext _context;

        public HomeController(BookStoreContext context)
        {
                _context = context;
        }
        public async Task<IActionResult> Index()
        {
           var books = await _context.WritingBook
                .Include(b => b.Book)
                    .ThenInclude(p => p.Publisher)
                .Include(w => w.Writing)
                    .ThenInclude(a => a.Author)
                .ToListAsync();

            return View(books);
        }
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            } 
                var book = await _context.WritingBook
                    .Include(b => b.Book)
                        .ThenInclude(p => p.Publisher)
                    .Include(w => w.Writing)
                        .ThenInclude(a => a.Author)
                    .FirstOrDefaultAsync(wb => wb.Book.Id == id);
                if (book == null)
                {
                    return NotFound();
                }
                return View(book);
        }
    }
}
