namespace Z.HangFire.BackgroundJobs.Abstractions;

public interface IBackgroundJobNameProvider
{
    string Name { get; }
}
