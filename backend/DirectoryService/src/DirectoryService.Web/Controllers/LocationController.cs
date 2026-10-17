using DirectoryService.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DirectoryService.Web;

[ApiController]
[Route("locations")]
public class LocationController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLocationDto createLocationDto)
    {
        return Ok(Guid.NewGuid());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return NotFound();
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Array.Empty<object>());
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]UpdateLocationDto updateLocationDto)
    {
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
     public async Task<IActionResult> Delete(Guid id)
    {
        return Ok();
    }
}