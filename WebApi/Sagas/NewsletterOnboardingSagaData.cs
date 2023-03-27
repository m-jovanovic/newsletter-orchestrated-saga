using Rebus.Sagas;

namespace WebApi.Sagas;

public class NewsletterOnboardingSagaData : ISagaData
{
    public Guid Id { get; set; }
    public int Revision { get; set; }

    public string Email { get; set; } = string.Empty;

    public bool WelcomeEmailSent { get; set; }

    public bool FollowUpEmailSent { get; set; }
}