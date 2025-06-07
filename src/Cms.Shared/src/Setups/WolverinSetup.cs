using Microsoft.Extensions.Configuration;
using Wolverine;
using Wolverine.RabbitMQ;

namespace Cms.Shared.Setups;

public static class WolverineSetup
{
    public static WolverineOptions UseRabbitMq(
        this WolverineOptions options,
        IConfiguration configuration
    )
    {
        options
            .UseRabbitMq(connectionFactory =>
                RabbitMqSetup.SetupConnectionFactory(connectionFactory, configuration)
            )
            .AutoProvision()
            .UseConventionalRouting();

        return options;
    }
}
