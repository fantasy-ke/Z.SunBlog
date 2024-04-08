using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Z.Fantasy.Core.ResultResponse;

public class ActionEmptyResultWrap : IActionResultWarp
{
    public void Wrap(FilterContext context)
    {
        switch (context)
        {
            case ResultExecutingContext resultExecutingContext:
                var statusCode = context.HttpContext.Response.StatusCode;
                var isSuccess = statusCode == StatusCodes.Status200OK;
                resultExecutingContext.Result = new ObjectResult(new ZEngineResponse(statusCode, isSuccess));
                return;
        }
    }
}
