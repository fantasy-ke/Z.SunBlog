using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnceMi.AspNetCore.OSS;
using Z.Foundation.Core.Exceptions;
using Z.OSSCore.EntityType;
using Z.OSSCore.Interface;
using Z.OSSCore.Services;

namespace Z.OSSCore
{
    public class OSSServiceFactory<T> : IOSSServiceFactory<T>
    {
        private readonly OSSOptions _options;
        private readonly ICacheProvider _cache;
        private readonly ILoggerFactory logger;

        public OSSServiceFactory(IOptions<OSSOptions> optionsMonitor
            , ICacheProvider provider
            , ILoggerFactory logger)
        {
            _options = optionsMonitor.Value ?? throw new ArgumentNullException();
            _cache = provider ?? throw new ArgumentNullException(nameof(IMemoryCache));
            this.logger = logger ?? throw new ArgumentNullException(nameof(ILoggerFactory));
        }

        public IOSSService<T> Create()
        {
            #region 参数验证

            if (string.IsNullOrEmpty(_options.DefaultBucket))
            {
                _options.DefaultBucket = DefaultOptionName.Name;
            }
            if (_options == null ||
                _options.Provider == OSSProvider.Invalid
                && string.IsNullOrEmpty(_options.Endpoint)
                && string.IsNullOrEmpty(_options.SecretKey)
                && string.IsNullOrEmpty(_options.AccessKey))
                throw new ArgumentException($"Cannot get option by name '{ _options.DefaultBucket}'.");
            if (_options.Provider == OSSProvider.Invalid)
                throw new ArgumentNullException(nameof(_options.Provider));
            if (string.IsNullOrEmpty(_options.SecretKey))
                throw new ArgumentNullException(nameof(_options.SecretKey), "SecretKey can not null.");
            if (string.IsNullOrEmpty(_options.AccessKey))
                throw new ArgumentNullException(nameof(_options.AccessKey), "AccessKey can not null.");
            if ((_options.Provider == OSSProvider.Minio
                || _options.Provider == OSSProvider.QCloud)
                && string.IsNullOrEmpty(_options.Region))
            {
                throw new ArgumentNullException(nameof(_options.Region), "When your provider is Minio, region can not null.");
            }

            #endregion

            switch (_options.Provider)
            {
                case OSSProvider.Aliyun:
                    return new AliyunOSSService<T>(_cache, _options);
                case OSSProvider.Minio:
                    return new MinioOSSService<T>(_cache, _options);
                case OSSProvider.QCloud:
                    return new QCloudOSSService<T>(_cache, _options);
                default:
                    throw new Exception("Unknow provider type");
            }
        }
    }
}