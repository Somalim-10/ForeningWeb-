using ForeningWeb.Data;
using ForeningWeb.Security;
using ForeningWeb.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ---------- Logging (Serilog) ----------
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // ls fra appsettings.json
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog();

// ---------- Services ----------
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<ForeningWeb.Services.DonationService>();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Custom services
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<DonationService>();
builder.Services.AddScoped<KontaktService>();
builder.Services.AddScoped<OmService>();

// Admin options (ngle fra appsettings)
builder.Services.Configure<AdminOptions>(
    builder.Configuration.GetSection("Admin"));

// Health checks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>("db");

// Razor Pages + beskyt hele /Admin-mappen
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin", "AdminOnly");
});

// ---------- Authentication & Authorization ----------
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login";
        options.LogoutPath = "/Admin/Logout";
        options.AccessDeniedPath = "/Admin/Login";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(12);
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim("role", "admin"));
});

// ---------- App pipeline ----------
var app = builder.Build();

SeedData.Initialize(app.Services);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization(); // <--- Manglede

app.MapRazorPages();    // <--- Manglede

app.Run();
