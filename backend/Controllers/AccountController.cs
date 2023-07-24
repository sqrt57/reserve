using System.Security.Claims;
using backend.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;


    public AccountController(
        ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDataDto loginDataDto)
    {
        if (loginDataDto.Login == null || loginDataDto.Password == null)
            return Unauthorized(new LoginResultDto {Error = "Empty login/password"});

        if (loginDataDto.Login == "admin" && loginDataDto.Password == "123")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginDataDto.Login),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
            };
            
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), 
                authProperties);
            
            return Ok(new {loggedInAs = "admin"});
        }
        else
        {
            return Unauthorized(new LoginResultDto {Error = "Incorrect login/password"});
        }
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok();
    }
}