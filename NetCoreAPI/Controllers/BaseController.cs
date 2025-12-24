using NetCoreAPI.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected IActionResult HandleResult<T>(ServiceResult<T> result, int? successStatusCode = null)
    {
        if (result == null)
            return NotFound();

        if (!result.Success)
            return BadRequest(new { error = result.ErrorMessage });

        return successStatusCode.HasValue ? StatusCode(successStatusCode.Value, result.Data) : Ok(result.Data);
    }

}