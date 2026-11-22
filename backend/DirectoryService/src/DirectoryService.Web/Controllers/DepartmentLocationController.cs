using Microsoft.AspNetCore.Mvc;
using DirectoryService.Application;

namespace DirectoryService.Web;

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
        var result = await _linkLocationUseCase.Create(departmentId, locationId, ct);
        return result.IsFailure ? result.Error.ToResponse() : Ok(result.Value);
    }

    [HttpDelete("{departmentId:guid}/locations/{locationId:guid}")]
    public async Task<IActionResult> UnLinkLocation(
        [FromRoute] Guid departmentId,
        [FromRoute] Guid locationId,
        CancellationToken ct)
    {
        var result = await _unLinkLocationUseCase.Delete(departmentId, locationId, ct);
        return result.IsFailure ? result.Error.ToResponse() : NoContent();
    }
}