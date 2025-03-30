using DigitalWallet.Application.DTO;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Application.Models.Responses;
using DigitalWallet.Domain.Entities;
using DigitalWallet.Persistance.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DigitalWallet.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthController(AppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<AuthTokenDto>>> Register([FromBody] RegisterDto request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return BadRequest(ServiceResponse<AuthTokenDto>.Failure("Email already registered."));

            var passwordHash = HashPassword(request.Password);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordHash,
                Wallet = new Wallet()
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var token = _tokenService.CreateToken(user);
            var result = new AuthTokenDto { Token = token };
            return Ok(ServiceResponse<AuthTokenDto>.Success(result));
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<AuthTokenDto>>> Login([FromBody] LoginDto request)
        {
            var user = await _context.Users
                .Include(u => u.Wallet)
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
                return StatusCode(401, ServiceResponse<AuthTokenDto>.Failure("Invalid credentials."));


            var token = _tokenService.CreateToken(user);
            var result = new AuthTokenDto { Token = token };
            return Ok(ServiceResponse<AuthTokenDto>.Success(result));
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }
    }
}
