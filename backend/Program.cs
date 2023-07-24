using backend.Security;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = AuthSchemeConstants.MarsAuthScheme;
    })
    .AddScheme<MarsAuthSchemeOptions, MarsAuthHandler>(
        AuthSchemeConstants.MarsAuthScheme, options => { });
builder.Services.AddAuthorization();
builder.Services.AddMemoryCache();
builder.Services.Add(ServiceDescriptor.Singleton<ISessions, MemorySessions>());

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