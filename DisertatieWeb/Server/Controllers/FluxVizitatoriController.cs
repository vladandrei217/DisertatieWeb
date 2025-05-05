using DisertatieWeb.Models;
using DisertatieWeb.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisertatieWeb.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FluxVizitatoriController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FluxVizitatoriController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<FluxVizitatoriModel>>> Get()
        {
            var fluxuri = await _context.VisitorFlows
                .Include(f => f.PointOfInterest)
                .OrderByDescending(f => f.OraRaportarii)
                .Take(10)
                .Select(f => new FluxVizitatoriModel
                {
                    Locație = f.PointOfInterest.Name,
                    NumarVizitatori = f.NumarVizitatori,
                    OraRaportarii = f.OraRaportarii
                })
                .ToListAsync();

            return Ok(fluxuri);
        }
    }
}
