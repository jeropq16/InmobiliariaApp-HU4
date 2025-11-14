namespace Inmobiliaria.Domain.Entities;

public class Property
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }
    
    public string ImageUrl { get; set; } = string.Empty;
    
    public Guid? AdminId { get; set; }
    public Admin? Admin { get; set; }
}