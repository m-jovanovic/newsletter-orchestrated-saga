using Rebus.Logging;

namespace WebApi.Emails;

public interface IEmailService
{
    Task SendWelcomeEmailAsync(string email);

    Task SendFollowUpEmailAsync(string email);
}