using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server;
using Server.Data;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly.GetName().Name;
var connectionString = builder.Configuration.GetConnectionString("Default");

//SeedData.EnsureSeedData(connectionString);

builder.Services.AddDbContext<AspNetIdentityDbContext>(opt => opt.UseNpgsql(connectionString, b => b.MigrationsAssembly(assembly)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AspNetIdentityDbContext>();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(options => options.ConfigureDbContext = b => b.UseNpgsql(connectionString, opt => opt.MigrationsAssembly(assembly)))
    .AddOperationalStore(options => options.ConfigureDbContext = b => b.UseNpgsql(connectionString, opt => opt.MigrationsAssembly(assembly)))
    .AddDeveloperSigningCredential();

builder.Services.AddRazorPages();

// Add services to the container.
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();

app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

//app.MapDefaultControllerRoute();

app.Run();
