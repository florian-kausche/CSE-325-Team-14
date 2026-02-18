namespace StudentProjectPlanner.Models;

/// <summary>
/// Configuration options for SMTP email sending.
/// These settings are read from appsettings.json under the "Email" section.
/// </summary>
public class EmailOptions
{
    /// <summary>
    /// The configuration section name for email settings.
    /// </summary>
    public const string SectionName = "Email";

    public string Host { get; set; } = string.Empty;

    public int Port { get; set; } = 587;

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string From { get; set; } = string.Empty;

    public bool EnableSsl { get; set; } = true;
}
