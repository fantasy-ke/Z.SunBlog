using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Module.Modules;

namespace Z.Module
{
    public class ZModuleApplicationServiceProvider : ZModuleApplicationBase, IZApplicationServiceProvider
    {
        public ZModuleApplicationServiceProvider(Type startModuleType, IServiceCollection services) : base(startModuleType, services)
        {
            services.AddSingleton<IZApplicationServiceProvider>(this);
        }

        public void Initialize(IServiceProvider serviceProvider)
        {
            SetServiceProvider(serviceProvider);
            InitializeModules();
        }
    }
}
