using Inmobiliaria.Application.DTOs.Properties;
using Inmobiliaria.Application.Interfaces;
using Inmobiliaria.Domain.Entities;
using Inmobiliaria.Domain.Interfaces;

namespace Inmobiliaria.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService(IPropertyRepository repository)
    {
        _propertyRepository = repository;
    }
    
    public async Task<IReadOnlyList<PropertyDto>> GetAll()
    {
        var properties = await _propertyRepository.GetAllAsync();

        return properties.Select(p => new PropertyDto
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            City = p.City,
            Address = p.Address,
            Price = p.Price,
            ImageUrl = p.ImageUrl,
            AdminId = p.AdminId
        }).ToList();
    }

    public async Task<PropertyDto?> GetById(Guid id)
    {
        var property = await _propertyRepository.GetByIdAsync(id);

        if (property == null) 
            return null;

        return new PropertyDto
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            City = property.City,
            Address = property.Address,
            Price = property.Price,
            ImageUrl = property.ImageUrl,
            AdminId = property.AdminId
        };
    }

    public async Task<PropertyDto> Create(PropertyCreateDto dto, string imageUrl)
    {
        var property = new Property
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            City = dto.City,
            Address = dto.Address,
            Price = dto.Price,
            AdminId = dto.AdminId,
            ImageUrl = imageUrl
        };

        await _propertyRepository.AddAsync(property);
        await _propertyRepository.SaveChangesAsync();
        
        return new PropertyDto
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            City = property.City,
            Address = property.Address,
            Price = property.Price,
            ImageUrl = property.ImageUrl,
            AdminId = property.AdminId
        };
    }

    public async Task<PropertyDto> Update(Guid id, PropertyUpdateDto dto, string? imageUrl)
    {
        var property = await _propertyRepository.GetByIdAsync(id);

        if (property == null)
            throw new Exception("La propiedad no existe");
        
        // Actualizamos solo lo enviado en el DTO
        if (!string.IsNullOrEmpty(dto.Title))
            property.Title = dto.Title;

        if (!string.IsNullOrEmpty(dto.Description))
            property.Description = dto.Description;

        if (!string.IsNullOrEmpty(dto.City))
            property.City = dto.City;

        if (!string.IsNullOrEmpty(dto.Address))
            property.Address = dto.Address;

        if (dto.Price.HasValue)
            property.Price = dto.Price.Value;

        // Si viene una imagen nueva reemplazar
        if (!string.IsNullOrEmpty(imageUrl))
            property.ImageUrl = imageUrl;

        _propertyRepository.UpdateAsync(property);
        await _propertyRepository.SaveChangesAsync();

        return new PropertyDto
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            City = property.City,
            Address = property.Address,
            Price = property.Price,
            ImageUrl = property.ImageUrl,
            AdminId = property.AdminId
        };
    }
    
    public async Task<bool> Delete(Guid id)
    {
        var property = await _propertyRepository.GetByIdAsync(id);

        if (property == null)
            return false;
        
        await _propertyRepository.DeleteAsync(id);
        await _propertyRepository.SaveChangesAsync();

        return true;
    }
}