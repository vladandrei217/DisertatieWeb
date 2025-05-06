using DisertatieWeb.Models;
using DisertatieWeb.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisertatieWeb.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> Get()
        {
            var users = await _context.Users
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    Nume = u.Name,
                    Email = u.Email
                }).ToListAsync();

            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserModel model)
        {
            var user = new User
            {
                Name = model.Nume,
                Email = model.Email,
                Username = model.Email.Split('@')[0],
                PasswordHash = "placeholder" // dacă nu folosești autentificare aici
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserModel model)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.Name = model.Nume;
            user.Email = model.Email;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

}
