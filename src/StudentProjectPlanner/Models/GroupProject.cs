using System.ComponentModel.DataAnnotations;

namespace StudentProjectPlanner.Models;

/// <summary>
/// Represents a group project with multiple team members
/// </summary>
public class GroupProject
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    // Navigation properties
    public virtual ICollection<ProjectMember> Members { get; set; } = new List<ProjectMember>();
    public virtual ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();

    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets the total number of tasks in the project
    /// </summary>
    public int TotalTasks => Tasks.Count;

    /// <summary>
    /// Gets the number of completed tasks
    /// </summary>
    public int CompletedTasks => Tasks.Count(t => t.Status == TaskStatus.Completed);

    /// <summary>
    /// Gets the completion percentage
    /// </summary>
    public double CompletionPercentage => TotalTasks > 0 ? (double)CompletedTasks / TotalTasks * 100 : 0;
}
