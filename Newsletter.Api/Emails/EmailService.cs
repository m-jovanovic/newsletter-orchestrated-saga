namespace Newsletter.Api.Emails;

internal sealed class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public Task SendWelcomeEmailAsync(string email)
    {
        _logger.LogInformation("Sending welcome email to {Email}", email);

        return Task.CompletedTask;
    }

    public Task SendFollowUpEmailAsync(string email)
    {
        _logger.LogInformation("Sending follow-up email to {Email}", email);

        return Task.CompletedTask;
    }
}