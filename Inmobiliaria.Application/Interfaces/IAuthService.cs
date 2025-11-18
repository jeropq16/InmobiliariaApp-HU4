using Inmobiliaria.Application.Dtos;

namespace Inmobiliaria.Application.Interfaces;

public interface IAuthService
{
    Task<string> Register(RegisterRequestDto registerRequest);
    Task<string> Login(LoginRequestDto loginRequest);
}
