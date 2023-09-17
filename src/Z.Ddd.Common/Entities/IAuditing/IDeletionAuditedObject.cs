using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Ddd.Common.Entities.IAuditing
{
    public interface IDeletionAuditedObject : IHasDeletionTime
    {
        string DeleterId { get; }
    }
}
