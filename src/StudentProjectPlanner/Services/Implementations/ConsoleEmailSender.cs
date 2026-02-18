using StudentProjectPlanner.Services.Interfaces;

namespace StudentProjectPlanner.Services.Implementations;

/// <summary>
/// Development email sender that logs emails to console instead of sending them.
/// Use this for testing password reset without configuring SMTP.
/// </summary>
public class ConsoleEmailSender : IEmailSender
{
    private readonly ILogger<ConsoleEmailSender> _logger;

    public ConsoleEmailSender(ILogger<ConsoleEmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string toEmail, string subject, string body)
    {
        _logger.LogInformation("==============================================");
        _logger.LogInformation("📧 EMAIL (Console Mode - Not Actually Sent)");
        _logger.LogInformation("==============================================");
        _logger.LogInformation("To: {ToEmail}", toEmail);
        _logger.LogInformation("Subject: {Subject}", subject);
        _logger.LogInformation("----------------------------------------------");
        _logger.LogInformation("{Body}", body);
        _logger.LogInformation("==============================================");
        
        Console.WriteLine("\n==============================================");
        Console.WriteLine("📧 PASSWORD RESET EMAIL");
        Console.WriteLine("==============================================");
        Console.WriteLine($"To: {toEmail}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine(body);
        Console.WriteLine("==============================================\n");

        return Task.CompletedTask;
    }
}
