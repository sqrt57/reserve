using Autofac;
using Autofac.Extensions.DependencyInjection;
using backend.Config;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace users;

public class Users
{
    private readonly IApplicationBuilder _app;

    public Users()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterModule<BusinessLayerModule>();
        });

        builder.Configuration.AddMarsConfiguration();
        builder.Services.AddMarsIdentity();

        _app = builder.Build();
    }

    public async Task Add(string login, string password, bool isAdmin)
    {
        using var scope = _app.ApplicationServices.CreateScope();
        var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
        if (userManager == null)
            throw new NullReferenceException();

        var user = new ApplicationUser
        {
            UserName = login,
        };
        var result = await userManager.CreateAsync(user, password);
        WriteIdentityResult(result, $"Added user {user}", "Error adding user:");
    }

    public async Task Change(string login, string password)
    {
        using var scope = _app.ApplicationServices.CreateScope();
        var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
        if (userManager == null)
            throw new NullReferenceException();

        var user = await userManager.FindByNameAsync(login);
        if (user == null)
        {
            WriteErrorLine($"Error: user {login} not found");
            return;
        }

        var removeResult = await userManager.RemovePasswordAsync(user);
        WriteIdentityResult(removeResult,
            $"Removed password for user {user}",
            "Error removing password:");

        var addResult = await userManager.AddPasswordAsync(user, password);
        WriteIdentityResult(addResult,
            $"Added new password for user {user}",
            "Error adding password:");

        if (user.LockoutEnd != null)
        {
            user.LockoutEnd = null;
            var updateResult = await userManager.UpdateAsync(user);
            WriteIdentityResult(updateResult, $"Unlocked user {login}",
                "Error unlocking user:");
        }
    }

    public async Task Unlock(string login)
    {
        using var scope = _app.ApplicationServices.CreateScope();
        var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
        if (userManager == null)
            throw new NullReferenceException();

        var user = await userManager.FindByNameAsync(login);
        if (user == null)
        {
            WriteErrorLine($"Error: user {login} not found");
            return;
        }

        user.LockoutEnd = null;
        var updateResult = await userManager.UpdateAsync(user);
        WriteIdentityResult(updateResult, $"Unlocked user {login}",
            "Error unlocking user:");
    }

    private static void WriteIdentityResult(IdentityResult result,
        string successMessage, string errorMessage)
    {
        if (result.Succeeded)
            WriteSuccessLine(successMessage);
        else
        {
            WriteErrorLine(errorMessage);
            foreach (var error in result.Errors)
                WriteErrorLine(error.Description);
        }
    }

    private static void WriteErrorLine(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private static void WriteSuccessLine(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
