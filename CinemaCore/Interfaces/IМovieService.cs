using CinemaCore.Models;

namespace CinemaCore.Interfaces
{
    public interface IMovieService
    {
        List<Movie> GetAll();
        Movie Create(Movie movie);
        Movie Update(int id, Movie movie);
        void Delete(int id);
    }
}
