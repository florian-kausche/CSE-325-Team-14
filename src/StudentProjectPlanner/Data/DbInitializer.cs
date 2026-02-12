using Microsoft.EntityFrameworkCore;
using StudentProjectPlanner.Models;

namespace StudentProjectPlanner.Data;

/// <summary>
/// Seeds initial data into the database
/// </summary>
public static class DbInitializer
{
    /// <summary>
    /// Initializes the database with seed data
    /// </summary>
    public static async Task InitializeAsync(ApplicationDbContext context)
    {
        // Ensure database is created
        await context.Database.EnsureCreatedAsync();

        // Check if data already exists
        if (await context.Courses.AnyAsync())
        {
            return; // Database has been seeded
        }

        // Add seed data here if needed
        // For now, we'll leave it empty and let users create their own data
    }

    /// <summary>
    /// Applies any pending migrations
    /// </summary>
    public static async Task MigrateAsync(ApplicationDbContext context)
    {
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            await context.Database.MigrateAsync();
        }
    }
}
