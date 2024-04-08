using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Z.OSSCore.Models.Policy
{
    public class StatementItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string Effect { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Principal Principal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Action { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Resource { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [JsonIgnore]
        public bool IsDelete { get; set; } = false;
    }
}
