using System.Security.Claims;
using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.Services;

public class MarsUserStore : UserStoreBase<ApplicationUser, int, ApplicationUserClaim, ApplicationUserLogin, ApplicationUserToken>
{
    public MarsUserStore(IdentityErrorDescriber describer) : base(describer)
    {
    }

    public override Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task<ApplicationUser?> FindByIdAsync(string userId, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task<ApplicationUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    protected override Task<ApplicationUser?> FindUserAsync(int userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override Task<ApplicationUserLogin?> FindUserLoginAsync(int userId, string loginProvider, string providerKey, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override Task<ApplicationUserLogin?> FindUserLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override Task<IList<Claim>> GetClaimsAsync(ApplicationUser user, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task AddClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task ReplaceClaimAsync(ApplicationUser user, Claim claim, Claim newClaim,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task RemoveClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task<IList<ApplicationUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    protected override Task<ApplicationUserToken?> FindTokenAsync(ApplicationUser user, string loginProvider, string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override Task AddUserTokenAsync(ApplicationUserToken token)
    {
        throw new NotImplementedException();
    }

    protected override Task RemoveUserTokenAsync(ApplicationUserToken token)
    {
        throw new NotImplementedException();
    }

    public override IQueryable<ApplicationUser> Users => throw new NotImplementedException();

    public override Task AddLoginAsync(ApplicationUser user, UserLoginInfo login,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task RemoveLoginAsync(ApplicationUser user, string loginProvider, string providerKey,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task<ApplicationUser?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }
}