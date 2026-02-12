using StudentProjectPlanner.Models;

namespace StudentProjectPlanner.Repositories.Interfaces;

/// <summary>
/// Repository interface for Course operations
/// </summary>
public interface ICourseRepository : IRepository<Course>
{
    Task<IEnumerable<Course>> GetCoursesByUserIdAsync(string userId);
    Task<Course?> GetCourseWithAssignmentsAsync(int courseId);
    Task<bool> CourseCodeExistsForUserAsync(string userId, string courseCode, int? excludeCourseId = null);
}
