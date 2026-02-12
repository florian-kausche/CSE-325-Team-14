using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProjectPlanner.Models;

/// <summary>
/// Represents an assignment or task for a specific course
/// </summary>
public class Assignment
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    public AssignmentStatus Status { get; set; } = AssignmentStatus.NotStarted;

    [Required]
    public Priority Priority { get; set; } = Priority.Medium;

    // Foreign keys
    [Required]
    public int CourseId { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    // Navigation properties
    [ForeignKey(nameof(CourseId))]
    public virtual Course Course { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;

    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Checks if the assignment is overdue
    /// </summary>
    public bool IsOverdue => Status != AssignmentStatus.Completed && DueDate < DateTime.UtcNow;

    /// <summary>
    /// Gets the number of days until the assignment is due
    /// </summary>
    public int DaysUntilDue => (DueDate.Date - DateTime.UtcNow.Date).Days;
}
