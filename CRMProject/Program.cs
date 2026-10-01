using System;
using System.Globalization;
using CRMProject.Data.Context;
using CRMProject.Data.Entities;
using CRMProject.Services.Implementations;
using CRMProject.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabani Baglantisi (DbContext)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Baglanti dizesi 'DefaultConnection' appsettings.json icinde bulunamadi.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Identity (Kimlik Dogrulama ve Yetkilendirme) Yapilandirmasi
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// 3. Giris ve Yetkilendirme Cookie Ayarlari
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
    options.SlidingExpiration = true;
});

// 4. Is Mantigi Servisleri (Dependency Injection)
builder.Services.AddScoped<ICariService, CariService>();
builder.Services.AddScoped<IMalzemeService, MalzemeService>();

// 5. MVC Servisleri
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 6. Veritabani Otomatik Seed Verisi (Roller, Admin, Ornek Veriler)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await DbInitializer.SeedAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Veritabani seed islemi sirasinda hata olustu.");
    }
}

// 7. Sayi ve Tarih Yerellestirme
var defaultCulture = new CultureInfo("tr-TR");
defaultCulture.NumberFormat.NumberDecimalSeparator = ".";
defaultCulture.NumberFormat.CurrencyDecimalSeparator = ".";
var supportedCultures = new[] { defaultCulture, new CultureInfo("en-US") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(defaultCulture),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// 8. HTTP Istek Hatti (Middleware Pipeline)
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
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
