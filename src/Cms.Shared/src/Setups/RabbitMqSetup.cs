using System;
using Cms.Shared.Configurations;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Cms.Shared.Setups;

internal static class RabbitMqSetup
{
    internal static void SetupConnectionFactory(
        ConnectionFactory connectionFactory,
        IConfiguration configuration
    )
    {
        var rabbitMqConfiguration = GetRabbitMqConfiguration(configuration);

        connectionFactory.HostName = rabbitMqConfiguration.HostName;
        connectionFactory.Port = rabbitMqConfiguration.Port;
        connectionFactory.UserName = rabbitMqConfiguration.UserName;
        connectionFactory.Password = rabbitMqConfiguration.Password;
        connectionFactory.Ssl = new SslOption { Enabled = rabbitMqConfiguration.Ssl.Enabled };
        connectionFactory.VirtualHost = rabbitMqConfiguration.VirtualHost;
    }

    private static RabbitMqConfiguration GetRabbitMqConfiguration(IConfiguration configuration)
    {
        var messagingBrokerConfiguration = configuration
            .GetSection("MessagingBroker")
            .Get<MessagingBrokerConfiguration>();

        if (messagingBrokerConfiguration is null)
        {
            throw new NullReferenceException(nameof(messagingBrokerConfiguration));
        }

        return messagingBrokerConfiguration.RabbitMq;
    }
}
