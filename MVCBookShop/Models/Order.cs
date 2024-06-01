using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBookShop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateOnly? DateTime { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }

        [Required]
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [Required]
        public Book Book { get; set; }
    }
}
