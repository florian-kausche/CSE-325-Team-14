using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProjectPlanner.Models;

/// <summary>
/// Represents a member's participation in a group project
/// </summary>
public class ProjectMember
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Role { get; set; } = "Member"; // e.g., "Owner", "Member", "Contributor"

    // Foreign keys
    [Required]
    public int GroupProjectId { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    // Navigation properties
    [ForeignKey(nameof(GroupProjectId))]
    public virtual GroupProject GroupProject { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;

    // Audit fields
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
}
