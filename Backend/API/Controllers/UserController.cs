using Aplication.DTOs.Users;
using Aplication.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController: ControllerBase
{
    public readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromForm] UserRequestDTO userRequest)
    {

        if (userRequest.Photo == null || userRequest.Photo.Length == 0)
            return BadRequest("No file uploaded.");
        try
        {
            var user = await _userService.CreateUserAsync(userRequest);
        
            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var res = await _userService.GetByIdAsync(id);
            return Ok(res);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [Authorize]
    [HttpGet("publicProfile")]
    public async Task<IActionResult> GetPublicProfileInfoAsync()
    {
        try
        {
            var res = await _userService.GetPublicProfileInfoAsync();
            return Ok(res);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [Authorize]
    [HttpGet("get")]
    public async Task<IActionResult> GetUser()
    {
        try
        {
            var res = await _userService.GetUser();
            return Ok(res);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromForm] UserProfileRequestDTO userRequest)
    {
        if (userRequest.ProfilePic == null || userRequest.ProfilePic.Length == 0)
            return BadRequest("No file uploaded.");

        try 
        {
            var res = await _userService.UpdateAsync(id, userRequest);

            return Ok(res);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
    
    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateProfileAsync([FromForm] UserUpdateProfileRequestDTO userRequest)
    {
        try 
        {
            var res = await _userService.UpdateProfileAsync(userRequest);
            return Ok(res);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpPatch("password")]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] UserNewPasswordRequestDTO user)
    {
        try
        {
            var res = await _userService.ChangePasswordAsync(user.Id, user.Password);
            return Ok(res);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}