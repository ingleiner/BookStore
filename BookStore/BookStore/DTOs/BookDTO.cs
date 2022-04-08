using BookStore.Data.Entities;

namespace BookStore.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime PublicationDate { get; set; }
        public int GenreId { get; set; }
        public int PublisherID { get; set; }
    }
}
