using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.EntityFrameworkCore.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class DisabledUnitOfWorkAttribute : Attribute
{
    public readonly bool Disabled;

    public DisabledUnitOfWorkAttribute(bool disabled = true)
    {
        Disabled = disabled;
    }
}
