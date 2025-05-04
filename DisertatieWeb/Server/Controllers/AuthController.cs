using DisertatieWeb.Shared.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisertatieWeb.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                return BadRequest("Email deja folosit.");

            var user = new User
            {
                Name = model.NumeComplet,
                Email = model.Email,
                Username = model.Username
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, model.Parola);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
            if (user == null)
                return Unauthorized("Nume utilizator invalid.");

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Parolă incorectă.");

            return Ok(); 
        }

        public class RegisterModel
        {
            public string NumeComplet { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string Parola { get; set; }
        }
        public class UserLoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
