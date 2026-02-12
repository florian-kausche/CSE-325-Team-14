using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentProjectPlanner.Data;
using StudentProjectPlanner.Models;
using StudentProjectPlanner.Repositories.Interfaces;
using StudentProjectPlanner.Services.Interfaces;

namespace StudentProjectPlanner.Services.Implementations;

/// <summary>
/// Service implementation for GroupProject operations
/// </summary>
public class GroupProjectService : IGroupProjectService
{
    private readonly IGroupProjectRepository _projectRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public GroupProjectService(
        IGroupProjectRepository projectRepository,
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context)
    {
        _projectRepository = projectRepository;
        _userManager = userManager;
        _context = context;
    }

    public async Task<IEnumerable<GroupProject>> GetUserProjectsAsync(string userId)
    {
        return await _projectRepository.GetProjectsByUserIdAsync(userId);
    }

    public async Task<GroupProject?> GetProjectByIdAsync(int projectId, string userId)
    {
        var isMember = await _projectRepository.IsUserMemberAsync(projectId, userId);
        if (!isMember)
        {
            return null;
        }

        return await _projectRepository.GetByIdAsync(projectId);
    }

    public async Task<GroupProject?> GetProjectWithDetailsAsync(int projectId, string userId)
    {
        var isMember = await _projectRepository.IsUserMemberAsync(projectId, userId);
        if (!isMember)
        {
            return null;
        }

        return await _projectRepository.GetProjectWithMembersAndTasksAsync(projectId);
    }

    public async Task<GroupProject> CreateProjectAsync(GroupProject project, string userId)
    {
        project.CreatedAt = DateTime.UtcNow;

        var createdProject = await _projectRepository.AddAsync(project);

        // Add the creator as the Owner member
        createdProject.Members.Add(new ProjectMember
        {
            GroupProjectId = createdProject.Id,
            UserId = userId,
            Role = "Owner",
            JoinedAt = DateTime.UtcNow
        });

        await _projectRepository.UpdateAsync(createdProject);

        return createdProject;
    }

    public async Task<bool> UpdateProjectAsync(int projectId, GroupProject project, string userId)
    {
        var isMember = await _projectRepository.IsUserMemberAsync(projectId, userId);
        if (!isMember)
        {
            return false;
        }

        var existingProject = await _projectRepository.GetByIdAsync(projectId);
        if (existingProject == null)
        {
            return false;
        }

        existingProject.Name = project.Name;
        existingProject.Description = project.Description;
        existingProject.DueDate = project.DueDate;
        existingProject.UpdatedAt = DateTime.UtcNow;

        await _projectRepository.UpdateAsync(existingProject);
        return true;
    }

    public async Task<bool> DeleteProjectAsync(int projectId, string userId)
    {
        var isMember = await _projectRepository.IsUserMemberAsync(projectId, userId);
        if (!isMember)
        {
            return false;
        }

        await _projectRepository.DeleteAsync(projectId);
        return true;
    }

    public async Task<bool> AddMemberAsync(int projectId, string userId, string memberEmail, string role = "Member")
    {
        var isMember = await _projectRepository.IsUserMemberAsync(projectId, userId);
        if (!isMember)
        {
            return false;
        }

        var memberUser = await _userManager.FindByEmailAsync(memberEmail);
        if (memberUser == null)
        {
            throw new InvalidOperationException($"No user found with email '{memberEmail}'.");
        }

        // Check if already a member
        var alreadyMember = await _projectRepository.IsUserMemberAsync(projectId, memberUser.Id);
        if (alreadyMember)
        {
            throw new InvalidOperationException("This user is already a member of the project.");
        }

        var project = await _projectRepository.GetProjectWithMembersAndTasksAsync(projectId);
        if (project == null)
        {
            return false;
        }

        project.Members.Add(new ProjectMember
        {
            GroupProjectId = projectId,
            UserId = memberUser.Id,
            Role = role,
            JoinedAt = DateTime.UtcNow
        });

        await _projectRepository.UpdateAsync(project);
        return true;
    }

    public async Task<bool> RemoveMemberAsync(int projectId, string userId, string memberUserId)
    {
        var isMember = await _projectRepository.IsUserMemberAsync(projectId, userId);
        if (!isMember)
        {
            return false;
        }

        var project = await _projectRepository.GetProjectWithMembersAndTasksAsync(projectId);
        if (project == null)
        {
            return false;
        }

        var member = project.Members.FirstOrDefault(m => m.UserId == memberUserId);
        if (member == null)
        {
            return false;
        }

        // Prevent removing the last owner
        if (member.Role == "Owner" && project.Members.Count(m => m.Role == "Owner") <= 1)
        {
            throw new InvalidOperationException("Cannot remove the last owner of the project.");
        }

        project.Members.Remove(member);
        await _projectRepository.UpdateAsync(project);
        return true;
    }

    public async Task<bool> UserIsMemberAsync(int projectId, string userId)
    {
        return await _projectRepository.IsUserMemberAsync(projectId, userId);
    }

    public async Task<ProjectTask> AddTaskAsync(int projectId, ProjectTask task, string userId)
    {
        var isMember = await _projectRepository.IsUserMemberAsync(projectId, userId);
        if (!isMember)
        {
            throw new InvalidOperationException("You are not a member of this project.");
        }

        task.GroupProjectId = projectId;
        task.Status = Models.TaskStatus.NotStarted;
        task.CreatedAt = DateTime.UtcNow;

        _context.ProjectTasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<bool> UpdateTaskStatusAsync(int projectId, int taskId, Models.TaskStatus newStatus, string userId)
    {
        var isMember = await _projectRepository.IsUserMemberAsync(projectId, userId);
        if (!isMember)
        {
            return false;
        }

        var task = await _context.ProjectTasks.FirstOrDefaultAsync(t => t.Id == taskId && t.GroupProjectId == projectId);
        if (task == null)
        {
            return false;
        }

        task.Status = newStatus;
        task.UpdatedAt = DateTime.UtcNow;
        task.CompletedAt = newStatus == Models.TaskStatus.Completed ? DateTime.UtcNow : null;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTaskAsync(int projectId, int taskId, string userId)
    {
        var isMember = await _projectRepository.IsUserMemberAsync(projectId, userId);
        if (!isMember)
        {
            return false;
        }

        var task = await _context.ProjectTasks.FirstOrDefaultAsync(t => t.Id == taskId && t.GroupProjectId == projectId);
        if (task == null)
        {
            return false;
        }

        _context.ProjectTasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}
