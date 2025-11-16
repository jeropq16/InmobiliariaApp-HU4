using Inmobiliaria.Application.DTOs.Properties;
using Inmobiliaria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly ICloudinaryService _cloudinaryService;

    public PropertyController(
        IPropertyService propertyService,
        ICloudinaryService cloudinaryService)
    {
        _propertyService = propertyService;
        _cloudinaryService = cloudinaryService;
    }
    
    // GET: api/properties
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var properties = await _propertyService.GetAll();
        return Ok(properties);
    }

    // GET: api/properties/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var property = await _propertyService.GetById(id);

        if (property == null)
            return NotFound(new { message = "La propiedad no existe" });

        return Ok(property);
    }
    
    // POST: api/properties (con imagen)
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] PropertyCreateDto dto, IFormFile? image)
    {
        string imageUrl = string.Empty;

        if (image != null)
        {
            imageUrl = await _cloudinaryService.UploadImageAsync(image);
        }

        var created = await _propertyService.Create(dto, imageUrl);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    
    // PUT: api/properties/{id} (con imagen opcional)
    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update(Guid id, [FromForm] PropertyUpdateDto dto, IFormFile? image)
    {
        string? imageUrl = null;

        if (image != null)
        {
            imageUrl = await _cloudinaryService.UploadImageAsync(image);
        }

        try
        {
            var updated = await _propertyService.Update(id, dto, imageUrl);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    // DELETE: api/properties/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _propertyService.Delete(id);

        if (!deleted)
            return NotFound(new { message = "La propiedad no existe" });

        return NoContent();
    }
}
