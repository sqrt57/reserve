using backend.Config;
using backend.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace users;

public class Users
{
    private readonly IApplicationBuilder _app;

    public Users(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddMarsConfiguration();
        builder.Services.AddDbConnection();
        builder.Services.AddMarsIdentity();

        _app = builder.Build();
    }

    public async Task Add(string login, string password, bool isAdmin)
    {
        using var scope = _app.ApplicationServices.CreateScope();
        var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
        var user = new ApplicationUser
        {
            UserName = login,
        };
        var result = await userManager!.CreateAsync(user, password);
        if (result.Succeeded)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Added user {user}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error adding user:");
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.Description);
                Console.ResetColor();
            }
        }
    }
}