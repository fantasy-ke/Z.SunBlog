using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Z.Foundation.Core.Exceptions;
using Z.OSSCore.Interface;
using Z.OSSCore.Services;

namespace Z.OSSCore
{
    public class OSSServiceFactory : IOSSServiceFactory
    {
        private readonly IOptionsMonitor<OSSOptions> optionsMonitor;
        private readonly ICacheProvider _cache;
        private readonly ILoggerFactory logger;

        public OSSServiceFactory(IOptionsMonitor<OSSOptions> optionsMonitor
            , ICacheProvider provider
            , ILoggerFactory logger)
        {
            this.optionsMonitor = optionsMonitor ?? throw new ArgumentNullException();
            _cache = provider ?? throw new ArgumentNullException(nameof(IMemoryCache));
            this.logger = logger ?? throw new ArgumentNullException(nameof(ILoggerFactory));
        }

        public IOSSService Create()
        {
            return Create(DefaultOptionName.Name);
        }

        public IOSSService Create(string name)
        {
            #region 参数验证

            if (string.IsNullOrEmpty(name))
            {
                name = DefaultOptionName.Name;
            }
            var options = optionsMonitor.Get(name);
            if (options == null ||
                options.Provider == OSSProvider.Invalid
                && string.IsNullOrEmpty(options.Endpoint)
                && string.IsNullOrEmpty(options.SecretKey)
                && string.IsNullOrEmpty(options.AccessKey))
                throw new ArgumentException($"Cannot get option by name '{name}'.");
            if (options.Provider == OSSProvider.Invalid)
                throw new ArgumentNullException(nameof(options.Provider));
            if (string.IsNullOrEmpty(options.SecretKey))
                throw new ArgumentNullException(nameof(options.SecretKey), "SecretKey can not null.");
            if (string.IsNullOrEmpty(options.AccessKey))
                throw new ArgumentNullException(nameof(options.AccessKey), "AccessKey can not null.");
            if ((options.Provider == OSSProvider.Minio
                || options.Provider == OSSProvider.QCloud)
                && string.IsNullOrEmpty(options.Region))
            {
                throw new ArgumentNullException(nameof(options.Region), "When your provider is Minio, region can not null.");
            }

            #endregion

            switch (options.Provider)
            {
                case OSSProvider.Aliyun:
                    return new AliyunOSSService(_cache, options);
                case OSSProvider.Minio:
                    return new MinioOSSService(_cache, options);
                default:
                    throw new Exception("Unknow provider type");
            }
        }
    }
}