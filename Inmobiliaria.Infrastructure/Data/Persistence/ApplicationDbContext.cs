using Microsoft.EntityFrameworkCore;
using Inmobiliaria.Domain.Entities;

namespace Inmobiliaria.Infrastructure.Data.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Property> Properties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //  Configuración de la Herencia de User (TPT - Table Per Type) by deivis ;D
        // Esto crea una tabla 'User' base y tablas separadas para 'Admin' y 'Client'
        modelBuilder.Entity<User>()
            .UseTptMappingStrategy();
        
        modelBuilder.Entity<Admin>().ToTable("Admins");
        modelBuilder.Entity<Client>().ToTable("Clients");

        modelBuilder.Entity<Property>(entity =>
        {
            entity.Property(p => p.UrlsImagenes)
                .HasColumnType("json");

            //relacion 1:N (admin a propiedad)
            entity.HasOne(p => p.Administrador)
                .WithMany()
                .HasForeignKey(p => p.AdministradorId)
                .IsRequired();
        });
    }
}