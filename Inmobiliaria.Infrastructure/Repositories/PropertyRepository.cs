using Inmobiliaria.Domain.Entities;
using Inmobiliaria.Domain.Interfaces;
using Inmobiliaria.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly AppDbContext _context;

    public PropertyRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IReadOnlyList<Property>> GetAllAsync()
    {
        return await _context.Properties.ToListAsync();
    }

    public async Task<Property?> GetByIdAsync(Guid id)
    {
        return await _context.Properties.FindAsync(id);
    }

    public async Task AddAsync(Property property)
    {
        await _context.Properties.AddAsync(property);
    }

    public async Task UpdateAsync(Property property)
    {
        _context.Update(property);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property == null) return false;
        _context.Properties.Remove(property);
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}