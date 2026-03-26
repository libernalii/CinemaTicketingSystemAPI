using CinemaCore.Interfaces;
using CinemaCore.Models;
using CinemaStorage.Data;

namespace CinemaStorage.Services
{
    public class TicketService : ITicketService
    {
        private readonly AppDbContext _context;

        public TicketService(AppDbContext context)
        {
            _context = context;
        }

        public Ticket Buy(int userId, int movieId)
        {
            var movie = _context.Movies.Find(movieId);
            if (movie == null) throw new Exception("Movie not found");

            var ticket = new Ticket
            {
                UserId = userId,
                MovieId = movieId,
                PurchaseDate = DateTime.Now,
                Status = "Active"
            };

            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            return ticket;
        }

        public List<Ticket> GetUserTickets(int userId)
        {
            return _context.Tickets
                .Where(t => t.UserId == userId)
                .ToList();
        }

        public List<Ticket> GetAll()
        {
            return _context.Tickets.ToList();
        }

        public Ticket Cancel(int id, int userId, bool isAdmin)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket == null) throw new Exception("Not found");

            if (ticket.UserId != userId && !isAdmin)
                throw new Exception("Forbidden");

            ticket.Status = "Cancelled";
            _context.SaveChanges();

            return ticket;
        }
    }
}
