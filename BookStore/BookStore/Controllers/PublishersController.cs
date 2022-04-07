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
    public class PublishersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PublishersController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherDTO>>> GetPublishers()
        {
            var publisher =  await _context.Publishers.ToListAsync();
            return _mapper.Map<List<PublisherDTO>>(publisher);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherDTO>> GetPublisher(int id)
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Id == id);

            if (publisher == null)
            {
                return NotFound();
            }

            return _mapper.Map<PublisherDTO>(publisher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisher(int id, PublisherCreationDTO publisherCreationDTO)
        {
            var publisherDB = await _context.Publishers.AnyAsync(p => p.Id == id);
            if (!publisherDB)
            {
                return NotFound();
            }
            var publisher = _mapper.Map<Publisher>(publisherCreationDTO);
            publisher.Id = id;
            _context.Update(publisher);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Publisher>> PostPublisher(PublisherCreationDTO publisherCreationDTO)
        {
            var publisher = _mapper.Map<Publisher>(publisherCreationDTO);
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPublisher", new { id = publisherCreationDTO.Name }, publisherCreationDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PublisherExists(int id)
        {
            return _context.Publishers.Any(e => e.Id == id);
        }
    }
}
