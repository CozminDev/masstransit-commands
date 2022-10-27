using consumer;
using MassTransit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(busConfig =>
        {
            //busConfig.AddConsumer<RandomCommandConsumer>().Endpoint(e =>
            //{
            //    e.ConfigureConsumeTopology = false;
            //    e.Name = "random-queue";
            //});

            busConfig.AddConsumer<RandomCommandConsumer>();

            busConfig.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(Environment.GetEnvironmentVariable("RABBIT"), h => {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("random-queue", e =>
                {
                    e.ConfigureConsumer(context, typeof(RandomCommandConsumer));
                });
                //cfg.ConfigureEndpoints(context);
            });
        });
    })
    .Build();

await host.RunAsync();