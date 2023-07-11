using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Ddd.Domain.Entities.IAuditing
{
    public interface IMayHaveCreator
    {
        /// <summary>
        /// Id of the creator.
        /// </summary>
        string CreatorId { get; }
    }
}
