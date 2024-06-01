namespace MVCBookShop.Models.Cart
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddItem(Book book)
        {
            var cartItem = Items.SingleOrDefault(i => i.BookId == book.Id);

            if (cartItem == null)
            {
                Items.Add(new CartItem
                {
                    BookId = book.Id,
                    Title = book.Title,
                    Price = book.Price,
                    Quantity = 1
                });
            } else
            {
                cartItem.Quantity++;
            }
        }

        public void RemoveItem(int bookId)
        {
            Items.RemoveAll(i => i.BookId == bookId);
        }
    }
}
