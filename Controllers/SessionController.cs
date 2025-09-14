using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Training_Session_Booking_Portal.DTOs;
using Training_Session_Booking_Portal.Interfaces;
using Training_Session_Booking_Portal.Models;

namespace Training_Session_Booking_Portal.Controllers
{
    [Authorize(Roles = "User,Trainer,Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSessions()
        {
            var sessions = await _sessionService.GetAllSessionsAsync();
            return Ok(sessions);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSession(int id)
        {
            var session = await _sessionService.GetSessionByIdAsync(id);
            if (session == null) return NotFound(new { Message = "Session not found" });
            return Ok(session);
        }

        [Authorize(Roles = "Trainer,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSession([FromBody] SessionDto dto)
        {
            var session = new Session
            {
                Title = dto.Title,
                Description = dto.Description,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Capacity = dto.Capacity
            };

            var result = await _sessionService.CreateSessionAsync(session, User);
            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Trainer,Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateSession([FromBody] SessionUpdateDto session)
        {
            var result = await _sessionService.UpdateSessionAsync(session);
            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveSession(int id, [FromQuery] bool approve = true)
        {
            var result = await _sessionService.ApproveSessionAsync(id, approve);
            if (result.Contains("not found")) return NotFound(new { Message = result });
            return Ok(new { Message = result });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var result = await _sessionService.DeleteSessionAsync(id);
            if (result.Contains("not found")) return NotFound(new { Message = result });
            return Ok(new { Message = result });
        }
    }


}
