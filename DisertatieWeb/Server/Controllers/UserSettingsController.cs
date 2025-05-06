using DisertatieWeb.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisertatieWeb.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserSettingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserSettings>> Get(int userId)
        {
            var settings = await _context.UserSettings.FirstOrDefaultAsync(u => u.UserId == userId);
            if (settings == null) return NotFound();
            return Ok(settings);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserSettings model)
        {
            _context.UserSettings.Add(model);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(int userId, UserSettings updated)
        {
            var settings = await _context.UserSettings.FirstOrDefaultAsync(u => u.UserId == userId);
            if (settings == null) return NotFound();

            _context.Entry(settings).CurrentValues.SetValues(updated);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }

}
