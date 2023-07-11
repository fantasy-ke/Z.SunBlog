using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Domain.Entities.IAuditing;

namespace Z.Ddd.Domain.Entities.Auditing;

public abstract class FullAuditedEntity : CreationAuditedEntity, IDeletionAuditedObject
{
    public virtual Guid? DeleterId { get; set; }

    public virtual DateTime? DeletionTime { get; set; }

    public virtual bool IsDeleted { get; set; }
}


public abstract class FullAuditedEntity<Tkey> : CreationAuditedEntity<Tkey>, IDeletionAuditedObject
{
    public virtual Guid? DeleterId { get; set; }

    public virtual DateTime? DeletionTime { get; set; }

    public virtual bool IsDeleted { get; set; }

    protected FullAuditedEntity(Tkey id) 
        : base(id)
    {

    }
}