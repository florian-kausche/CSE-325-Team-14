using StudentProjectPlanner.Models;

namespace StudentProjectPlanner.Repositories.Interfaces;

/// <summary>
/// Repository interface for GroupProject operations
/// </summary>
public interface IGroupProjectRepository : IRepository<GroupProject>
{
    Task<IEnumerable<GroupProject>> GetProjectsByUserIdAsync(string userId);
    Task<GroupProject?> GetProjectWithMembersAndTasksAsync(int projectId);
    Task<bool> IsUserMemberAsync(int projectId, string userId);
}
