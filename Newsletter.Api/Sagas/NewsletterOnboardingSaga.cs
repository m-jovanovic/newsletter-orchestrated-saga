using Newsletter.Api.Messages;
using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;

namespace Newsletter.Api.Sagas;

public class NewsletterOnboardingSaga :
    Saga<NewsletterOnboardingSagaData>,
    IAmInitiatedBy<SubscribeToNewsletter>,
    IHandleMessages<WelcomeEmailSent>,
    IHandleMessages<FollowUpEmailSent>
{
    private readonly IBus _bus;

    public NewsletterOnboardingSaga(IBus bus)
    {
        _bus = bus;
    }

    protected override void CorrelateMessages(ICorrelationConfig<NewsletterOnboardingSagaData> config)
    {
        config.Correlate<SubscribeToNewsletter>(m => m.Email, d => d.Email);

        config.Correlate<WelcomeEmailSent>(m => m.Email, d => d.Email);

        config.Correlate<FollowUpEmailSent>(m => m.Email, d => d.Email);
    }

    public async Task Handle(SubscribeToNewsletter message)
    {
        if (!IsNew)
        {
            return;
        }

        await _bus.Send(new SendWelcomeEmail(message.Email));
    }

    public async Task Handle(WelcomeEmailSent message)
    {
        Data.WelcomeEmailSent = true;

        await _bus.Defer(TimeSpan.FromSeconds(5), new SendFollowUpEmail(message.Email));
    }

    public Task Handle(FollowUpEmailSent message)
    {
        Data.FollowUpEmailSent = true;

        MarkAsComplete();

        return Task.CompletedTask;
    }
}
