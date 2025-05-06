using DisertatieWeb.Models;
using DisertatieWeb.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisertatieWeb.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PuncteInteresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PuncteInteresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PunctDeInteresModel>>> Get()
        {
            var result = await _context.Points_Of_Interest
                .Select(p => new PunctDeInteresModel
                {
                    Id = p.Id,
                    Nume = p.Name,
                    Descriere = p.Description,
                    ImagineUrl = p.ImagineUrl,
                    Latitudine = p.Latitude,
                    Longitudine = p.Longitude
                })
                .ToListAsync();

            return Ok(result);
        }
    }
}
