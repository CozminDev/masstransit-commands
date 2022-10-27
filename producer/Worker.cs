using MassTransit;
using Commands;

namespace producer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IBus bus;

    public Worker(ILogger<Worker> logger, IBus bus)
    {
        _logger = logger;
        this.bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker is sending command at: {time}", DateTimeOffset.Now);

            ISendEndpoint endpoint = await bus.GetSendEndpoint(new Uri("queue:random-queue"));

            await endpoint.Send(new RandomCommand() { Success = true });

            _logger.LogInformation("Worker sent command at: {time}", DateTimeOffset.Now);

            await Task.Delay(10_000, stoppingToken);
        }
    }
}
