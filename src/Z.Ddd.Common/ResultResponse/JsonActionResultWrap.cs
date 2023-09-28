using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResultExecutingContext = Microsoft.AspNetCore.Mvc.Filters.ResultExecutingContext;
using Microsoft.AspNetCore.Http;
using Z.Ddd.Common.Exceptions;

namespace Z.Ddd.Common.ResultResponse;

public class JsonActionResultWrap : IActionResultWarp
{
    public void Wrap(FilterContext context)
    {

        JsonResult? jsonResult = null;

        switch (context)
        {
            case ResultExecutingContext resultExecutingContext:
                jsonResult = resultExecutingContext.Result as JsonResult;
                break;
        }

        if (jsonResult == null)
        {
            throw new UserFriendlyException("Action Result should be JsonResult!");
        }

        if (!(jsonResult.Value is ZResponseBase))
        {
            var response = new ZEngineResponse();
            response.Result = jsonResult.Value;
            response.Success = true;
            response.StatusCode = "200";
            jsonResult.Value = response;
        }
    }
}
