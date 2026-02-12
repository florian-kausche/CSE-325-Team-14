using Microsoft.EntityFrameworkCore;
using StudentProjectPlanner.Data;
using StudentProjectPlanner.Models;
using StudentProjectPlanner.Repositories.Interfaces;

namespace StudentProjectPlanner.Repositories.Implementations;

/// <summary>
/// Repository implementation for Course operations
/// </summary>
public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Course>> GetCoursesByUserIdAsync(string userId)
    {
        return await _dbSet
            .Where(c => c.UserId == userId)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<Course?> GetCourseWithAssignmentsAsync(int courseId)
    {
        return await _dbSet
            .Include(c => c.Assignments)
            .FirstOrDefaultAsync(c => c.Id == courseId);
    }

    public async Task<bool> CourseCodeExistsForUserAsync(string userId, string courseCode, int? excludeCourseId = null)
    {
        var query = _dbSet.Where(c => c.UserId == userId && c.CourseCode == courseCode);

        if (excludeCourseId.HasValue)
        {
            query = query.Where(c => c.Id != excludeCourseId.Value);
        }

        return await query.AnyAsync();
    }
}
