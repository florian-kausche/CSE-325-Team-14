using StudentProjectPlanner.Models;

namespace StudentProjectPlanner.Services.Interfaces;

/// <summary>
/// Service interface for GroupProject operations
/// </summary>
public interface IGroupProjectService
{
    Task<IEnumerable<GroupProject>> GetUserProjectsAsync(string userId);
    Task<GroupProject?> GetProjectByIdAsync(int projectId, string userId);
    Task<GroupProject?> GetProjectWithDetailsAsync(int projectId, string userId);
    Task<GroupProject> CreateProjectAsync(GroupProject project, string userId);
    Task<bool> UpdateProjectAsync(int projectId, GroupProject project, string userId);
    Task<bool> DeleteProjectAsync(int projectId, string userId);
    Task<bool> AddMemberAsync(int projectId, string userId, string memberEmail, string role = "Member");
    Task<bool> RemoveMemberAsync(int projectId, string userId, string memberUserId);
    Task<bool> UserIsMemberAsync(int projectId, string userId);
    Task<ProjectTask> AddTaskAsync(int projectId, ProjectTask task, string userId);
    Task<bool> UpdateTaskStatusAsync(int projectId, int taskId, Models.TaskStatus newStatus, string userId);
    Task<bool> DeleteTaskAsync(int projectId, int taskId, string userId);
}
