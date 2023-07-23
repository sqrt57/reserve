using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Index(LoginData loginData)
    {
        if (loginData.Login == "admin" && loginData.Password == "123")
        {
            return Ok(new {loggedInAs = "admin"});
        }
        else
        {
            return Unauthorized(new {error = "Incorrect login/password"});
        }
    }
}

public class LoginData
{
    public string Login { get; set; }
    public string Password { get; set; }
}