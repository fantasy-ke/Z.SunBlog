using System.Collections.Generic;
using Z.OSSCore.Models.Policy;

namespace Z.OSSCore
{
    public class PolicyInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<StatementItem> Statement { get; set; }
    }
}
