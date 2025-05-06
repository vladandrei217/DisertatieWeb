using DisertatieWeb.Models;
using DisertatieWeb.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisertatieWeb.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorInteractionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SensorInteractionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<InteractiuneSenzorModel>>> Get()
        {
            var interactions = await _context.SensorInteractions
                .Include(s => s.PointOfInterest)
                .Select(s => new InteractiuneSenzorModel
                {
                    Id = s.Id,
                    PunctDeInteres = s.PointOfInterest.Name,
                    NotificareTrimisa = s.NotificareTrimisa,
                    UltimaDetectie = s.UltimaDetectie,
                    UltimulComentariu = s.UltimulComentariu
                })
                .ToListAsync();

            return Ok(interactions);
        }
    }
}
