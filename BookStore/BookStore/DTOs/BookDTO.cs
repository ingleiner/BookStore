using BookStore.Data.Entities;

namespace BookStore.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime PublicationDate { get; set; }
        public virtual Genre Genre { get; set; } 
        public virtual Publisher Publisher { get; set; }
    }
}
