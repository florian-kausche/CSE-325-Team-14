using Microsoft.EntityFrameworkCore;
using StudentProjectPlanner.Data;
using StudentProjectPlanner.Models;
using StudentProjectPlanner.Repositories.Interfaces;

namespace StudentProjectPlanner.Repositories.Implementations;

/// <summary>
/// Repository implementation for Assignment operations
/// </summary>
public class AssignmentRepository : Repository<Assignment>, IAssignmentRepository
{
    public AssignmentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Assignment>> GetAssignmentsByUserIdAsync(string userId)
    {
        return await _dbSet
            .Include(a => a.Course)
            .Where(a => a.UserId == userId)
            .OrderBy(a => a.DueDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Assignment>> GetAssignmentsByCourseIdAsync(int courseId)
    {
        return await _dbSet
            .Include(a => a.Course)
            .Where(a => a.CourseId == courseId)
            .OrderBy(a => a.DueDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Assignment>> GetUpcomingAssignmentsAsync(string userId, int days = 7)
    {
        var futureDate = DateTime.UtcNow.AddDays(days);

        return await _dbSet
            .Include(a => a.Course)
            .Where(a => a.UserId == userId
                && a.Status != AssignmentStatus.Completed
                && a.DueDate <= futureDate
                && a.DueDate >= DateTime.UtcNow)
            .OrderBy(a => a.DueDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Assignment>> GetOverdueAssignmentsAsync(string userId)
    {
        return await _dbSet
            .Include(a => a.Course)
            .Where(a => a.UserId == userId
                && a.Status != AssignmentStatus.Completed
                && a.DueDate < DateTime.UtcNow)
            .OrderBy(a => a.DueDate)
            .ToListAsync();
    }

    public async Task<Assignment?> GetAssignmentWithCourseAsync(int assignmentId)
    {
        return await _dbSet
            .Include(a => a.Course)
            .FirstOrDefaultAsync(a => a.Id == assignmentId);
    }
}
