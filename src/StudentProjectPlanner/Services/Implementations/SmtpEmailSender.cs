using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using StudentProjectPlanner.Models;
using StudentProjectPlanner.Services.Interfaces;

namespace StudentProjectPlanner.Services.Implementations;

public class SmtpEmailSender : IEmailSender
{
    private readonly EmailOptions _options;

    public SmtpEmailSender(IOptions<EmailOptions> options)
    {
        _options = options.Value;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        if (string.IsNullOrWhiteSpace(_options.Host))
        {
            throw new InvalidOperationException("Email host is not configured.");
        }

        var fromAddress = string.IsNullOrWhiteSpace(_options.From)
            ? _options.Username
            : _options.From;

        if (string.IsNullOrWhiteSpace(fromAddress))
        {
            throw new InvalidOperationException("Email 'From' address is not configured.");
        }

        using var client = new SmtpClient(_options.Host, _options.Port)
        {
            EnableSsl = _options.EnableSsl,
            Credentials = new NetworkCredential(_options.Username, _options.Password)
        };

        using var message = new MailMessage(fromAddress, toEmail, subject, body)
        {
            IsBodyHtml = false
        };

        await client.SendMailAsync(message);
    }
}
