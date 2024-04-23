using System.ComponentModel.DataAnnotations;
using Z.Foundation.Core.Entities.IAuditing;

namespace Z.Foundation.Core.Entities.Auditing;

public abstract class FullAuditedEntity : CreationAuditedEntity, IDeletionAuditedObject
{
    [MaxLength(32)]
    public virtual string DeleterId { get; set; }

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
    [MaxLength(32)]
    public virtual string DeleterId { get; set; }

    public virtual DateTime? DeletionTime { get; set; }

    public virtual bool? IsDeleted { get; set; }

}