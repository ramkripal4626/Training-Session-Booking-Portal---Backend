using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Training_Session_Booking_Portal.DTOs;
using Training_Session_Booking_Portal.Interfaces;
namespace Training_Session_Booking_Portal.Controllers
{
    [Authorize(Roles = "Trainer")]
    [ApiController]
    [Route("api/[controller]")]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [HttpPost("create-session")]
        public async Task<IActionResult> CreateSession(SessionDto dto)
        {
            var trainerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var message = await _trainerService.CreateSessionAsync(trainerId, dto);
            return Ok(new { Message = message });
        }

        [HttpGet("my-sessions")]
        public async Task<IActionResult> GetMySessions()
        {
            var trainerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var sessions = await _trainerService.GetMySessionsAsync(trainerId);
            return Ok(sessions);
        }
    }

}
