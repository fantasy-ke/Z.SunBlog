using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Module.Extensions;
using Z.Module.Modules.interfaces;

namespace Z.Module.Modules
{
    public abstract class ZModuleApplicationBase : IZModuleApplication
    {
        public Type StartupModuleType { get; }

        public IServiceCollection Services { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }

        public IReadOnlyList<IZModuleDescritor> Modules { get; private set; }

        internal ZModuleApplicationBase(Type startupModuleType, IServiceCollection services)
        {
            ZModule.CheckModuleType(startupModuleType);
            services.CheckNull();
            Services = services;
            StartupModuleType = startupModuleType;
            ConfigerService();
        }

        public void ConfigerService()
        {
            var context = new ServiceConfigerContext(Services);

            ////模块里的ServiceConfigerContext赋值
            foreach (var module in Modules)
            {
                if (module.Instance is ZModule abpModule)
                {
                    abpModule.ServiceConfigerContext = context;
                }
            }

            //PreConfigureServices
            foreach (var module in Modules.Where(m => m.Instance is IPreConfigureServices))
            {
                try
                {
                    module.Instance.PreConfigureServices(context);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"期间发生错误 {nameof(IPreConfigureServices.PreConfigureServices)} 模块阶段 {module.Type.AssemblyQualifiedName}.有关详细信息，请参阅内部异常。.", ex);
                }
            }

            //ConfigureServices
            foreach (var module in Modules)
            {
                try
                {
                    module.Instance.ConfigureServices(context);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"期间发生错误  {nameof(IZModule.ConfigureServices)}  模块阶段  {module.Type.AssemblyQualifiedName}.有关详细信息，请参阅内部异常。", ex);
                }
            }
            //清空模块里的ServiceConfigerContext
            foreach (var module in Modules)
            {
                if (module.Instance is ZModule abpModule)
                {
                    abpModule.ServiceConfigerContext = null;
                }
            }


        }

        public void InitializeModules(IServiceCollection services)
        {
            throw new NotImplementedException();
        }
    }
}
