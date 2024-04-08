using System.Threading.Tasks;

namespace Z.HangFire.BackgroundJobs.Abstractions;

public interface IBackgroundJobExecuter
{
    Task ExecuteAsync(JobExecutionContext context);
}
