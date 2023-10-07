using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Z.Ddd.Common.ResultResponse;
using Z.Module.DependencyInjection;

namespace Z.Ddd.Common.Authorization
{
    public class ZAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler, ISingletonDependency
    {
        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (!authorizeResult.Succeeded || authorizeResult.Challenged)
            {
                var reason = authorizeResult?.AuthorizationFailure?.FailureReasons.FirstOrDefault();
                var isLogin = context?.User?.Identity?.IsAuthenticated ?? false;
                var path = context?.Request?.Path ?? "";
                context!.Response.StatusCode = StatusCodes.Status401Unauthorized;
                var error = new ErrorInfo();
                error.Error = isLogin ? reason?.Message : "请先登录系统";
                var response = new ZEngineResponse(error, true);
                response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(response);
                return;
            }
            await next(context);
        }
    }
}
