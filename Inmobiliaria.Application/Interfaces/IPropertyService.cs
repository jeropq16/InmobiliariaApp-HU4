using Inmobiliaria.Domain.Entities;

namespace Inmobiliaria.Application.Interfaces;

public interface IPropertyService
{
    Task<IReadOnlyList<Property>> Get();
    Task<Property?> Get(Guid id);
    Task<Property> Create(Property property);
    Task<Property> Update(Property property);
    Task<bool> Delete(Guid id);
}