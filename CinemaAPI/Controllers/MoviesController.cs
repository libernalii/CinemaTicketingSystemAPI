using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CinemaStorage.Data;
using CinemaCore.Models;

namespace CinemaAPI.Controllers
{

    [ApiController]
    [Route("movies")]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        // ВСІ БАЧАТЬ ФІЛЬМИ
        [HttpGet]
        public IActionResult GetAll()
        {
            var movies = _context.Movies.ToList();
            return Ok(movies);
        }

        // ТІЛЬКИ ADMIN
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();

            return Ok(movie);
        }

        // ТІЛЬКИ ADMIN
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, Movie updatedMovie)
        {
            var movie = _context.Movies.Find(id);

            if (movie == null)
                return NotFound();

            movie.Title = updatedMovie.Title;
            movie.Genre = updatedMovie.Genre;
            movie.DurationMinutes = updatedMovie.DurationMinutes;
            movie.Description = updatedMovie.Description;

            _context.SaveChanges();

            return Ok(movie);
        }

        // ТІЛЬКИ ADMIN
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.Find(id);

            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return Ok("Deleted");
        }
    }
}
