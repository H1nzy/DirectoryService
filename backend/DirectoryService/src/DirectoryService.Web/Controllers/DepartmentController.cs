using DirectoryService.Contracts;
using Microsoft.AspNetCore.Mvc;
using DirectoryService.Application;

namespace DirectoryService.Web;

[ApiController]
[Route("departments")]

public class DepartmentController : ControllerBase
{
    private readonly CreateDepartmentUseCase _createDepartmentUseCase;
    private readonly UpdateDepartmentUseCase _updateDepartmentUseCase;
    
    public DepartmentController(CreateDepartmentUseCase createDepartmentUseCase, UpdateDepartmentUseCase updateDepartmentUseCase)
    {
        _createDepartmentUseCase = createDepartmentUseCase;
        _updateDepartmentUseCase = updateDepartmentUseCase;

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

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDepartmentDto updateDepartmentDto, CancellationToken ct)
    {
        await _updateDepartmentUseCase.Handle(id, updateDepartmentDto, ct);
        return Ok(id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok();
        
    }
    
}