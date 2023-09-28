using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.Entities.Auditing;

namespace Z.Ddd.Common.Entities.Users
{
    public class ZUserInfo : FullAuditedEntity<string>
    {

        /// <summary>
        /// 昵称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string? PassWord { get; set; }

        public ZUserInfo()
        {
            
        }

        public ZUserInfo(string id) : base(id)
        {
        }
    }
}
