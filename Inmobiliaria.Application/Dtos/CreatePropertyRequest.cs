namespace Inmobiliaria.Application.Dtos;

public class CreatePropertyRequest
{
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public string Ubicacion { get; set; }
    // Las URLs de las imágenes se manejarán por separado más adelante.
}
