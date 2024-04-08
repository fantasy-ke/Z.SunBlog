using Microsoft.Extensions.Logging;
using Z.EventBus.Handlers;
using Z.Module.DependencyInjection;

namespace Z.SunBlog.Core.Handlers.TestHandlers
{
    public class TestEventHandler : IEventHandler<TestDto>, ITransientDependency
    {
        private Microsoft.Extensions.Logging.ILogger _logger;
        public TestEventHandler(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger<TestEventHandler>();
        }
        public Task HandelrAsync(TestDto eto)
        {
            _logger.LogInformation($"{typeof(TestDto).Name}--{eto.Name}--{eto.Description}");
            return Task.CompletedTask;
        }
    }
}
