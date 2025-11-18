using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Inmobiliaria.Application.Dtos;
using Inmobiliaria.Application.Interfaces;
using Inmobiliaria.Domain.Entities;
using Inmobiliaria.Infrastructure.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Inmobiliaria.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<string> Register(RegisterRequestDto registerRequest)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Email == registerRequest.Email);
        if (userExists)
        {
            throw new Exception("El usuario ya existe");
        }

        User user;
        if (registerRequest.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
        {
            user = new Admin
            {
                Name = registerRequest.Name,
                Email = registerRequest.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password),
                Role = "Admin"
            };
        }
        else
        {
            user = new Client
            {
                Name = registerRequest.Name,
                Email = registerRequest.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password),
                Role = "Client",
            };
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return GenerateToken(user);
    }

    public async Task<string> Login(LoginRequestDto loginRequest)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginRequest.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
        {
            throw new Exception("Credenciales inválidas");
        }

        return GenerateToken(user);
    }

    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
