using StudentProjectPlanner.Models;

namespace StudentProjectPlanner.Services.Interfaces;

/// <summary>
/// Service interface for Course operations
/// </summary>
public interface ICourseService
{
    Task<IEnumerable<Course>> GetUserCoursesAsync(string userId);
    Task<Course?> GetCourseByIdAsync(int courseId, string userId);
    Task<Course?> GetCourseWithAssignmentsAsync(int courseId, string userId);
    Task<Course> CreateCourseAsync(Course course, string userId);
    Task<bool> UpdateCourseAsync(int courseId, Course course, string userId);
    Task<bool> DeleteCourseAsync(int courseId, string userId);
    Task<bool> UserOwnsCourseAsync(int courseId, string userId);
}
