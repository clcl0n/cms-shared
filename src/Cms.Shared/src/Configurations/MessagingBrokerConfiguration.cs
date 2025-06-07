namespace Cms.Shared.Configurations;

public sealed class MessagingBrokerConfiguration
{
    public required RabbitMqConfiguration RabbitMq { get; set; }
}
