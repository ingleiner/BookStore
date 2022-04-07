namespace BookStore.DTOs
{
    public class AuthorDTOWhitBooks: AuthorDTO
    {
        public ICollection<BookDTO> BooKs { get; set; }
    }
}
