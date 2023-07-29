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
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(
        ILogger<AccountController> logger,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDataDto loginDataDto)
    {
        if (loginDataDto.Login == null || loginDataDto.Password == null)
            return BadRequest(new ErrorResultDto {Error = "Empty login/password"});

        var user = await _userManager.FindByNameAsync(loginDataDto.Login);
        if (user == null)
            return BadRequest(new ErrorResultDto {Error = "Incorrect login/password"});

        var signInResult = await _signInManager.PasswordSignInAsync(
            user, loginDataDto.Password, true, true);
        
        if (signInResult.Succeeded)
            return Ok();
        
        if (signInResult.IsLockedOut)
            return BadRequest(new ErrorResultDto {Error = "User locked out"});
        if (signInResult.IsNotAllowed)
            return BadRequest(new ErrorResultDto {Error = "Login not allowed"});
        return BadRequest(new ErrorResultDto {Error = "Incorrect login/password"});
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }
}