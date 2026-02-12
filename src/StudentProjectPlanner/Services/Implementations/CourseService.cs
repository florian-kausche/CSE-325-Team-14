using StudentProjectPlanner.Models;
using StudentProjectPlanner.Repositories.Interfaces;
using StudentProjectPlanner.Services.Interfaces;

namespace StudentProjectPlanner.Services.Implementations;

/// <summary>
/// Service implementation for Course operations
/// </summary>
public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<Course>> GetUserCoursesAsync(string userId)
    {
        return await _courseRepository.GetCoursesByUserIdAsync(userId);
    }

    public async Task<Course?> GetCourseByIdAsync(int courseId, string userId)
    {
        var course = await _courseRepository.GetByIdAsync(courseId);

        if (course == null || course.UserId != userId)
        {
            return null;
        }

        return course;
    }

    public async Task<Course?> GetCourseWithAssignmentsAsync(int courseId, string userId)
    {
        var course = await _courseRepository.GetCourseWithAssignmentsAsync(courseId);

        if (course == null || course.UserId != userId)
        {
            return null;
        }

        return course;
    }

    public async Task<Course> CreateCourseAsync(Course course, string userId)
    {
        // Validate course code uniqueness for user
        var codeExists = await _courseRepository.CourseCodeExistsForUserAsync(userId, course.CourseCode);
        if (codeExists)
        {
            throw new InvalidOperationException($"A course with code '{course.CourseCode}' already exists.");
        }

        course.UserId = userId;
        course.CreatedAt = DateTime.UtcNow;

        return await _courseRepository.AddAsync(course);
    }

    public async Task<bool> UpdateCourseAsync(int courseId, Course course, string userId)
    {
        var existingCourse = await _courseRepository.GetByIdAsync(courseId);

        if (existingCourse == null || existingCourse.UserId != userId)
        {
            return false;
        }

        // Validate course code uniqueness for user (excluding current course)
        var codeExists = await _courseRepository.CourseCodeExistsForUserAsync(userId, course.CourseCode, courseId);
        if (codeExists)
        {
            throw new InvalidOperationException($"A course with code '{course.CourseCode}' already exists.");
        }

        existingCourse.Name = course.Name;
        existingCourse.CourseCode = course.CourseCode;
        existingCourse.Semester = course.Semester;
        existingCourse.Description = course.Description;
        existingCourse.Color = course.Color;
        existingCourse.UpdatedAt = DateTime.UtcNow;

        await _courseRepository.UpdateAsync(existingCourse);
        return true;
    }

    public async Task<bool> DeleteCourseAsync(int courseId, string userId)
    {
        var course = await _courseRepository.GetByIdAsync(courseId);

        if (course == null || course.UserId != userId)
        {
            return false;
        }

        await _courseRepository.DeleteAsync(courseId);
        return true;
    }

    public async Task<bool> UserOwnsCourseAsync(int courseId, string userId)
    {
        var course = await _courseRepository.GetByIdAsync(courseId);
        return course != null && course.UserId == userId;
    }
}
