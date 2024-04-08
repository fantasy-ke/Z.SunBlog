
using Hangfire;

namespace Z.HangFire.BackgroundJobs.Abstractions;

public interface IHangfireBackgroundScheduleJob : IBackgroundScheduleJob
{
    RecurringJobOptions JobOptions { get; set; }

    string Queue { get; set; }
}
