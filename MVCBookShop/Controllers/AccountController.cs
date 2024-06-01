using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using MVCBookShop.Models.VIewModel;

namespace MVCBookShop.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly BookStoreContext _context;

        public AccountController(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var userLogin = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.User.FirstOrDefaultAsync(a => a.Login == userLogin);

            if (user == null)
            {
                return NotFound();
            }

            var orders = await _context.Order
                .Where(u => u.UserId == user.Id)
                .Include(o => o.Book)
                .ThenInclude(b => b.WritingBook)
                .ThenInclude(wb => wb.Writing)
                .ThenInclude(w => w.Author)
                .ToListAsync();

            var model = new UserProfileViewModel
            {
                Name = user.Name,
                Login = user.Login,
                Orders = orders.Select(o => new OrderViewModel
                {
                    DateTime = DateOnly.FromDateTime(DateTime.Now),
                    BookTitle = o.Book.Title,
                    Price = o.Book.Price,
                    Authors = o.Book.WritingBook.Select(wb => new AuthorViewModel
                    {
                        FirstName = wb.Writing.Author.FirstName,
                        SecondName = wb.Writing.Author.SecondName
                    }).ToList()
                }).ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
