using DisertatieWeb.Models;
using DisertatieWeb.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static DisertatieWeb.Pages.AppPages.Dashboard;

namespace DisertatieWeb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("stats")]
        public async Task<ActionResult<DashboardStats>> GetStats()
        {
            var dispozitiveActive = await _context.Devices.CountAsync();

            var puncte = await _context.Points_Of_Interest.CountAsync();

            var comentarii = await _context.Comments.CountAsync();

            var dispozitiveRecente = await _context.Devices
                .CountAsync(d => d.LastSeen != null && d.LastSeen >= DateTime.UtcNow.AddDays(-1));

            var comentariiRecente = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.PointOfInterest)
                .OrderByDescending(c => c.CreatedAt)
                .Take(5)
                .Select(c => new ComentariuRecent
                {
                    UserEmail = c.User.Email,
                    Continut = c.Content,
                    Data = c.CreatedAt,
                    PunctInteres = c.PointOfInterest.Name
                })
                .ToListAsync();

            return new DashboardStats
            {
                DispozitiveActive = dispozitiveActive,
                PuncteTuristice = puncte,
                ComentariiTotal = comentarii,
                DispozitiveRecente = dispozitiveRecente,
                ComentariiRecente = comentariiRecente
            };
        }
        [HttpGet("evenimente")]
        public async Task<ActionResult<List<EvenimentTrafic>>> GetEvenimente()
        {
            var evenimente = await _context.Events
                .OrderByDescending(e => e.CreatedAt)
                .Take(10)
                .Select(e => new EvenimentTrafic
                {
                    Tip = e.Tip,
                    Locatie = e.Locatie,
                    Data = e.CreatedAt
                })
                .ToListAsync();

            return Ok(evenimente);
        }
    }
}
