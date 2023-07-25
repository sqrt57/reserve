using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Extensions.Caching.Memory;

namespace backend.Security;

public class MemorySessions : ISessions
{
    private readonly IMemoryCache _memoryCache;
    private readonly TimeSpan _expiration = TimeSpan.FromMinutes(10);

    public MemorySessions(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    
    public ClaimsPrincipal? Get(string id)
    {
        _memoryCache.TryGetValue<ClaimsPrincipal>(id, out var principal);
        return principal;
    }

    public string Create(ClaimsPrincipal principal)
    {
        var key = Guid.NewGuid().ToString();
        _memoryCache.Set(key, principal, _expiration);
        return key;
    }

    public void Remove(string id)
    {
        _memoryCache.Remove(id);
    }
}