using System.Security.Claims;
using backend.Dto;
using backend.Models;
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
[AllowAnonymous]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly ISessions _sessions;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;


    public AccountController(
        ILogger<AccountController> logger,
        ISessions sessions,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _sessions = sessions;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDataDto loginDataDto)
    {
        if (loginDataDto.Login == null || loginDataDto.Password == null)
            return Unauthorized(new ErrorResultDto {Error = "Empty login/password"});

        var user = await _userManager.FindByNameAsync(loginDataDto.Login);
        if (user == null)
            return Unauthorized(new ErrorResultDto {Error = "Incorrect login/password"});
        
        var properties = new AuthenticationProperties
        {
            IsPersistent = true,
            AllowRefresh = true,
        };
        await _signInManager.SignInAsync(user, properties,
            MarsAuthenticationDefaults.AuthenticationScheme);

        if (loginDataDto.Login == "admin" && loginDataDto.Password == "123")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginDataDto.Login),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, MarsAuthenticationDefaults.AuthenticationScheme);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var sessionId = _sessions.Create(claimsPrincipal);

            return Ok(new LoginResultDto {SessionId = sessionId,});
        }
        else
        {
            return Unauthorized(new ErrorResultDto {Error = "Incorrect login/password"});
        }
    }

    [HttpPost]
    public IActionResult Logout()
    {
        var sessionId = HttpContext.Features.Get<IAuthenticateResultFeature>()?.AuthenticateResult?.Properties
            ?.Items["SessionId"];
        if (sessionId != null)
        {
            _sessions.Remove(sessionId);
            return Ok();
        }
        else
        {
            return BadRequest(new ErrorResultDto() {Error = "No session id provided"});
        }
    }
}