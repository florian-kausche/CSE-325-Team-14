using StudentProjectPlanner.Models;

namespace StudentProjectPlanner.Repositories.Interfaces;

/// <summary>
/// Repository interface for Assignment operations
/// </summary>
public interface IAssignmentRepository : IRepository<Assignment>
{
    Task<IEnumerable<Assignment>> GetAssignmentsByUserIdAsync(string userId);
    Task<IEnumerable<Assignment>> GetAssignmentsByCourseIdAsync(int courseId);
    Task<IEnumerable<Assignment>> GetUpcomingAssignmentsAsync(string userId, int days = 7);
    Task<IEnumerable<Assignment>> GetOverdueAssignmentsAsync(string userId);
    Task<Assignment?> GetAssignmentWithCourseAsync(int assignmentId);
}
