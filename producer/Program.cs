using MassTransit;
using producer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(busConfig =>
        {
            busConfig.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(Environment.GetEnvironmentVariable("RABBIT"), h => {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        });

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
