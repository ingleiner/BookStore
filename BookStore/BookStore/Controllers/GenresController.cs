﻿#nullable disable
using BookStore.Data;
using BookStore.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly DataContext _context;

        public GenresController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return genre;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, Genre genre)
        {
            if (id != genre.Id)
            {
                return BadRequest();
            }

            _context.Entry(genre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Genre>> PostGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenre", new { id = genre.Id }, genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenreExists(int id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }
    }
}
