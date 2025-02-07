using Business.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MySolution.WebAPI.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : Controller
{
    private IUsers _users { get; set; }

    public UserController(IUsers users)
    {
        _users = users;
    }


    [HttpGet]
    [Route("get_all")]
    public async Task<IActionResult> GetListUser()
    {
        var data = await _users.GetAll();
        return Ok(data);
    }
}