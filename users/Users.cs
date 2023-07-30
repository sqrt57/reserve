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
