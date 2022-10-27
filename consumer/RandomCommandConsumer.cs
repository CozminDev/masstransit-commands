using Commands;
using MassTransit;

namespace consumer
{
    internal class RandomCommandConsumer : IConsumer<RandomCommand>
    {
        private readonly ILogger<RandomCommandConsumer> _logger;

        public RandomCommandConsumer(ILogger<RandomCommandConsumer> logger)
        {
            _logger = logger;
        }


        public Task Consume(ConsumeContext<RandomCommand> context)
        {
            _logger.LogInformation("Random command processed at: {time}", DateTimeOffset.Now);

            return Task.CompletedTask;
        }
    }
}
