using Newsletter.Api.Emails;
using Newsletter.Api.Messages;
using Rebus.Bus;
using Rebus.Handlers;

namespace Newsletter.Api.Handlers;

public class SendWelcomeEmailHandler : IHandleMessages<SendWelcomeEmail>
{
    private readonly IEmailService _emailService;
    private readonly IBus _bus;

    public SendWelcomeEmailHandler(IEmailService emailService, IBus bus)
    {
        _emailService = emailService;
        _bus = bus;
    }

    public async Task Handle(SendWelcomeEmail message)
    {
        await _emailService.SendWelcomeEmailAsync(message.Email);

        await _bus.Reply(new WelcomeEmailSent(message.Email));
    }
}