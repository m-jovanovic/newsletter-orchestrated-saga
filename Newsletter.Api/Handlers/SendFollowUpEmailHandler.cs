using Newsletter.Api.Emails;
using Newsletter.Api.Messages;
using Rebus.Bus;
using Rebus.Handlers;

namespace Newsletter.Api.Handlers;

public class SendFollowUpEmailHandler : IHandleMessages<SendFollowUpEmail>
{
    private readonly IEmailService _emailService;
    private readonly IBus _bus;

    public SendFollowUpEmailHandler(IEmailService emailService, IBus bus)
    {
        _emailService = emailService;
        _bus = bus;
    }

    public async Task Handle(SendFollowUpEmail message)
    {
        await _emailService.SendFollowUpEmailAsync(message.Email);

        await _bus.Reply(new FollowUpEmailSent(message.Email));
    }
}