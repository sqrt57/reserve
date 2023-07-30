using System.Security.Claims;
using backend.Models;
using Dapper;
using Microsoft.AspNetCore.Identity;

namespace backend.DbStores;

public class MarsUserStore : UserStoreBase<ApplicationUser, int, ApplicationUserClaim, ApplicationUserLogin,
    ApplicationUserToken>
{
    private readonly DapperConnections _dapperConnections;

    public MarsUserStore(
        IdentityErrorDescriber describer,
        DapperConnections dapperConnections)
        : base(describer)
    {
        _dapperConnections = dapperConnections;
    }

    public override async Task<IdentityResult> CreateAsync(ApplicationUser user,
        CancellationToken cancellationToken = new CancellationToken())
    {
        const string query = @"
INSERT INTO [dbo].[MarsUsers]
        ([UserName]
        ,[IsActive]
        ,[NormalizedUserName]
        ,[Email]
        ,[NormalizedEmail]
        ,[EmailConfirmed]
        ,[PasswordHash]
        ,[SecurityStamp]
        ,[ConcurrencyStamp]
        ,[PhoneNumber]
        ,[PhoneNumberConfirmed]
        ,[TwoFactorEnabled]
        ,[LockoutEnd]
        ,[LockoutEnabled]
        ,[AccessFailedCount])
    VALUES
        (@UserName
        ,1
        ,@NormalizedUserName
        ,@Email
        ,@NormalizedEmail
        ,@EmailConfirmed
        ,@PasswordHash
        ,@SecurityStamp
        ,@ConcurrencyStamp
        ,@PhoneNumber
        ,@PhoneNumberConfirmed
        ,@TwoFactorEnabled
        ,@LockoutEnd
        ,@LockoutEnabled
        ,@AccessFailedCount)
";

        var parameters = new DynamicParameters();
        parameters.Add("UserName", user.UserName);
        parameters.Add("NormalizedUserName", user.NormalizedUserName);
        parameters.Add("Email", user.Email);
        parameters.Add("NormalizedEmail", user.NormalizedEmail);
        parameters.Add("EmailConfirmed", user.EmailConfirmed);
        parameters.Add("PasswordHash", user.PasswordHash);
        parameters.Add("SecurityStamp", user.SecurityStamp);
        parameters.Add("ConcurrencyStamp", user.ConcurrencyStamp);
        parameters.Add("PhoneNumber", user.PhoneNumber);
        parameters.Add("PhoneNumberConfirmed", user.PhoneNumberConfirmed);
        parameters.Add("TwoFactorEnabled", user.TwoFactorEnabled);
        parameters.Add("LockoutEnd", user.LockoutEnd);
        parameters.Add("LockoutEnabled", user.LockoutEnabled);
        parameters.Add("AccessFailedCount", user.AccessFailedCount);

        using var connection = await _dapperConnections.CreateAsync();
        var rowsAffected = await connection.ExecuteAsync(query, parameters);
        return rowsAffected > 0
            ? IdentityResult.Success
            : IdentityResult.Failed(new[]
            {
                new IdentityError
                {
                    Code = "DbInsertError",
                    Description = "Error inserting new user into [dbo].[MarsUsers] table",
                }
            });
    }

    public override async Task<IdentityResult> UpdateAsync(ApplicationUser user,
        CancellationToken cancellationToken = new CancellationToken())
    {
        const string query = @"
UPDATE [dbo].[MarsUsers] SET
	[UserName] = @UserName
    ,[IsActive] = 1
    ,[NormalizedUserName] = @NormalizedUserName
    ,[Email] = @Email
    ,[NormalizedEmail] = @NormalizedEmail
    ,[EmailConfirmed] = @EmailConfirmed
    ,[PasswordHash] = @PasswordHash
    ,[SecurityStamp] = @SecurityStamp
    ,[ConcurrencyStamp] = @ConcurrencyStamp
    ,[PhoneNumber] = @PhoneNumber
    ,[PhoneNumberConfirmed] = @PhoneNumberConfirmed
    ,[TwoFactorEnabled] = @TwoFactorEnabled
    ,[LockoutEnd] = @LockoutEnd
    ,[LockoutEnabled] = @LockoutEnabled
    ,[AccessFailedCount] = @AccessFailedCount
WHERE [Id] = @Id
";

        var parameters = new DynamicParameters();
        parameters.Add("Id", user.Id);
        parameters.Add("UserName", user.UserName);
        parameters.Add("NormalizedUserName", user.NormalizedUserName);
        parameters.Add("Email", user.Email);
        parameters.Add("NormalizedEmail", user.NormalizedEmail);
        parameters.Add("EmailConfirmed", user.EmailConfirmed);
        parameters.Add("PasswordHash", user.PasswordHash);
        parameters.Add("SecurityStamp", user.SecurityStamp);
        parameters.Add("ConcurrencyStamp", user.ConcurrencyStamp);
        parameters.Add("PhoneNumber", user.PhoneNumber);
        parameters.Add("PhoneNumberConfirmed", user.PhoneNumberConfirmed);
        parameters.Add("TwoFactorEnabled", user.TwoFactorEnabled);
        parameters.Add("LockoutEnd", user.LockoutEnd);
        parameters.Add("LockoutEnabled", user.LockoutEnabled);
        parameters.Add("AccessFailedCount", user.AccessFailedCount);

        using var connection = await _dapperConnections.CreateAsync();
        var rowsAffected = await connection.ExecuteAsync(query, parameters);
        return rowsAffected > 0
            ? IdentityResult.Success
            : IdentityResult.Failed(new[]
            {
                new IdentityError
                {
                    Code = "DbUpdateError",
                    Description = "Error updating user in [dbo].[MarsUsers] table",
                }
            });
    }

    public override Task<IdentityResult> DeleteAsync(ApplicationUser user,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task<ApplicationUser?> FindByIdAsync(string userId,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override async Task<ApplicationUser?> FindByNameAsync(string normalizedUserName,
        CancellationToken cancellationToken = new CancellationToken())
    {
        const string query = @"
SELECT * FROM [MarsUsers]
WHERE [NormalizedUserName] = @NormalizedUserName
AND IsActive = 1
";
        using var connection = await _dapperConnections.CreateAsync();
        return await connection.QuerySingleOrDefaultAsync<ApplicationUser>(query, new {normalizedUserName});
    }

    protected override Task<ApplicationUser?> FindUserAsync(int userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override Task<ApplicationUserLogin?> FindUserLoginAsync(int userId, string loginProvider,
        string providerKey, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override Task<ApplicationUserLogin?> FindUserLoginAsync(string loginProvider, string providerKey,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override Task<IList<Claim>> GetClaimsAsync(ApplicationUser user,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult<IList<Claim>>(new List<Claim>());
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

    public override Task<IList<ApplicationUser>> GetUsersForClaimAsync(Claim claim,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    protected override Task<ApplicationUserToken?> FindTokenAsync(ApplicationUser user, string loginProvider,
        string name, CancellationToken cancellationToken)
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

    public override Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override Task<ApplicationUser?> FindByEmailAsync(string normalizedEmail,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }
}
