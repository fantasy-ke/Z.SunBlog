using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Module.Modules.interfaces;

namespace Z.Module.Modules
{
    public class DependOnAttribute : Attribute, IDependsAttrProvider
    {
        public Type[] DependedTypes { get;}
        public DependOnAttribute(params Type[] types) 
        {
            DependedTypes = types ?? new Type[0];
        }
        public Type[] GetDependsModulesType()
        {
            return DependedTypes;
        }
    }
}
