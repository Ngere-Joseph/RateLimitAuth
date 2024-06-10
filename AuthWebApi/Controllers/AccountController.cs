using AuthWebApi.AuthWebApi.Application.LoginUser;
using AuthWebApi.AuthWebApi.Application.RegisterUser;
using AuthWebApi.AuthWebApi.Application.RoleUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateRegisterCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.Succeeded)
        {
            return Ok(new { Message = "User registered successfully" });
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] CreateLoginCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.Succeeded)
        {
            return Ok(new { Message = "Login successful" });
        }
        return Unauthorized();
    }

    [HttpPost("assign-role")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.Succeeded)
        {
            return Ok(new { Message = "Role assigned successfully" });
        }

        return BadRequest(result.Errors);
    }
}
