using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace backend.Security;

public class MarsAuthHandler : AuthenticationHandler<MarsAuthSchemeOptions>
{
    private readonly ISessions _sessions;

    public MarsAuthHandler(
        IOptionsMonitor<MarsAuthSchemeOptions> options,
        ILoggerFactory logger, 
        UrlEncoder encoder,
        ISystemClock clock,
        ISessions sessions)
        : base(options, logger, encoder, clock)
    {
        _sessions = sessions;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(HeaderNames.Authorization, out var headerValues))
            return Task.FromResult(AuthenticateResult.Fail("Authorization header not found"));
        if (headerValues.Count != 1)
            return Task.FromResult(AuthenticateResult.Fail("Multiple authorization headers found"));
        var headerString = headerValues.ToString();
        if (!headerString.StartsWith("Bearer "))
            return Task.FromResult(AuthenticateResult.Fail("Only Bearer authentication is supported"));
        var tokenString = headerString.Substring(7);
        var bytes = Convert.FromBase64String(tokenString);
        var token = JsonSerializer.Deserialize<TokenDto>(bytes, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
        if (token == null)
            return Task.FromResult(AuthenticateResult.Fail("Session not found"));
        var principal = _sessions.Get(token.SessionId);
        if (principal == null)
            return Task.FromResult(AuthenticateResult.Fail("Session not found"));

        var properties = new AuthenticationProperties(
            new Dictionary<string, string?>{ ["SessionId"] = token.SessionId, });
        
        var ticket = new AuthenticationTicket(principal, properties, Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}