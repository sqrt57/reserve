using backend.Exceptions;
using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.Services;

public class UserIdAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserIdAccessor(
        IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public int GetUserId()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
            throw new UserIdNotAvailableException();
        var stringId = _userManager.GetUserId(httpContext.User);
        if (stringId == null)
            throw new UserIdNotAvailableException();
        if (!int.TryParse(stringId, out var id))
            throw new UserIdNotAvailableException();
        return id;
    }
}
