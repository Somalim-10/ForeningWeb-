using ForeningWeb.Data;
using ForeningWeb.Security;
using ForeningWeb.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ---------- Logging (Serilog) ----------
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // læs fra appsettings.json
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog();

// ---------- Services ----------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<EventService>();

builder.Services.Configure<AdminOptions>(
    builder.Configuration.GetSection("Admin"));

builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>("db");

builder.Services.AddRazorPages();

// ---------- App pipeline ----------
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSerilogRequestLogging();   // request logs (URL, status, ms)
app.MapHealthChecks("/health");   // health endpoint

app.MapRazorPages();

app.Run();
