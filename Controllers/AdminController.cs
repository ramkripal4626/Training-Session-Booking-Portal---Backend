using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Training_Session_Booking_Portal.DTOs;
using Training_Session_Booking_Portal.Interfaces;
using Training_Session_Booking_Portal.Models;

namespace Training_Session_Booking_Portal.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }

        [HttpGet("pending-sessions")]
        public async Task<IActionResult> GetPendingSessions()
        {
            var sessions = await _service.GetPendingSessionsAsync();
            return Ok(sessions);
        }

        [HttpPost("approve-session/{id}")]
        public async Task<IActionResult> ApproveSession(int id)
        {
            var success = await _service.ApproveSessionAsync(id);
            return success ? Ok(new { Message = "Session approved." }) : NotFound("Session not found.");
        }

        [HttpPost("reject-session/{id}")]
        public async Task<IActionResult> RejectSession(int id)
        {
            var success = await _service.RejectSessionAsync(id);
            return success ? Ok(new { Message = "Session rejected and deleted." }) : NotFound("Session not found.");
        }

        [HttpPost("add-trainer")]
        public async Task<IActionResult> AddTrainer([FromBody] AddTrainerDto dto)
        {
            var success = await _service.AddTrainerAsync(dto);
            return success ? Ok(new { Message = "Trainer added." }) : BadRequest("Failed to add trainer.");
        }

        [HttpPost("promote-to-trainer/{userId}")]
        public async Task<IActionResult> PromoteUserToTrainer(int userId)
        {
            var success = await _service.PromoteUserToTrainerAsync(userId);
            return success ? Ok(new { Message = "User promoted to Trainer." }) : NotFound("User not found.");
        }

        [HttpDelete("delete-trainer/{id}")]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            var success = await _service.DeleteTrainerAsync(id);
            return success ? Ok(new { Message = "Trainer deleted." }) : NotFound("Trainer not found.");
        }
    }
}
