using CinemaCore.Interfaces;
using CinemaCore.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaAPI.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _service;

        public MoviesController(IMovieService service)
        {
            _service = service;
        }

        // ВСІ БАЧАТЬ ФІЛЬМИ
        [HttpGet]
        public IActionResult GetAll()
        {
            var movies = _service.GetAll();
            return Ok(movies);
        }

        // ТІЛЬКИ ADMIN
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(MovieRequest movie)
        {
            var result = _service.Create(movie);
            return Ok(result);
        }

        // ТІЛЬКИ ADMIN
        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, MovieRequest movie)
        {
            var result = _service.Update(id, movie);
            return Ok(result);
        }

        // ТІЛЬКИ ADMIN
        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok("Deleted");
        }
    }
}
