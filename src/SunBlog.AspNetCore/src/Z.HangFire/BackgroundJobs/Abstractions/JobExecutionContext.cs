using System;
using System.Threading;

namespace Z.HangFire.BackgroundJobs.Abstractions;

public class JobExecutionContext
{
    public IServiceProvider ServiceProvider { get; }

    public Type JobType { get; }

    public object JobArgs { get; }

    public CancellationToken CancellationToken { get; }

    public JobExecutionContext(
        IServiceProvider serviceProvider,
        Type jobType,
        object jobArgs,
        CancellationToken cancellationToken = default)
    {
        ServiceProvider = serviceProvider;
        JobType = jobType;
        JobArgs = jobArgs;
        CancellationToken = cancellationToken;
    }
}
