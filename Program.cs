using GameStoreMVC.Data;
using GameStoreMVC.Interfaces;
using GameStoreMVC.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(connectionString!));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login";
        options.LogoutPath = "/Login/Logout";
        options.AccessDeniedPath = "/Login/AcessoNegado";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    var usuarioRepo = scope.ServiceProvider.GetRequiredService<IUsuarioRepository>();
    var adminEmail = "admin@gamestore.com";
    if (!await usuarioRepo.EmailExisteAsync(adminEmail))
    {
        await usuarioRepo.AdicionarAsync(new GameStoreMVC.Models.Usuario
        {
            Nome = "Administrador",
            Email = adminEmail,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            IsAdmin = true
        });
        await usuarioRepo.SalvarAsync();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
