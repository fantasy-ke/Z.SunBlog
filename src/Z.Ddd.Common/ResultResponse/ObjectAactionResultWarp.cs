using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Z.Ddd.Common.Exceptions;
using Z.Ddd.Common.Extensions;

namespace Z.Ddd.Common.ResultResponse;

public class ObjectAactionResultWarp : IActionResultWarp
{
    public void Wrap(FilterContext context)
    {
        ObjectResult? objectResult = null;

        switch (context)
        {
            case ResultExecutingContext resultExecutingContext:
                objectResult = resultExecutingContext.Result as ObjectResult;
                break;
        }

        if (objectResult == null)
        {
            throw new UserFriendlyException("Action Result should be JsonResult!");
        }

        if (!(objectResult.Value is ZResponseBase))
        {
            var response = new ZEngineResponse();
            response.Result = objectResult.Value;
            response.StatusCode = StatusCodes.Status200OK;
            response.Success = true;
            response.Extras = HttpExtension.Take();
            objectResult.Value = response;
            objectResult.DeclaredType = typeof(ZEngineResponse);
        }
    }
}
