using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Training_Session_Booking_Portal.Data;
using Training_Session_Booking_Portal.Interfaces;
using Training_Session_Booking_Portal.Models;

namespace Training_Session_Booking_Portal.Controllers
{
    [Authorize(Roles = "User")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("available-sessions")]
        public async Task<IActionResult> GetAvailableSessions()
        {
            var sessions = await _userService.GetAvailableSessionsAsync();
            return Ok(sessions);
        }

        [HttpPost("register/{sessionId}")]
        public async Task<IActionResult> RegisterSession(int sessionId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var result = await _userService.RegisterSessionAsync(sessionId, userId);

            if (result.Contains("successfully"))
                return Ok(new { Message = result });

            return BadRequest(new { Message = result });
        }

        [HttpGet("my-sessions")]
        public async Task<IActionResult> GetMySessions()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var bookings = await _userService.GetMySessionsAsync(userId);
            return Ok(bookings); 
        }


        [HttpDelete("withdraw/{sessionId}")]
        public async Task<IActionResult> WithdrawFromSession(int sessionId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var result = await _userService.WithdrawFromSessionAsync(sessionId, userId);

            if (result.Contains("successfully"))
                return Ok(new { Message = result });

            return NotFound(new { Message = result });
        }
    }


}
