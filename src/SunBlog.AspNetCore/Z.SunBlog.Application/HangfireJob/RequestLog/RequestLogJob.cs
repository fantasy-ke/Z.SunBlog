using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Z.Foundation.Core.AutofacExtensions;
using Z.Foundation.Core.Entities.EntityLog;
using Z.Foundation.Core.Entities.Repositories;
using Z.Foundation.Core.UnitOfWork;
using Z.HangFire.BackgroundJobs.Abstractions;
using Z.Module.DependencyInjection;

namespace Z.SunBlog.Application.HangfireJob.RequestLog;

/// <summary>
/// 请求日志清理
/// </summary>
public class RequestLogJob : BackgroundScheduleJobBase, ITransientDependency
{
    /// <summary>
    /// 构造函数 每天23点执行
    /// </summary>
    public RequestLogJob(IServiceProvider serviceProvider = null) : base(serviceProvider)
    {
        ServiceProvider = serviceProvider ?? IOCManager.GetService<IServiceProvider>();
        Id = "requestlogjob";
        CronSeqs = TimeSpan.FromHours(23).TotalMilliseconds;
    }

    /// <summary>
    /// 重新任务 删除5天前的请求日志
    /// </summary>
    /// <returns></returns>
    public override async Task DoWorkAsync()
    {
        await using var scope = ServiceProvider.CreateAsyncScope();
        using var unit = scope.ServiceProvider.GetService<IUnitOfWork>();
        try
        {
            await unit.BeginTransactionAsync();
            var _requestLogRepository = scope.ServiceProvider.GetRequiredService<IBasicRepository<ZRequestLog>>();
            await _requestLogRepository.DeleteAsync(c => c.CreationTime < DateTime.Now.AddDays(-5));
            await unit.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await unit.RollbackTransactionAsync();
            Log.Error($"定时清除请求日志失败{ex}");
        }

        unit.Dispose();

    }
}
