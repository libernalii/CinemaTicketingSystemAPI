using CinemaAPI.DTOs;
using CinemaCore.Models;
using CinemaStorage.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Controllers
{
    [ApiController]
    [Route("tickets")]
    public class TicketsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketsController(AppDbContext context)
        {
            _context = context;
        }

        // Купити квиток (Customer)
        [Authorize]
        [HttpPost]
        public IActionResult BuyTicket([FromBody] CreateTicketRequest request)
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);

            var movie = _context.Movies.Find(request.MovieId);
            if (movie == null)
                return NotFound("Movie not found");

            var ticket = new Ticket
            {
                UserId = userId,
                MovieId = request.MovieId,
                PurchaseDate = DateTime.Now,
                Status = "Active"
            };

            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            return Ok(ticket);
        }

        // Мої квитки
        [Authorize]
        [HttpGet("user")]
        public IActionResult GetMyTickets()
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);

            var tickets = _context.Tickets
                .Include(t => t.Movie)
                .Where(t => t.UserId == userId)
                .ToList();

            return Ok(tickets);
        }

        // Всі квитки (Admin)
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var tickets = _context.Tickets
                .Include(t => t.User)
                .Include(t => t.Movie)
                .ToList();

            return Ok(tickets);
        }

        // Скасувати квиток
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Cancel(int id)
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);

            var ticket = _context.Tickets.Find(id);

            if (ticket == null)
                return NotFound();

            // тільки свій квиток або адмін
            if (ticket.UserId != userId && !User.IsInRole("Admin"))
                return Forbid();

            ticket.Status = "Cancelled";
            _context.SaveChanges();

            return Ok(ticket);
        }
    }
}
