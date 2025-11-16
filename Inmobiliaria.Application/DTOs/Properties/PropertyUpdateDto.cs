namespace Inmobiliaria.Application.DTOs.Properties;

public class PropertyUpdateDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public decimal? Price { get; set; }
}