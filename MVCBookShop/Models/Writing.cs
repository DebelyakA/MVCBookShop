using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBookShop.Models
{
    public class Writing
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public int? Year { get; set; }
        [Required]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        [Required]
        public Author Author { get; set; }
        public string? Description { get; set; }
    }
}