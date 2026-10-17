using DirectoryService.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DirectoryService.Web;

[ApiController]
[Route("departments")]

public class DepartmentController : ControllerBase
{
    [HttpPost]

    public async Task<IActionResult> Create([FromBody] CreateDepartmentDto createDepartmentDto)
    {
        return Ok(Guid.NewGuid());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(Array.Empty<object>());
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDepartmentDto updateDepartmentDto)
    {
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok();
        
    }
    
}