/*
 * ========================================
 * Student Project Planner - Application Entry Point
 * ========================================
 * This is the main application configuration file that sets up all services,
 * middleware, authentication, and database connections for the Student Project 
 * Planner web application.
 * 
 * Key Responsibilities:
 * - Configure dependency injection for services and repositories
 * - Set up authentication (Identity)
 * - Configure database context and connection string handling
 * - Register Razor components and middleware
 * - Initialize middleware pipeline
 * 
 * Technologies: ASP.NET Core Blazer, Entity Framework Core, Identity
 */

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentProjectPlanner.Components;
using StudentProjectPlanner.Data;
using StudentProjectPlanner.Models;
using StudentProjectPlanner.Repositories.Implementations;
using StudentProjectPlanner.Repositories.Interfaces;
using StudentProjectPlanner.Services.Implementations;
using StudentProjectPlanner.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ===========================
// Add Services to the Container
// ===========================

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add cascading authentication state
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
builder.Services.AddHttpContextAccessor();

// ===========================
// Configure External Services
// ===========================

// Configure OpenWeatherMap for weather functionality
builder.Services.Configure<OpenWeatherMapOptions>(
    builder.Configuration.GetSection(OpenWeatherMapOptions.SectionName));
builder.Services.AddHttpClient<IWeatherService, OpenWeatherMapService>();

// Configure email sender for password reset
builder.Services.Configure<EmailOptions>(
    builder.Configuration.GetSection(EmailOptions.SectionName));

// Use ConsoleEmailSender in development, SmtpEmailSender in production when configured
var emailConfig = builder.Configuration.GetSection(EmailOptions.SectionName).Get<EmailOptions>();
if (builder.Environment.IsDevelopment() || string.IsNullOrWhiteSpace(emailConfig?.Host))
{
    // Development mode: log emails to console instead of sending them
    builder.Services.AddTransient<IEmailSender, ConsoleEmailSender>();
}
else
{
    // Production mode: send real emails via SMTP
    builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();
}

// ===========================
// Configure Database Context
// ===========================

// Get the database connection string (required - will throw if not found)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Configure database - use SQLite for all environments (can be overridden with USE_SQLSERVER env var)
var useSqlServer = builder.Configuration.GetValue<bool>("USE_SQLSERVER", false);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (useSqlServer)
    {
        // Use SQL Server when explicitly configured
        options.UseSqlServer(connectionString);
    }
    else
    {
        // Use SQLite by default for all environments
        options.UseSqlite(connectionString);
    }
});

// ===========================
// Configure Identity & Authentication
// ===========================

// Configure Identity with strong password and lockout policies
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password settings - strong security requirements
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 4;

    // Lockout settings - protect against brute force attacks
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    // Sign-in settings
    options.SignIn.RequireConfirmedEmail = false; // Set to true in production with email service
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Add authorization
builder.Services.AddAuthorization();

// Configure application cookie settings for authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.AccessDeniedPath = "/access-denied";
});

// ===========================
// Register Repositories (Data Access Layer)
// ===========================

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IGroupProjectRepository, GroupProjectRepository>();

// ===========================
// Register Services (Business Logic Layer)
// ===========================

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IGroupProjectService, GroupProjectService>();

// ===========================
// Build Application & Initialize Database
// ===========================

var app = builder.Build();

// Initialize database on startup (run migrations and seed data)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await DbInitializer.MigrateAsync(context);
        await DbInitializer.InitializeAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while initializing the database.");
    }
}

// ===========================
// Configure HTTP Request Pipeline & Middleware
// ===========================

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // Production middleware
    app.UseExceptionHandler("/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}

// Add static file serving
app.UseStaticFiles();
app.UseRouting();

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Add antiforgery token validation for form submissions
app.UseAntiforgery();

// ===========================
// Map HTTP Endpoints for Authentication
// ===========================

// Logout endpoint - signs out the user and redirects to login page
app.MapGet("/account/logout", async (HttpContext context, SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Redirect("/login");
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
