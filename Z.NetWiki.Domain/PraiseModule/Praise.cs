using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.Entities.Auditing;

namespace Z.NetWiki.Domain.PraiseModule;

public class Praise:FullAuditedEntity<Guid>
{
    /// <summary>
    /// 用户ID 
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 点赞对象ID
    /// </summary>
    public Guid ObjectId { get; set; }
}
