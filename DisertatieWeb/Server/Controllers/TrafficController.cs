using DisertatieWeb.Models;
using DisertatieWeb.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisertatieWeb.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrafficController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrafficController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<FluxTrafic>>> GetFluxuri()
        {
            var fluxuri = await _context.TrafficFlows
                .Include(f => f.Masuratori)
                .OrderBy(f => f.Nume)
                .Select(f => new FluxTrafic
                {
                    Id = f.Id,
                    Nume = f.Nume,
                    Descriere = f.Descriere,
                    DataUltimeiActualizari = f.DataUltimeiActualizari,
                    Detalii = f.Masuratori
                        .OrderByDescending(m => m.Ora)
                        .Select(m => new FluxTraficDetalii
                        {
                            Id = m.Id,
                            Ora = m.Ora,
                            Valoare = m.Valoare
                        }).ToList()
                })
                .ToListAsync();

            return Ok(fluxuri);
        }
    }
}
