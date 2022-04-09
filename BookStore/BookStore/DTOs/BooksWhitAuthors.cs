namespace BookStore.DTOs
{
    public class BooksWhitAuthors: BookDTO
    {
        public ICollection<AuthorDTO> Authors { get; set; }
        public virtual GenreDTO Genre { get; set; }
        public virtual PublisherDTO Publisher { get; set; }
    }
}
