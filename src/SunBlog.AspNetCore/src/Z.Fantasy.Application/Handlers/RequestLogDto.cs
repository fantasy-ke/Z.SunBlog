using Z.EventBus.Attributes;
using Z.Foundation.Core.Entities.EntityLog;

namespace Z.Fantasy.Application.Handlers
{
    [EventDiscriptor("RequestLog", 1000, false)]
    public class RequestLogDto: ZRequestLog
    {
    }
}
