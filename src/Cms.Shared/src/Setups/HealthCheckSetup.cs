using Cms.Shared.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMQ.Client;

namespace Cms.Shared.Setups;

public static class HealthCheckSetup
{
    public static IHealthChecksBuilder AddRabbitMQ(
        this IHealthChecksBuilder builder,
        IConfiguration configuration
    )
    {
        builder.AddRabbitMQ(
            factory: async _ =>
            {
                var factory = new ConnectionFactory();

                RabbitMqSetup.SetupConnectionFactory(factory, configuration);

                return await factory.CreateConnectionAsync();
            },
            name: "rabbitmq",
            failureStatus: HealthStatus.Unhealthy,
            [HealthCheckTag.LivenessTag, HealthCheckTag.ReadinessTag]
        );

        return builder;
    }

    public static WebApplication UseHealthCheck(this WebApplication app)
    {
        app.MapHealthChecks(
            "/health/liveness",
            new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(HealthCheckTag.LivenessTag),
            }
        );
        app.MapHealthChecks(
            "/health/readiness",
            new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(HealthCheckTag.ReadinessTag),
            }
        );
        app.MapHealthChecks(
            "/health/startup",
            new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(HealthCheckTag.StartupTag),
            }
        );
        app.UseHealthChecksPrometheusExporter("/metrics");

        return app;
    }
}
