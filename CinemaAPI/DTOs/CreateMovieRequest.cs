namespace CinemaAPI.DTOs
{
    public class CreateMovieRequest
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int DurationMinutes { get; set; }
        public string Description { get; set; }
        public DateTime ShowTime { get; set; }
    }
}
