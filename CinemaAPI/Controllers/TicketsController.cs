using CinemaAPI.DTOs;
using CinemaCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaAPI.Controllers
{
    [ApiController]
    [Route("tickets")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _service;

        public TicketsController(ITicketService service)
        {
            _service = service;
        }

        // Купити квиток
        [Authorize]
        [HttpPost]
        public IActionResult Buy(CreateTicketRequest request)
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);

            var ticket = _service.Buy(userId, request.MovieId);

            return Ok(ticket);
        }

        // Мої квитки
        [Authorize]
        [HttpGet("user")]
        public IActionResult GetMy()
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);

            var tickets = _service.GetUserTickets(userId);

            return Ok(tickets);
        }

        // Всі квитки
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var tickets = _service.GetAll();
            return Ok(tickets);
        }

        // Скасування
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Cancel(int id)
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);
            var isAdmin = User.IsInRole("Admin");

            var result = _service.Cancel(id, userId, isAdmin);

            return Ok(result);
        }
    }
}
