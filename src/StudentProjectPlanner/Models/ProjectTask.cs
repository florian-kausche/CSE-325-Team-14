using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProjectPlanner.Models;

/// <summary>
/// Represents a task within a group project
/// </summary>
public class ProjectTask
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    [Required]
    public TaskStatus Status { get; set; } = TaskStatus.NotStarted;

    public DateTime? DueDate { get; set; }

    // Foreign keys
    [Required]
    public int GroupProjectId { get; set; }

    public string? AssignedUserId { get; set; }

    // Navigation properties
    [ForeignKey(nameof(GroupProjectId))]
    public virtual GroupProject GroupProject { get; set; } = null!;

    [ForeignKey(nameof(AssignedUserId))]
    public virtual ApplicationUser? AssignedUser { get; set; }

    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Checks if the task is overdue
    /// </summary>
    public bool IsOverdue => Status != TaskStatus.Completed && DueDate.HasValue && DueDate.Value < DateTime.UtcNow;
}
