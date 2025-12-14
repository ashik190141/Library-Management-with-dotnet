using Library_Management.Interfaces;
using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;
namespace Library_Management.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        var response = await _userService.AddUserAsync(user);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var response = await _userService.GetAllUserAsync();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        var response = await _userService.GetUserByIdAsync(id);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UpdateUserDto user)
    {
        var response = await _userService.UpdateUserAsync(id, user);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        var response = await _userService.DeleteUserAsync(id);
        return Ok(response);
    }
}