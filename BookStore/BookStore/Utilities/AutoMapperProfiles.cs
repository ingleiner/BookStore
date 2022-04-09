using AutoMapper;
using BookStore.Data.Entities;
using BookStore.DTOs;

namespace BookStore.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AuthorCreationDTO, Author>();

            CreateMap<Author, AuthorDTO>();

            CreateMap<Author, AuthorDTOWhitBooks>()
                .ForMember(aDTO => aDTO.BooKs, op => op.MapFrom(MapAuthorsBooks));

            CreateMap<BookCreationDTO, Book>()
                .ForMember(b => b.AuthorsBooks, op => op.MapFrom(MapAuthorsBooks));

            CreateMap<BookUpDateDTO, Book>();

            CreateMap<Book, BookDTO>();

            CreateMap<Genre, GenreDTO>();
            CreateMap<Publisher, PublisherDTO>();
            CreateMap<Book, BooksWhitAuthors>()
                .ForMember(bDTO => bDTO.Authors, op => op.MapFrom(MapBookDTOAuthors));

            CreateMap<GenreCreationDTO, Genre>();
            
            CreateMap<PublisherCreationDTO, Publisher>();
            
            
        }

        private List<BookDTO> MapAuthorsBooks(Author author, AuthorDTO authorDTO)
        {
            var result = new List<BookDTO>();
            if(author.AuthorsBooks == null)
            {
                return result;
            }
            
            foreach (var bookAuthor in author.AuthorsBooks)
            {
                result.Add(new BookDTO() { 
                    Id = bookAuthor.BookId,
                    Name = bookAuthor.Book.Name
                });
            }
            return result;
        }
        //private GenreDTO MapGenreBook(Book book, BookDTO bookDTO)
        //{
        //    var result = new GenreDTO(){
        //        Id = book.GenreId,
        //        Name = book.Genre.Name,
        //    };
                       
        //    return result;
        //}
        private List<AuthorDTO> MapBookDTOAuthors(Book book, BookDTO bookDTO)
        {
            var result = new List<AuthorDTO>();

            if(book.AuthorsBooks == null)
            {
                return result;
            }
            foreach(var authorBook in book.AuthorsBooks)
            {
                result.Add(new AuthorDTO()
                {
                    Id = authorBook.AuthorId,
                    FirstName = authorBook.Author.FirstName,
                    LastName = authorBook.Author.LastName,
                });
            }

            return result;
        }
        private List<AuthorBook> MapAuthorsBooks(BookCreationDTO bookCreationDTO, Book book)
        {
            var result = new List<AuthorBook>();

            if(bookCreationDTO.AuthorsId == null) { return result; }

            foreach(var authorID in bookCreationDTO.AuthorsId)
            {
                result.Add(new AuthorBook()
                {
                    AuthorId = authorID,
                });
            }
            return result;
        }
    }
}
