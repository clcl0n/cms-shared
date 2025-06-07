using System;
using Cms.Shared.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.Shared.Setups;

public static class ConfigurationSetup
{
    public static IServiceCollection AddMessagingBrokerConfiguration(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddOptions<MessagingBrokerConfiguration>()
            .Bind(configuration.GetSection("MessagingBroker"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
