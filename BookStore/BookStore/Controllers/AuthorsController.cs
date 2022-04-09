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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthorsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AuthorsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            var authors =  await _context.Authors.ToListAsync();
            return _mapper.Map<List<AuthorDTO>>(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTOWhitBooks>> GetAuthor(int id)
        {
            var author = await _context.Authors
                .Include(b => b.AuthorsBooks)
                .ThenInclude(bDTO => bDTO.Book)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return _mapper.Map<AuthorDTOWhitBooks>(author);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAuthor(AuthorCreationDTO authorCreationDTO, int id)
        {

            var authorExis = await _context.Authors.AnyAsync(a => a.Id == id);

            if(!authorExis)
            {
                return NotFound();
            }

            var author = _mapper.Map<Author>(authorCreationDTO);
            author.Id = id;
            _context.Update(author);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorCreationDTO authorCreationDTO)
        {
           var author = _mapper.Map<Author>(authorCreationDTO);

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            var authorDTO = _mapper.Map<AuthorDTO>(author);
            return CreatedAtAction("GetAuthor", new { id = author.Id }, authorDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
