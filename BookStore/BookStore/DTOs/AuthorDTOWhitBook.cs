namespace BookStore.DTOs
{
    public class AuthorDTOWhitBook
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public List<AuthorDTO> Authors { get; set; }
        public List<BookDTO> Books { get; set; }
    }
}

