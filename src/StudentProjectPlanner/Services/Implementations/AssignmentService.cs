using StudentProjectPlanner.Models;
using StudentProjectPlanner.Repositories.Interfaces;
using StudentProjectPlanner.Services.Interfaces;

namespace StudentProjectPlanner.Services.Implementations;

/// <summary>
/// Service implementation for Assignment operations
/// </summary>
public class AssignmentService : IAssignmentService
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly ICourseRepository _courseRepository;

    public AssignmentService(IAssignmentRepository assignmentRepository, ICourseRepository courseRepository)
    {
        _assignmentRepository = assignmentRepository;
        _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<Assignment>> GetUserAssignmentsAsync(string userId)
    {
        return await _assignmentRepository.GetAssignmentsByUserIdAsync(userId);
    }

    public async Task<IEnumerable<Assignment>> GetCourseAssignmentsAsync(int courseId, string userId)
    {
        // Verify user owns the course
        var course = await _courseRepository.GetByIdAsync(courseId);
        if (course == null || course.UserId != userId)
        {
            return Enumerable.Empty<Assignment>();
        }

        return await _assignmentRepository.GetAssignmentsByCourseIdAsync(courseId);
    }

    public async Task<IEnumerable<Assignment>> GetUpcomingAssignmentsAsync(string userId, int days = 7)
    {
        return await _assignmentRepository.GetUpcomingAssignmentsAsync(userId, days);
    }

    public async Task<IEnumerable<Assignment>> GetOverdueAssignmentsAsync(string userId)
    {
        return await _assignmentRepository.GetOverdueAssignmentsAsync(userId);
    }

    public async Task<Assignment?> GetAssignmentByIdAsync(int assignmentId, string userId)
    {
        var assignment = await _assignmentRepository.GetAssignmentWithCourseAsync(assignmentId);

        if (assignment == null || assignment.UserId != userId)
        {
            return null;
        }

        return assignment;
    }

    public async Task<Assignment> CreateAssignmentAsync(Assignment assignment, string userId)
    {
        // Verify user owns the course
        var course = await _courseRepository.GetByIdAsync(assignment.CourseId);
        if (course == null || course.UserId != userId)
        {
            throw new InvalidOperationException("Course not found or you don't have permission to add assignments to it.");
        }

        assignment.UserId = userId;
        assignment.CreatedAt = DateTime.UtcNow;

        return await _assignmentRepository.AddAsync(assignment);
    }

    public async Task<bool> UpdateAssignmentAsync(int assignmentId, Assignment assignment, string userId)
    {
        var existingAssignment = await _assignmentRepository.GetByIdAsync(assignmentId);

        if (existingAssignment == null || existingAssignment.UserId != userId)
        {
            return false;
        }

        // If course is being changed, verify user owns the new course
        if (existingAssignment.CourseId != assignment.CourseId)
        {
            var course = await _courseRepository.GetByIdAsync(assignment.CourseId);
            if (course == null || course.UserId != userId)
            {
                throw new InvalidOperationException("Course not found or you don't have permission to use it.");
            }
        }

        existingAssignment.Name = assignment.Name;
        existingAssignment.Description = assignment.Description;
        existingAssignment.DueDate = assignment.DueDate;
        existingAssignment.Status = assignment.Status;
        existingAssignment.Priority = assignment.Priority;
        existingAssignment.CourseId = assignment.CourseId;
        existingAssignment.UpdatedAt = DateTime.UtcNow;

        if (assignment.Status == AssignmentStatus.Completed && existingAssignment.CompletedAt == null)
        {
            existingAssignment.CompletedAt = DateTime.UtcNow;
        }

        await _assignmentRepository.UpdateAsync(existingAssignment);
        return true;
    }

    public async Task<bool> UpdateAssignmentStatusAsync(int assignmentId, AssignmentStatus status, string userId)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(assignmentId);

        if (assignment == null || assignment.UserId != userId)
        {
            return false;
        }

        assignment.Status = status;
        assignment.UpdatedAt = DateTime.UtcNow;

        if (status == AssignmentStatus.Completed && assignment.CompletedAt == null)
        {
            assignment.CompletedAt = DateTime.UtcNow;
        }

        await _assignmentRepository.UpdateAsync(assignment);
        return true;
    }

    public async Task<bool> DeleteAssignmentAsync(int assignmentId, string userId)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(assignmentId);

        if (assignment == null || assignment.UserId != userId)
        {
            return false;
        }

        await _assignmentRepository.DeleteAsync(assignmentId);
        return true;
    }

    public async Task<bool> UserOwnsAssignmentAsync(int assignmentId, string userId)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(assignmentId);
        return assignment != null && assignment.UserId == userId;
    }
}
