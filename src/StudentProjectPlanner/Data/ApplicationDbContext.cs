using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentProjectPlanner.Models;

namespace StudentProjectPlanner.Data;

/// <summary>
/// Database context for the Student Project Planner application
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets for application entities
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Assignment> Assignments { get; set; } = null!;
    public DbSet<GroupProject> GroupProjects { get; set; } = null!;
    public DbSet<ProjectTask> ProjectTasks { get; set; } = null!;
    public DbSet<ProjectMember> ProjectMembers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure Course entity
        builder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Courses)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.UserId, e.CourseCode })
                .IsUnique();
        });

        // Configure Assignment entity
        builder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.Course)
                .WithMany(c => c.Assignments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Assignments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascade path

            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.CourseId);
            entity.HasIndex(e => e.DueDate);
        });

        // Configure GroupProject entity
        builder.Entity<GroupProject>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        // Configure ProjectTask entity
        builder.Entity<ProjectTask>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.GroupProject)
                .WithMany(p => p.Tasks)
                .HasForeignKey(e => e.GroupProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.AssignedUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(e => e.AssignedUserId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasIndex(e => e.GroupProjectId);
            entity.HasIndex(e => e.AssignedUserId);
        });

        // Configure ProjectMember entity
        builder.Entity<ProjectMember>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.GroupProject)
                .WithMany(p => p.Members)
                .HasForeignKey(e => e.GroupProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany(u => u.ProjectMemberships)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.GroupProjectId, e.UserId })
                .IsUnique();
        });
    }
}
