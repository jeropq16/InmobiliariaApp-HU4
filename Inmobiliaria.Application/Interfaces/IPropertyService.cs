using System;
using System.Collections.Generic;
using Inmobiliaria.Application.Dtos;

namespace Inmobiliaria.Application.Interfaces;

public interface IPropertyService
{
    Task<PropertyResponse> CreatePropertyAsync(CreatePropertyRequest request, Guid adminId);
    Task<PropertyResponse> UpdatePropertyAsync(Guid id, UpdatePropertyRequest request);
    Task<bool> DeletePropertyAsync(Guid id);
    Task<PropertyResponse> GetPropertyByIdAsync(Guid id);
    Task<IEnumerable<PropertyResponse>> GetAllPropertiesAsync();
}
