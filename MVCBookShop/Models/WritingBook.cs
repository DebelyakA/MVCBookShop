using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBookShop.Models
{
    public class WritingBook
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [Required]
        public Book Book { get; set; }

        [Required]
        [ForeignKey("Writing")]
        public int WritingId { get; set; }
        [Required]
        public Writing Writing { get; set; }


    }
}
