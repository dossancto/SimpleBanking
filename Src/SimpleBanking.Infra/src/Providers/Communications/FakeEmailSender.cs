using Microsoft.Extensions.Logging;
using SimpleBanking.Adapters.Communications.EmailSenders;

namespace SimpleBanking.Infra.Providers.Communications;

public class FakeEmailSender(
    ILogger<FakeEmailSender> _logger
    ) : IEmailSenderAdapter
{
    public Task NotifyTranfer(NotifyTranferInput input)
    {
        var message = $"{input.Sender.ResponsableFullName} transfered '{input.Ammount} to '{input.Receiver.ResponsableFullName}'";

        _logger.LogInformation(message);

        return Task.CompletedTask;
    }
}
