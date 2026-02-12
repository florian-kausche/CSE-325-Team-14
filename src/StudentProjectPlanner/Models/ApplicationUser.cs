using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace StudentProjectPlanner.Models;

/// <summary>
/// Represents a user in the Student Project Planner application
/// Extends IdentityUser for authentication support
/// </summary>
public class ApplicationUser : IdentityUser
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    public virtual ICollection<ProjectMember> ProjectMemberships { get; set; } = new List<ProjectMember>();
    public virtual ICollection<ProjectTask> AssignedTasks { get; set; } = new List<ProjectTask>();

    /// <summary>
    /// Gets the full name of the user
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";
}
