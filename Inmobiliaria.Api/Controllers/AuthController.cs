using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]//La ruta es /api/Auth
    
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] object registerRequest)
        {
            //logica de registro(guardar en DB)
            return Ok(new { Message = "Registro exitoso" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] object loginRequest)
        {
            //Inicio de sesion
            return Ok(new { Message = "Inicio de sesion exitoso" });
        }
    }
}