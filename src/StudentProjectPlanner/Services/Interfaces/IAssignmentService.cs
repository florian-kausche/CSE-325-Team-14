using StudentProjectPlanner.Models;

namespace StudentProjectPlanner.Services.Interfaces;

/// <summary>
/// Service interface for Assignment operations
/// </summary>
public interface IAssignmentService
{
    Task<IEnumerable<Assignment>> GetUserAssignmentsAsync(string userId);
    Task<IEnumerable<Assignment>> GetCourseAssignmentsAsync(int courseId, string userId);
    Task<IEnumerable<Assignment>> GetUpcomingAssignmentsAsync(string userId, int days = 7);
    Task<IEnumerable<Assignment>> GetOverdueAssignmentsAsync(string userId);
    Task<Assignment?> GetAssignmentByIdAsync(int assignmentId, string userId);
    Task<Assignment> CreateAssignmentAsync(Assignment assignment, string userId);
    Task<bool> UpdateAssignmentAsync(int assignmentId, Assignment assignment, string userId);
    Task<bool> UpdateAssignmentStatusAsync(int assignmentId, AssignmentStatus status, string userId);
    Task<bool> DeleteAssignmentAsync(int assignmentId, string userId);
    Task<bool> UserOwnsAssignmentAsync(int assignmentId, string userId);
}
