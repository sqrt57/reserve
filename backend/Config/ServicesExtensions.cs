using backend.DbStores;
using backend.Models;
using backend.Security;
using backend.Services;

namespace backend.Config;

public static class ServicesExtensions
{
    public static void AddMarsConfiguration(this IConfigurationBuilder configurationBuilder)
    {
        // ReSharper disable once StringLiteralTypo
        var path = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "appsettings-secret.json");
        configurationBuilder.AddJsonFile(path);
    }    
    
    public static void AddDbConnection(this IServiceCollection services)
    {
        services.AddSingleton<DapperConnections>();
    }    
    
    public static void AddMarsIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(setup =>
            {
                setup.Password.RequiredLength = 3;
                setup.Password.RequiredUniqueChars = 1;
                setup.Password.RequireDigit = false;
                setup.Password.RequireLowercase = false;
                setup.Password.RequireUppercase = false;
                setup.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<ApplicationRole>()
            .AddUserStore<MarsUserStore>()
            .AddRoleStore<MarsRoleStore>();
    }
}