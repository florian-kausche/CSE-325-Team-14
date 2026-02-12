using Microsoft.EntityFrameworkCore;
using StudentProjectPlanner.Data;
using StudentProjectPlanner.Models;
using StudentProjectPlanner.Repositories.Interfaces;

namespace StudentProjectPlanner.Repositories.Implementations;

/// <summary>
/// Repository implementation for GroupProject operations
/// </summary>
public class GroupProjectRepository : Repository<GroupProject>, IGroupProjectRepository
{
    public GroupProjectRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<GroupProject>> GetProjectsByUserIdAsync(string userId)
    {
        return await _dbSet
            .Include(p => p.Members)
            .Include(p => p.Tasks)
            .Where(p => p.Members.Any(m => m.UserId == userId))
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<GroupProject?> GetProjectWithMembersAndTasksAsync(int projectId)
    {
        return await _dbSet
            .Include(p => p.Members)
                .ThenInclude(m => m.User)
            .Include(p => p.Tasks)
                .ThenInclude(t => t.AssignedUser)
            .FirstOrDefaultAsync(p => p.Id == projectId);
    }

    public async Task<bool> IsUserMemberAsync(int projectId, string userId)
    {
        return await _context.ProjectMembers
            .AnyAsync(pm => pm.GroupProjectId == projectId && pm.UserId == userId);
    }
}
