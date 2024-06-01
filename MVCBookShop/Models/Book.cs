using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MVCBookShop.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        public int? Year { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string? ImageLink { get; set; }

        [ForeignKey("Publisher")]
        public int? PublisherId { get; set; }
        public Publisher? Publisher { get; set; }

        public ICollection<WritingBook> WritingBook { get; set; } = new List<WritingBook>();
    }
}
