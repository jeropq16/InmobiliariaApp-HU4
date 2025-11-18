using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Inmobiliaria.Application.Dtos;
using Inmobiliaria.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers;

[ApiController]
[Route("api/propiedades")]
public class PropiedadesController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropiedadesController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateProperty([FromBody] CreatePropertyRequest request)
    {
        // Extraemos el ID del admin (que ahora es un Guid) del token
        var adminId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var property = await _propertyService.CreatePropertyAsync(request, adminId);
        return CreatedAtAction(nameof(GetPropertyById), new { id = property.Id }, property);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProperty(Guid id, [FromBody] UpdatePropertyRequest request)
    {
        var property = await _propertyService.UpdatePropertyAsync(id, request);
        if (property == null)
        {
            return NotFound();
        }
        return Ok(property);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProperty(Guid id)
    {
        var success = await _propertyService.DeletePropertyAsync(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPropertyById(Guid id)
    {
        var property = await _propertyService.GetPropertyByIdAsync(id);
        if (property == null)
        {
            return NotFound();
        }
        return Ok(property);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllProperties()
    {
        var properties = await _propertyService.GetAllPropertiesAsync();
        return Ok(properties);
    }
}
