namespace StudentProjectPlanner.Services.Interfaces;

/// <summary>
/// Service interface for dashboard data aggregation
/// </summary>
public interface IDashboardService
{
    Task<DashboardData> GetDashboardDataAsync(string userId);
}

/// <summary>
/// Data model for dashboard information
/// </summary>
public class DashboardData
{
    public int TotalCourses { get; set; }
    public int TotalAssignments { get; set; }
    public int CompletedAssignments { get; set; }
    public int UpcomingAssignments { get; set; }
    public int OverdueAssignments { get; set; }
    public int TotalGroupProjects { get; set; }
    public double CompletionPercentage { get; set; }
}
