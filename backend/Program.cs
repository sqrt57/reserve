using backend.Models;
using backend.Security;
using backend.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = MarsAuthenticationDefaults.AuthenticationScheme;
    })
    .AddScheme<MarsAuthenticationSchemeOptions, MarsAuthenticationHandler>(
        MarsAuthenticationDefaults.AuthenticationScheme, options => { });
builder.Services.AddAuthorization();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ISessions, MemorySessions>();
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddUserStore<MarsUserStore>()
    .AddRoleStore<MarsRoleStore>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();