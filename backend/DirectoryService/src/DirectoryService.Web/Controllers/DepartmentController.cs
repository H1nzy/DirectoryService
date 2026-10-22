using DirectoryService.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using DirectoryService.Application;

namespace DirectoryService.Web;

[ApiController]
[Route("departments")]

public class DepartmentController : ControllerBase
{
    private readonly CreateDepartmentUseCase _createDepartmentUseCase;
    
    public DepartmentController(CreateDepartmentUseCase createDepartmentUseCase)
    {
        _createDepartmentUseCase = createDepartmentUseCase;
    }
    [HttpPost]

    public async Task<IActionResult> Create([FromBody] CreateDepartmentDto createDepartmentDto, CancellationToken ct)
    {
        var department = await _createDepartmentUseCase.CreateAsync(createDepartmentDto, ct);
        Guid id = department.Id;
        return Ok(id);
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