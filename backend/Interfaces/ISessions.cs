using System.Security.Claims;

namespace backend.Security;

public interface ISessions
{
    ClaimsPrincipal? Get(string id);
    string Create(ClaimsPrincipal principal);
    void Remove(string id);
}