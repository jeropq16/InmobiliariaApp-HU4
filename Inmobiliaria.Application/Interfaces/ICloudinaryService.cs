using Microsoft.AspNetCore.Http;

namespace Inmobiliaria.Application.Interfaces;

public interface ICloudinaryService
{
    Task<string> UploadImageAsync(IFormFile file);
}