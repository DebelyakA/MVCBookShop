using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using MVCBookShop.Models;


namespace MVCBookShop.Controllers
{
    public class AuthController : Controller
    {
        private readonly BookStoreContext _context;

        public AuthController(BookStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegisterIndex()
        {
            return View();
        }
        public async Task<IActionResult> Login([FromForm]string login, [FromForm]string password)
        {
           var user = await _context.User.FirstOrDefaultAsync(x => x.Login == login);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                TempData["Message"] = "Неверный логин или пароль";
                return RedirectToAction("Index", "Auth");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, login),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            if(user.Name != null)
            {
                TempData["Message"] = $"{user.Name}, добро пожаловать!";
            } else TempData["Message"] = $"{user.Login}, добро пожаловать!";

            return RedirectToAction("Index", "Account");
        }

        public async Task<ActionResult<string>> Register([FromForm] string login, [FromForm] string password)
        {
            var user = new User();
            user.Login = login;
            user.Password = BCrypt.Net.BCrypt.HashPassword(password);
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Регистрация прошла успешно!";
            return RedirectToAction("Index", "Auth");

        }
    }
}
