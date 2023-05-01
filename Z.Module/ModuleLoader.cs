using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Module.Extensions;
using Z.Module.Modules;
using Z.Module.Modules.interfaces;

namespace Z.Module
{
    public class ModuleLoader : IModuleLoader
    {
        public List<IZModuleDescritor> GetZModuleDescritors(IServiceCollection service,
            Type startupModuleType)
        {
            service.CheckNull();
            List<IZModuleDescritor> result = new();
            LoadModules(startupModuleType, result, service);
            //反向排序 保证被依赖的模块优先级高于依赖的模块
            result.Reverse();
            return result;
        }

        protected virtual void LoadModules(Type type, List<IZModuleDescritor> descritors, IServiceCollection services)
        {
            foreach (var item in ZModuleHelper.LoadModules(type))
            {
                descritors.Add(CreateModuleDescritor(item, services));
            }
        }


        protected virtual IZModule CreateAndRegisterModule(Type moduleType, IServiceCollection services)
        {
            var module = Activator.CreateInstance(moduleType) as IZModule;
            services.AddSingleton(moduleType, module);
            return module;
        }

        private IZModuleDescritor CreateModuleDescritor(Type type, IServiceCollection services)
        {
            return new ZModuleDescritor(type, CreateAndRegisterModule(type, services));
        }


    }
}
