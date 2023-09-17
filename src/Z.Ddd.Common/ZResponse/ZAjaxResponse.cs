using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Ddd.Common.ZResponse
{
    [Serializable]
    public class ZAjaxResponse<TResult> : ZResponseBase
    {
        public TResult? Result { get; set; }

        public ZAjaxResponse(TResult result)
        {
            Result = result;
            Success = true;
        }

        public ZAjaxResponse()
        {
            Success = true;
        }

        public ZAjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
        {
            Error = error;
            UnAuthorizedRequest = unAuthorizedRequest;
            Success = false;
        }
    }

    [Serializable]
    public class ZAjaxResponse : ZAjaxResponse<object>
    {
        public ZAjaxResponse(object result) : base(result)
        {
        }

        public ZAjaxResponse(ErrorInfo error, bool unAuthorizedRequest) : base(error, unAuthorizedRequest)
        {

        }

        public ZAjaxResponse(object result, bool _unAuthorizedRequest) : base(result)
        {
            UnAuthorizedRequest = _unAuthorizedRequest;
        }


        public ZAjaxResponse() : base() { }
    }
}
