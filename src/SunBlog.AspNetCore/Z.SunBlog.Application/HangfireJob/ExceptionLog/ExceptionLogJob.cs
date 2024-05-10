using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Z.Foundation.Core.AutofacExtensions;
using Z.Foundation.Core.Entities.EntityLog;
using Z.Foundation.Core.Entities.Repositories;
using Z.Foundation.Core.UnitOfWork;
using Z.HangFire.BackgroundJobs.Abstractions;
using Z.Module.DependencyInjection;

namespace Z.SunBlog.Application.HangfireJob.ExceptionLog;

/// <summary>
/// 异常日志
/// </summary>
public class ExceptionLogJob : BackgroundScheduleJobBase, ITransientDependency
{
    /// <summary>
    /// 构造函数 每5天执行一次
    /// </summary>
    public ExceptionLogJob(IServiceProvider serviceProvider = null) : base(serviceProvider)
    {
        ServiceProvider = serviceProvider ?? IOCManager.GetService<IServiceProvider>();
        Id = "exceptionlogjob";
        CronExpression = "0 0 */5 * *";
    }

    /// <summary>
    /// 重新任务删除30天前的异常日志
    /// </summary>
    /// <returns></returns>
    public override async Task DoWorkAsync()
    {
        await using var scope = ServiceProvider.CreateAsyncScope();
        using var unit = scope.ServiceProvider.GetService<IUnitOfWork>();
        try
        {
            await unit.BeginTransactionAsync();
            var _requestLogRepository = scope.ServiceProvider.GetRequiredService<IBasicRepository<ZExceptionLog>>();
            await _requestLogRepository.DeleteAsync(c => c.CreationTime < DateTime.Now.AddDays(-30));
            await unit.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await unit.RollbackTransactionAsync();
            Log.Error( $"定时清除异常日志失败{ex}");
        }

        unit.Dispose();
        
    }
}
