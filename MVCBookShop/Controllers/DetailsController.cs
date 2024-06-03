using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVCBookShop.Controllers
{
    public class DetailsController : Controller
    {
        private readonly BookStoreContext _context;
        public DetailsController(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> PublisherIndex(int id)
        {
            var publishers = await _context.Publisher.Include(b => b.Books).FirstOrDefaultAsync(p => p.Id == id);
            return View(publishers);
        }

        public async Task<IActionResult> AuthorIndex(int id)
        {
            var authors = await _context.Author.Include(w => w.Writings)
                .ThenInclude(wb => wb.WritingBooks)
                .ThenInclude(b => b.Book)
                .FirstOrDefaultAsync(a => a.Id == id);
            return View(authors);
        }

        public async Task<IActionResult> WritingIndex(int id)
        {
            var writing = await _context.Writing
                .Include(wb => wb.WritingBooks)
                .ThenInclude(b => b.Book)
                .FirstOrDefaultAsync(a => a.Id == id);
            return View(writing);
        }
    }
}
