using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Ddd.Common.ZResponse
{
    public abstract class ZResponseBase
    {
        /// <summary>
        /// This property can be used to redirect user to a specified URL.
        /// </summary>
        public string TargetUrl { get; set; }

        /// <summary>
        /// Indicates success status of the result.
        /// Set <see cref="Error"/> if this value is false.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Error details (Must and only set if <see cref="Success"/> is false).
        /// </summary>
        public ErrorInfo Error { get; set; }

        /// <summary>
        /// This property can be used to indicate that the current user has no privilege to perform this request.
        /// </summary>
        public bool UnAuthorizedRequest { get; set; }

        public string StatusCode { get; set; }
    }
}
