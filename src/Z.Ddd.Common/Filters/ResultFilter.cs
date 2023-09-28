
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Z.Ddd.Common.Attributes;
using Z.Ddd.Common.ResultResponse;

namespace Z.Ddd.Common.ResultResponse;

public class ResultFilter : IResultFilter
{
    private readonly ILogger _logger;
    private readonly IActionResultWrapFactory _actionResultWrapFactory;

    public ResultFilter(ILogger<ResultFilter> logger, IActionResultWrapFactory actionResultWrapFactory)
    {
        _logger = logger;
        _actionResultWrapFactory = actionResultWrapFactory;
    }
    public void OnResultExecuted(ResultExecutedContext context)
    {
    }

    public void OnResultExecuting(ResultExecutingContext context)
    {
        var action = context.ActionDescriptor as ControllerActionDescriptor;
        var controllerAttribute = context.Controller
            .GetType()?
            .CustomAttributes?
            .FirstOrDefault(p => p.AttributeType == typeof(NoResultAttribute));
        if (controllerAttribute != null)
        {
            return;
        }
        var method = action?.MethodInfo;
        var nonResult = method?
            .CustomAttributes?
            .Where(p => p.AttributeType == typeof(NoResultAttribute));
        if (nonResult is null || !nonResult.Any())
        {
            _actionResultWrapFactory.CreateContext(context).Wrap(context);
        }
    }
}
