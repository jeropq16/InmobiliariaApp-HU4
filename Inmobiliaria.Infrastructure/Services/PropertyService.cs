using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inmobiliaria.Application.Dtos;
using Inmobiliaria.Application.Interfaces;
using Inmobiliaria.Domain.Entities;
using Inmobiliaria.Infrastructure.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria.Infrastructure.Services;

public class PropertyService : IPropertyService
{
    private readonly ApplicationDbContext _context;

    public PropertyService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyResponse> CreatePropertyAsync(CreatePropertyRequest request, Guid adminId)
    {
        // Usamos el constructor de la entidad para crear la propiedad, respetando el DDD
        var property = new Property(
            adminId,
            request.Titulo,
            request.Descripcion,
            request.Precio,
            request.Ubicacion,
            new List<string>() // La lista de imágenes se inicia vacía
        );

        _context.Properties.Add(property);
        await _context.SaveChangesAsync();

        return MapToResponse(property);
    }

    public async Task<PropertyResponse> UpdatePropertyAsync(Guid id, UpdatePropertyRequest request)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property == null)
        {
            return null;
        }

        // Usamos el método del dominio para actualizar, manteniendo la lógica de negocio encapsulada
        property.UpdateDetails(
            request.Titulo,
            request.Descripcion,
            request.Precio,
            request.Ubicacion
        );

        await _context.SaveChangesAsync();
        return MapToResponse(property);
    }

    public async Task<bool> DeletePropertyAsync(Guid id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property == null)
        {
            return false;
        }

        _context.Properties.Remove(property);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<PropertyResponse> GetPropertyByIdAsync(Guid id)
    {
        var property = await _context.Properties.FindAsync(id);
        return property == null ? null : MapToResponse(property);
    }

    public async Task<IEnumerable<PropertyResponse>> GetAllPropertiesAsync()
    {
        return await _context.Properties
            .Select(p => MapToResponse(p))
            .ToListAsync();
    }

    // Método privado para mapear la entidad a un DTO de respuesta
    private PropertyResponse MapToResponse(Property property)
    {
        return new PropertyResponse
        {
            Id = property.Id,
            Titulo = property.Titulo,
            Descripcion = property.Descripcion,
            Precio = property.Precio,
            Ubicacion = property.Ubicacion,
            AdministradorId = property.AdministradorId,
            UrlsImagenes = property.UrlsImagenes ?? new List<string>()
        };
    }
}
