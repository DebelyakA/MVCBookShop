using System.ComponentModel.DataAnnotations;

namespace MVCBookShop.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
        public string? ImageLink { get; set; }
    }
}