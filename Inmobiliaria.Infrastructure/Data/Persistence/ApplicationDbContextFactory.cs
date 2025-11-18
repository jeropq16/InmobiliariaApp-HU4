using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Inmobiliaria.Infrastructure.Data.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Cuando se ejecuta desde la raíz de la solución, CurrentDirectory es la raíz.
        // La ruta correcta al proyecto de la API es relativa a la raíz de la solución.
        string basePath = Directory.GetCurrentDirectory();

        // Si la carpeta 'Inmobiliaria.Api' no está en la ruta base, subimos un nivel.
        // Esto hace que la búsqueda sea más robusta.
        while (!Directory.Exists(Path.Combine(basePath, "Inmobiliaria.Api")))
        {
            basePath = Directory.GetParent(basePath)?.FullName;
            if (basePath == null)
            {
                throw new DirectoryNotFoundException("No se pudo encontrar el directorio del proyecto 'Inmobiliaria.Api'.");
            }
        }
        
        var apiProjectPath = Path.Combine(basePath, "Inmobiliaria.Api");

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(apiProjectPath)
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        return new ApplicationDbContext(builder.Options);
    }
}