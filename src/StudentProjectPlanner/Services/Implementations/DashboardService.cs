using StudentProjectPlanner.Models;
using StudentProjectPlanner.Repositories.Interfaces;
using StudentProjectPlanner.Services.Interfaces;

namespace StudentProjectPlanner.Services.Implementations;

/// <summary>
/// Service implementation for dashboard data aggregation
/// </summary>
public class DashboardService : IDashboardService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IGroupProjectRepository _projectRepository;

    public DashboardService(
        ICourseRepository courseRepository,
        IAssignmentRepository assignmentRepository,
        IGroupProjectRepository projectRepository)
    {
        _courseRepository = courseRepository;
        _assignmentRepository = assignmentRepository;
        _projectRepository = projectRepository;
    }

    public async Task<DashboardData> GetDashboardDataAsync(string userId)
    {
        var courses = await _courseRepository.GetCoursesByUserIdAsync(userId);
        var assignments = await _assignmentRepository.GetAssignmentsByUserIdAsync(userId);
        var projects = await _projectRepository.GetProjectsByUserIdAsync(userId);

        var upcomingAssignments = await _assignmentRepository.GetUpcomingAssignmentsAsync(userId, 7);
        var overdueAssignments = await _assignmentRepository.GetOverdueAssignmentsAsync(userId);

        var totalAssignments = assignments.Count();
        var completedAssignments = assignments.Count(a => a.Status == AssignmentStatus.Completed);

        return new DashboardData
        {
            TotalCourses = courses.Count(),
            TotalAssignments = totalAssignments,
            CompletedAssignments = completedAssignments,
            UpcomingAssignments = upcomingAssignments.Count(),
            OverdueAssignments = overdueAssignments.Count(),
            TotalGroupProjects = projects.Count(),
            CompletionPercentage = totalAssignments > 0
                ? Math.Round((double)completedAssignments / totalAssignments * 100, 1)
                : 0
        };
    }
}
