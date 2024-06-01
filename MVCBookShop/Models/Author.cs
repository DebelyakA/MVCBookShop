using System.ComponentModel.DataAnnotations;

namespace MVCBookShop.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string? SecondName { get; set; }

        public ICollection<Writing> Writings { get; set; }
    }
}
