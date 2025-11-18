using System;
using System.Collections.Generic;

namespace Inmobiliaria.Application.Dtos;

public class PropertyResponse
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public string Ubicacion { get; set; }
    public Guid AdministradorId { get; set; }
    public List<string> UrlsImagenes { get; set; }
}
