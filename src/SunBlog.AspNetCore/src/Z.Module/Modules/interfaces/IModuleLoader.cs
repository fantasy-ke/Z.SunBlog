using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Module.Modules.interfaces
{
    internal interface IModuleLoader
    {
        List<IZModuleDescritor> GetZModuleDescritors(
            IServiceCollection service,
            Type startupModuleType);
    }
}
