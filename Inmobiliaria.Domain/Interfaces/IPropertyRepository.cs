using Inmobiliaria.Domain.Entities;

namespace Inmobiliaria.Domain.Interfaces;

public interface IPropertyRepository
{
    Task<IReadOnlyList<Property>> GetAllAsync(); // IReadOnlyList sirve para solo leer (ver) sin poder modificar nada.
    Task<Property?> GetByIdAsync(Guid id);
    Task AddAsync(Property property);
    Task UpdateAsync(Property property);
    Task<bool> DeleteAsync(Guid id);
    Task SaveChangesAsync();
}