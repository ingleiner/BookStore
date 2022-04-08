namespace BookStore.DTOs
{
    public class BooksWhitAuthors : BookDTO
    {
        public ICollection<AuthorDTO> Authors { get; set; }
    }
}