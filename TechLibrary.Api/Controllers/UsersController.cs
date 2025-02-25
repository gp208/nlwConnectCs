using Microsoft.AspNetCore.Mvc;

namespace TechLibrary.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult Register()
    {
        return Created();
    }
}
