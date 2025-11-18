namespace Inmobiliaria.Application.Dtos;

public class RegisterRequestDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Dni { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string? PhoneNumber { get; set; }
}
