namespace MVCBookShop.Models.VIewModel
{
    public class UserProfileViewModel
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }

    public class OrderViewModel
    {
        public DateOnly DateTime { get; set; }
        public string BookTitle { get; set; }
        public decimal Price { get; set; }
        public List<AuthorViewModel> Authors { get; set; }
    }

    public class AuthorViewModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
}
