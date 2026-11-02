using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Web;
using DirectoryService.Application;

[ApiController]
[Route("departments")]
public sealed class DepartmentLocationController : ControllerBase
{
    private readonly LinkLocationUseCase _linkLocationUseCase;
    private readonly UnLinkLocationUseCase _unLinkLocationUseCase;

    public DepartmentLocationController(
        LinkLocationUseCase linkLocationUseCase,
        UnLinkLocationUseCase unLinkLocationUseCase)
    {
        _linkLocationUseCase = linkLocationUseCase;
        _unLinkLocationUseCase = unLinkLocationUseCase;
    }

    [HttpPost("{departmentId:guid}/locations/{locationId:guid}")]
    public async Task<IActionResult> LinkLocation(
        [FromRoute] Guid departmentId,
        [FromRoute] Guid locationId,
        CancellationToken ct)
    {
        var id = await _linkLocationUseCase.Create(departmentId, locationId, ct);
        return Ok(id);
    }

    [HttpDelete("{departmentId:guid}/locations/{locationId:guid}")]
    public async Task<IActionResult> UnLinkLocation(
        [FromRoute] Guid departmentId,
        [FromRoute] Guid locationId,
        CancellationToken ct)
    {
        await _unLinkLocationUseCase.Delete(departmentId, locationId, ct);
        return NoContent();
    }
}