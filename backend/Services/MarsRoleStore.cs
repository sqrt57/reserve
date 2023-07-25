using System.Security.Claims;
using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.Security;

public class MarsRoleStore : RoleStoreBase<ApplicationRole, int, ApplicationUserRole, ApplicationRoleClaim>
{
    public MarsRoleStore(IdentityErrorDescriber describer) : base(describer)
    {
    }

    public override Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task<ApplicationRole?> FindByIdAsync(string id, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task<ApplicationRole?> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task<IList<Claim>> GetClaimsAsync(ApplicationRole role, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task AddClaimAsync(ApplicationRole role, Claim claim, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task RemoveClaimAsync(ApplicationRole role, Claim claim, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override IQueryable<ApplicationRole> Roles => throw new NotImplementedException();
}