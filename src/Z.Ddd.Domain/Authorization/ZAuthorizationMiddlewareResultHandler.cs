using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Z.Ddd.Domain.ZResponse;
using Z.Module.DependencyInjection;

namespace Z.Ddd.Domain.Authorization
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
                var response = new ZAjaxResponse();
                response.UnAuthorizedRequest = true;
                response.StatusCode = "401";
                var error = new ErrorInfo();
                error.Error = isLogin ? $"你没有权限访问该接口-接口路由{path}" : "请先登录系统";
                response.Error = error;
                await context.Response.WriteAsJsonAsync(response);
                return;
            }
            await next(context);
        }
    }
}
