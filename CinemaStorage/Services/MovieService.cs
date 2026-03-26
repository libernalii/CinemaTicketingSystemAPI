using CinemaCore.Interfaces;
using CinemaCore.Models;
using CinemaStorage.Data;

namespace CinemaStorage.Services
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext _context;

        public MovieService(AppDbContext context)
        {
            _context = context;
        }

        public List<Movie> GetAll()
        {
            return _context.Movies.ToList();
        }

        public Movie Create(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return movie;
        }

        public Movie Update(int id, Movie updated)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null) throw new Exception("Not found");

            movie.Title = updated.Title;
            movie.Genre = updated.Genre;
            movie.DurationMinutes = updated.DurationMinutes;
            movie.Description = updated.Description;

            _context.SaveChanges();
            return movie;
        }

        public void Delete(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null) throw new Exception("Not found");

            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
    }
}
