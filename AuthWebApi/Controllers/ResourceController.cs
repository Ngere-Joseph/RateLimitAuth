using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ResourceController : ControllerBase
{
    [HttpGet("public-resource")]
    public IActionResult GetPublicResource()
    {
        return Ok("This is a public resource");
    }

    [HttpGet("private-resource")]
    [Authorize]
    public IActionResult GetProtectedResource()
    {
        return Ok("This is a protected resource");
    }

    [HttpGet("admin-resource")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAdminResource()
    {
        return Ok("This is an admin resource");
    }
}
