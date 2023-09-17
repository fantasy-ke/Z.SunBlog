using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Ddd.Common.Authorization.Authorize;

public class Permission
{
    public string Name { get; set; }

    public string Code { get; set; }

    public string? ParentCode { get; set; }

    public List<Permission> Children { get; set; }

    public virtual void CreateChildren(string name)
    {
        Children = Children ?? new List<Permission>();
        Children.Add(new Permission()
        {
            Name = name,
            ParentCode = this.ParentCode
        });
    }
}
