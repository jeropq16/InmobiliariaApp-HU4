using System.Security.Claims;
using Inmobiliaria.Application.Dtos;
using Inmobiliaria.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<TokenResponse>> Register([FromBody] RegisterRequestDto registerRequest)
        {
            try
            {
                var tokenString = await _authService.Register(registerRequest);
                return Ok(new TokenResponse { Token = tokenString });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginRequestDto loginRequest)
        {
            try
            {
                var tokenString = await _authService.Login(loginRequest);
                return Ok(new TokenResponse { Token = tokenString });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("profile")]
        [Authorize] // ¡Esta es la clave! Solo usuarios con un token válido pueden acceder.
        public IActionResult GetProfile()
        {
            // Gracias a [Authorize], podemos confiar en que el usuario está autenticado.
            // La información del usuario se extrae de los claims del token JWT.
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            return Ok(new { Id = userId, Email = userEmail, Role = userRole });
        }
    }
}
