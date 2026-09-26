using Microsoft.EntityFrameworkCore;
using GameVault.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configurar Serilog para guardar los logs en un archivo de texto en la carpeta /Logs/
Log.Logger = new LoggerConfiguration()
.MinimumLevel.Information()
.WriteTo.Console()
.WriteTo.File("Logs/gamevault-.txt", rollingInterval: RollingInterval.Day)
.CreateLogger();

// Decirle a ASP.NET Core que use Serilog como su sistema de logs principal
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TiendaDbContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("ConexionTienda")));

//Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();