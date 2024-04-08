using Microsoft.Extensions.Logging;
using Z.HangFire.BackgroundJobs.Abstractions;
using Z.Module.DependencyInjection;

namespace Z.SunBlog.Core.jobs.TestJob
{
    public class TestJob : AsyncBackgroundJob<TestJobDto>,ITransientDependency
    {
        public ILogger<TestJob> _Logger { get; set; }

        public TestJob(ILogger<TestJob> logger)
        {
            _Logger = logger;
        }

        public override async Task ExecuteAsync(TestJobDto args)
        {
            _Logger.LogInformation("Guid" + args.Id);

            await Task.CompletedTask;
        }
    }
}
