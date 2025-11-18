namespace Inmobiliaria.Application.Dtos;

public class UpdatePropertyRequest
{
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public string Ubicacion { get; set; }
}
