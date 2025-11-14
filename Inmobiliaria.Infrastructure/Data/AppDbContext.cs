using Inmobiliaria.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Property> Properties { get; set; }
}