using DisertatieWeb.Models;
using DisertatieWeb.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisertatieWeb.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZonePopulareController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ZonePopulareController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ZonaPopularaModel>>> Get()
        {
            var result = await _context.Points_Of_Interest
                .Where(p => p.VizitatoriTotal > 0)
                .OrderByDescending(p => p.VizitatoriTotal)
                .Take(10)
                .Select(p => new ZonaPopularaModel
                {
                    Id = p.Id,
                    NumeZona = p.Name,
                    Vizitatori = p.VizitatoriTotal,
                    Latitudine = p.Latitude,
                    Longitudine = p.Longitude
                }).ToListAsync();

            return Ok(result);
        }
    }
}
