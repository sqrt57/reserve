using backend.Config;
using backend.DbStores;
using backend.Models;
using backend.Security;
using backend.Services;
using Microsoft.AspNetCore.Identity;



var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddMarsConfiguration();
builder.Services.AddDbConnection();
builder.Services.AddMarsIdentity();

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