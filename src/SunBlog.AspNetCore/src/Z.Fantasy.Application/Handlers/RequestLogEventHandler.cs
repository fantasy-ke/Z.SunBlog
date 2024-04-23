using Microsoft.Extensions.Logging;
using Z.EventBus.Handlers;
using Z.Module.DependencyInjection;
using Z.Foundation.Core.AutofacExtensions;
using Z.Foundation.Core.Entities.EntityLog;
using Z.Foundation.Core.Entities.Repositories;
using Z.Foundation.Core.UnitOfWork;

namespace Z.Fantasy.Application.Handlers
{
    public class RequestLogEventHandler : IEventHandler<RequestLogDto>, ITransientDependency
    {
        private Microsoft.Extensions.Logging.ILogger _logger;
        public RequestLogEventHandler(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger<RequestLogEventHandler>();
        }
        public Task HandelrAsync(RequestLogDto eto)
        {
            using var unit = IOCManager.GetService<IUnitOfWork>();
            try
            {
                unit.BeginTransaction();
                var service = IOCManager.GetService<IBasicRepository<ZRequestLog>>();
                var entity = service.Insert(new ZRequestLog()
                {
                    RequestUri = eto.RequestUri,
                    RequestType = eto.RequestType,
                    RequestData = eto.RequestData,
                    ResponseData = eto.ResponseData,
                    UserId = eto.UserId,
                    UserName = eto.UserName,
                    ClientIP = eto.ClientIP,
                    SpendTime = eto.SpendTime,
                    UserOS = eto.UserOS,
                    UserAgent = eto.UserAgent,
                });
                unit.CommitTransaction();
            }
            catch (Exception ex)
            {
                unit.RollbackTransaction();
                _logger.LogError(ex, $"插入请求日志失败={ex.Message}（requestMethod={eto.RequestType}，requestUri={eto.RequestUri}，ipAddress={eto.ClientIP}）");
            }

            unit.Dispose();
            return Task.CompletedTask;
        }
    }
}
