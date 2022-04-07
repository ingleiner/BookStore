namespace BookStore.Data.Entities
{
    public class AuthorBook
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        public int Orden { get; set; }

        public Book Book { get; set; }
        public Author Author { get; set; }

    }
}
