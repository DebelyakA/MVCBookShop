using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCBookShop.Models.Cart;
using MVCBookShop.Models;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MVCBookShop.Controllers
{
    public class CartController : Controller
    {
        private readonly BookStoreContext _context;

        public CartController(BookStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var userLogin = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.User.FirstOrDefaultAsync(u => u.Login == userLogin);
            if (user == null)
            {
                return NotFound();
            }

            var cart = GetCart();
            if (cart == null)
            {
                TempData["Message"] = "Ваша корзина пуста!";
                return RedirectToAction("Index", "Home");
            }
            foreach (var item in cart.Items)
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    var order = new Order
                    {
                        DateTime = DateOnly.FromDateTime(DateTime.Now),
                        UserId = user.Id,
                        BookId = item.BookId,
                    };

                    _context.Order.Add(order);
                }
            }

            await _context.SaveChangesAsync();
            HttpContext.Session.Remove("Cart");
            TempData["Message"] = "Заказ успешно оформлен!";

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int bookId)
        {
            var writingBook = await _context.WritingBook
             .Include(b => b.Book)
             .FirstOrDefaultAsync(wb => wb.Book.Id == bookId);
            if (writingBook == null)
            {
                return NotFound();
            }

            var cart = GetCart();
            cart.AddItem(writingBook.Book);
            SaveCart(cart);
            TempData["Message"] = "Товар успешно добавлен в корзину!";
            return RedirectToAction("Index","Home");
        }

        public IActionResult RemoveFromCart(int bookId)
        {
            var cart = GetCart();
            cart.RemoveItem(bookId);
            SaveCart(cart);
            TempData["Message"] = "Товар успешно удален из корзины!";
            return RedirectToAction("Index");
        }

        private Cart GetCart()
        {
            var cart = HttpContext.Session.GetString("Cart");
            return cart == null ? new Cart() : JsonConvert.DeserializeObject<Cart>(cart);
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
        }
    }
}
