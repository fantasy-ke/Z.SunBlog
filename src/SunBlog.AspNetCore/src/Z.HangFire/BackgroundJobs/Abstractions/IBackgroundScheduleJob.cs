using Hangfire;

namespace Z.HangFire.BackgroundJobs.Abstractions;

public interface IBackgroundScheduleJob
{
    public string Id { get; set; }

    public double CronSeqs { get; set; }

    public string CronExpression { get; set; }

    Task DoWorkAsync();
}
