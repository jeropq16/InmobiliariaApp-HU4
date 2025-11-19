using Inmobiliaria.Application.DTOs.Properties;

namespace Inmobiliaria.Application.Interfaces;

public interface IPropertyService
{
    Task<IReadOnlyList<PropertyDto>> GetAll();
    Task<PropertyDto?> GetById(Guid id);
    Task<PropertyDto> Create(PropertyCreateDto dto, string imageUrl);
    Task<PropertyDto> Update(Guid id, PropertyUpdateDto dto, string? imageUrl);
    Task<bool> Delete(Guid id);
}