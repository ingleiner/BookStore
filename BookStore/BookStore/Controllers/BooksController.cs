#nullable disable
using AutoMapper;
using BookStore.Data;
using BookStore.Data.Entities;
using BookStore.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BooksController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BooksController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            var books =  await _context.Books.Include(b => b.AuthorsBooks)
                .ThenInclude(ab => ab.Author)
                .ToListAsync();
            return _mapper.Map<List<BookDTO>>(books);    
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BooksWhitAuthors>> GetBook(int id)
        {
            var book = await _context.Books.Include(b => b.AuthorsBooks)
                .ThenInclude(ab => ab.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return _mapper.Map<BooksWhitAuthors>(book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookUpDateDTO bookUpDateDTO)
        {
            
            var bookDB = await _context.Books
                .Include(b => b.AuthorsBooks)
                .FirstOrDefaultAsync(b => b.Id==id);
            if (bookDB == null)
            {
                return NotFound();
            }

            bookDB = _mapper.Map(bookUpDateDTO, bookDB);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> PostBook(int genreId, int publisherId, BookCreationDTO bookCreationDTO)
        {
            if (bookCreationDTO.AuthorsId == null)
            {
                return BadRequest("No se puede crear un libro sin un autor");
            }
            var authorsIds = await _context.Authors.Where(a => bookCreationDTO.AuthorsId
            .Contains(a.Id)).Select(au => au.Id).ToListAsync();
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == genreId);
            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Id == publisherId);
            var book = _mapper.Map<Book>(bookCreationDTO);

            if(bookCreationDTO.AuthorsId.Count != authorsIds.Count)
            {
                return BadRequest("No Existe uno de los autores enviados");
            }
            if(genre == null)
            {
                return BadRequest("No Existe el género relacionado");
            }
            if (publisher == null)
            {
                return BadRequest("No Existe la Editorial relacionada");
            }

            book.GenreId = genreId;
            book.PublisherId = publisherId;
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            var bookDTO = _mapper.Map<BookDTO>(book);
            return CreatedAtRoute("ObtenerLibro", new {id = book.Id}, bookDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
