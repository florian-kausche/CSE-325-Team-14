using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentProjectPlanner.Models;

namespace StudentProjectPlanner.Data;

/// <summary>
/// Database context for the Student Project Planner application.
/// Inherits from IdentityDbContext to include user authentication/authorization tables.
/// 
/// Configures all entity relationships and database constraints:
/// - Courses: User's enrolled courses
/// - Assignments: Individual assignments for courses
/// - GroupProjects: Collaborative group projects
/// - ProjectTasks: Tasks within group projects
/// - ProjectMembers: Membership of users in group projects
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// DbSets for all application entities.
    /// These represent tables in the database.
    /// </summary>
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Assignment> Assignments { get; set; } = null!;
    public DbSet<GroupProject> GroupProjects { get; set; } = null!;
    public DbSet<ProjectTask> ProjectTasks { get; set; } = null!;
    public DbSet<ProjectMember> ProjectMembers { get; set; } = null!;

    /// <summary>
    /// Configures entity relationships, constraints, and indexes for the models.
    /// Called when the model is being created.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // ===========================
        // Configure Course Entity
        // ===========================
        // Relationship: One user can have many courses
        // Cascade delete ensures courses are deleted when user is deleted
        builder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Courses)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure course code is unique per user (user can't have duplicate course codes)
            entity.HasIndex(e => new { e.UserId, e.CourseCode })
                .IsUnique();
        });

        // ===========================
        // Configure Assignment Entity
        // ===========================
        // Relationship: One course can have many assignments
        // Relationship: One user can have many assignments
        builder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.Id);

            // Cascade delete when course is deleted
            entity.HasOne(e => e.Course)
                .WithMany(c => c.Assignments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // No action on user delete to prevent cascade path issues
            entity.HasOne(e => e.User)
                .WithMany(u => u.Assignments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Add indexes for common query operations
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.CourseId);
            entity.HasIndex(e => e.DueDate); // For sorting by due date
        });

        // ===========================
        // Configure GroupProject Entity
        // ===========================
        builder.Entity<GroupProject>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        // ===========================
        // Configure ProjectTask Entity
        // ===========================
        // Relationship: One group project can have many tasks
        // Relationship: One user can have many assigned tasks
        builder.Entity<ProjectTask>(entity =>
        {
            entity.HasKey(e => e.Id);

            // Cascade delete tasks when project is deleted
            entity.HasOne(e => e.GroupProject)
                .WithMany(p => p.Tasks)
                .HasForeignKey(e => e.GroupProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Set null when assigned user is deleted (task becomes unassigned)
            entity.HasOne(e => e.AssignedUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(e => e.AssignedUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Add indexes for common queries
            entity.HasIndex(e => e.GroupProjectId);
            entity.HasIndex(e => e.AssignedUserId);
        });

        // ===========================
        // Configure ProjectMember Entity
        // ===========================
        // Relationship: One group project can have many members
        // Relationship: One user can be member of many group projects
        builder.Entity<ProjectMember>(entity =>
        {
            entity.HasKey(e => e.Id);

            // Cascade delete when project is deleted
            entity.HasOne(e => e.GroupProject)
                .WithMany(p => p.Members)
                .HasForeignKey(e => e.GroupProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cascade delete when user is deleted
            entity.HasOne(e => e.User)
                .WithMany(u => u.ProjectMemberships)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure a user can only be a member of a project once
            entity.HasIndex(e => new { e.GroupProjectId, e.UserId })
                .IsUnique();
        });
    }
}
