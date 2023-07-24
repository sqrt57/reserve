using System.Security.Claims;
using backend.Dto;
using backend.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly ISessions _sessions;


    public AccountController(
        ILogger<AccountController> logger,
        ISessions sessions)
    {
        _logger = logger;
        _sessions = sessions;
    }

    [HttpPost]
    public IActionResult Login(LoginDataDto loginDataDto)
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

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var sessionId = _sessions.Create(claimsPrincipal);

            return Ok(new LoginResultDto {SessionId = sessionId,});
        }
        else
        {
            return Unauthorized(new LoginResultDto {Error = "Incorrect login/password"});
        }
    }

    [HttpPost]
    [Authorize]
    public IActionResult Logout()
    {
        var sessionId = HttpContext.Features.Get<IAuthenticateResultFeature>()?.AuthenticateResult?.Properties?.Items["SessionId"];
        if (sessionId != null)
            _sessions.Remove(sessionId);
        return Ok();
    }
}