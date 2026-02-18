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
 * - Set up authentication (Identity, Google OAuth)
 * - Configure database context and connection string handling
 * - Register Razor components and middleware
 * - Initialize middleware pipeline
 * 
 * Technologies: ASP.NET Core Blazer, Entity Framework Core, Identity, Google Auth
 */

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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

// ===========================
// Configure Database Context
// ===========================

// Get the database connection string (required - will throw if not found)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Configure database based on environment (SQLite for dev, SQL Server for prod)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        // Use SQLite for development
        options.UseSqlite(connectionString);
    }
    else
    {
        // Use SQL Server for production
        options.UseSqlServer(connectionString);
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

// Configure external authentication (Google OAuth)
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? string.Empty;
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? string.Empty;
    });

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

/// <summary>
/// Validates and sanitizes return URL to prevent open redirect attacks.
/// Only allows URLs starting with "/" (relative URLs).
/// </summary>
static string GetSafeReturnUrl(string? returnUrl)
{
    if (string.IsNullOrWhiteSpace(returnUrl) || !returnUrl.StartsWith("/", StringComparison.Ordinal))
    {
        return "/";
    }

    return returnUrl;
}

// External login endpoint - initiates OAuth flow for external providers (Google)
app.MapGet("/account/external-login", (string provider, string? returnUrl, SignInManager<ApplicationUser> signInManager) =>
{
    var safeReturnUrl = GetSafeReturnUrl(returnUrl);
    var redirectUrl = $"/account/external-callback?returnUrl={Uri.EscapeDataString(safeReturnUrl)}";
    var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
    return Results.Challenge(properties, new[] { provider });
});

// External login callback endpoint - handles OAuth provider response
// This is called after the external provider (Google) authenticates the user
app.MapGet("/account/external-callback", async (string? returnUrl,
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager) =>
{
    var safeReturnUrl = GetSafeReturnUrl(returnUrl);
    var info = await signInManager.GetExternalLoginInfoAsync();
    if (info == null)
    {
        return Results.Redirect("/login?error=external");
    }

    // Try to sign in with existing external login
    var signInResult = await signInManager.ExternalLoginSignInAsync(
        info.LoginProvider,
        info.ProviderKey,
        isPersistent: false,
        bypassTwoFactor: true);

    if (signInResult.Succeeded)
    {
        return Results.Redirect(safeReturnUrl);
    }

    // If user doesn't exist, get email from external provider and create new user
    var email = info.Principal.FindFirstValue(ClaimTypes.Email);
    if (string.IsNullOrWhiteSpace(email))
    {
        return Results.Redirect("/login?error=external");
    }

    var user = await userManager.FindByEmailAsync(email);
    if (user == null)
    {
        // Create new user from external login information
        user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty,
            LastName = info.Principal.FindFirstValue(ClaimTypes.Surname) ?? string.Empty,
            EmailConfirmed = true
        };

        var createResult = await userManager.CreateAsync(user);
        if (!createResult.Succeeded)
        {
            return Results.Redirect("/login?error=external");
        }
    }

    var addLoginResult = await userManager.AddLoginAsync(user, info);
    if (!addLoginResult.Succeeded)
    {
        return Results.Redirect("/login?error=external");
    }

    await signInManager.SignInAsync(user, isPersistent: false);
    return Results.Redirect(safeReturnUrl);
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
