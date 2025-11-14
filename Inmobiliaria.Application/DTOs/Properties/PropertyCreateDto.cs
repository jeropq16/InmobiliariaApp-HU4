namespace Inmobiliaria.Application.DTOs.Properties;

public class PropertyCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public Guid? AdminId { get; set; }
    
    public IFormFile? Image { get; set; }
}