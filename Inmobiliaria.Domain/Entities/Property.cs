using System;
using System.Collections.Generic;

namespace Inmobiliaria.Domain.Entities;

public class Property
{
    public Guid Id { get; private set; }

    public string Titulo { get; private set; } = string.Empty;
    public string Descripcion { get; private set; } = string.Empty;

    public decimal Precio { get; private set; } = 0m;
    public string Ubicacion { get; private set; } = string.Empty;

    public List<string> UrlsImagenes { get; private set; } = new List<string>();
    
    public Guid AdministradorId { get; private set; }
    public Admin Administrador { get; private set; } = null!;
    
    //Constructor para inicializar el Id y asegurar la integridad 
    public Property()
    {
        Id=Guid.NewGuid();
    }
    
    
    // Método de fábrica o constructor para crear la propiedad (DDDD/Inmutabilidad)
    public Property(Guid administradorId, string titulo, string descripcion, decimal precio, string ubicacion,
        List<string> urlsImagenes) : this()
    {
        AdministradorId = administradorId;
        Titulo = titulo;
        Descripcion = descripcion;
        Precio = precio;
        Ubicacion = ubicacion;
        UrlsImagenes = urlsImagenes;
    }
    // Ejemplo de método para modificar (Asegura que la lógica de negocio esté en el Dominio)
    public void UpdateDetails(string titulo, string descripcion, decimal precio, string ubicaciion)
    {
        Titulo = titulo;
        Descripcion = descripcion;
        Precio = precio;
        Ubicacion = ubicaciion;
    }
    
}