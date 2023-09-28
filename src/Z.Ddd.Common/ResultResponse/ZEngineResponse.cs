using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Ddd.Common.ResultResponse
{
    [Serializable]
    public class ZEngineResponse<TResult> : ZResponseBase
    {
        public TResult? Result { get; set; }

        public ZEngineResponse(TResult result)
        {
            Result = result;
            Success = true;
        }

        public ZEngineResponse()
        {
            Success = true;
        }

        public ZEngineResponse(ErrorInfo error, bool unAuthorizedRequest = false)
        {
            Error = error;
            UnAuthorizedRequest = unAuthorizedRequest;
            Success = false;
        }
    }

    [Serializable]
    public class ZEngineResponse : ZEngineResponse<object>
    {
        public ZEngineResponse(object result) : base(result)
        {
        }

        public ZEngineResponse(ErrorInfo error, bool unAuthorizedRequest) : base(error, unAuthorizedRequest)
        {

        }

        public ZEngineResponse(object result, bool _unAuthorizedRequest) : base(result)
        {
            UnAuthorizedRequest = _unAuthorizedRequest;
        }


        public ZEngineResponse() : base() { }
    }
}
