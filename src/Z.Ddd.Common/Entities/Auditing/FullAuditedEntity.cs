using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.Entities.IAuditing;

namespace Z.Ddd.Common.Entities.Auditing;

public abstract class FullAuditedEntity : CreationAuditedEntity, IDeletionAuditedObject
{
    public virtual string? DeleterId { get; set; }

    public virtual DateTime? DeletionTime { get; set; }

    public virtual bool? IsDeleted { get; set; }
}


public abstract class FullAuditedEntity<Tkey> : CreationAuditedEntity<Tkey>, IDeletionAuditedObject
{

    public FullAuditedEntity()
    {
        
    }
    public FullAuditedEntity(Tkey id)
    {
        Id = id;
    }
    public virtual string? DeleterId { get; set; }

    public virtual DateTime? DeletionTime { get; set; }

    public virtual bool? IsDeleted { get; set; }

}