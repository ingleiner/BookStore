#nullable disable
using AutoMapper;
using BookStore.Data;
using BookStore.Data.Entities;
using BookStore.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allorigin")]
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
        public async Task<ActionResult<IEnumerable<BooksWhitAuthors>>> GetBooks()
        {
            var books = await _context.Books
                .Include(ab => ab.AuthorsBooks)
                .ThenInclude(ab => ab.Author).Include(ge=> ge.Genre).Include(pu => pu.Publisher)
                .ToListAsync();

            return _mapper.Map<List<BooksWhitAuthors>>(books);    
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BooksWhitAuthors>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(ab => ab.AuthorsBooks)
                .ThenInclude(ab => ab.Author)
                .Include(ge => ge.Genre).Include(pu => pu.Publisher)
                .FirstOrDefaultAsync(ab =>ab.Id == id);

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
            return Ok("200");
        }

        [HttpPost]
        public async Task<ActionResult> PostBook(BookCreationDTO bookCreationDTO)
        {
            if (bookCreationDTO.AuthorsId == null)
            {
                return BadRequest("No se puede crear un libro sin un autor");
            }
            var authorsIds = await _context.Authors.Where(a => bookCreationDTO.AuthorsId
            .Contains(a.Id)).Select(au => au.Id).ToListAsync();
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == bookCreationDTO.GenreId);
            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Id == bookCreationDTO.PublisherId);
            var book = _mapper.Map<Book>(bookCreationDTO);

            if(bookCreationDTO.AuthorsId.Count != authorsIds.Count)
            {
                return BadRequest("No Existe uno de los autores enviados");
            }

            book.GenreId = bookCreationDTO.GenreId;
            book.PublisherId = bookCreationDTO.PublisherId;
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            var bookDTO = _mapper.Map<BookDTO>(book);
            return Ok("200");
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
