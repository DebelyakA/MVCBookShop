using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCBookShop.Models
{
    public class BookDto
    {

        public long Id { get; set; }
        public int? Year { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? ImageLink { get; set; }
        public int? PublisherId { get; set; }
        public Publisher? Publisher
        {
            get; set;
        }
    }
	public class AuthorDto
	{
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string? SecondName { get; set; }
        public ICollection<Writing> Writings { get; set; }
    }

    public class OrderDto
    {
        public int Id { get; set; }

        public DateOnly? DateTime { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int BookId { get; set; }
      
        public Book Book { get; set; }
    }

    public class PublisherDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageLink { get; set; }
    }
    public class WritingDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public string? Description { get; set; }
    }
    public class WritingBookDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
       
        public Book Book { get; set; }

        public int WritingId { get; set; }
        public Writing Writing { get; set; }
    }
}
