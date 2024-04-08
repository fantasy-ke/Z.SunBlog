using Microsoft.Extensions.Options;
using Serilog;
using Z.EventBus.Handlers;
using Z.Module.DependencyInjection;
using Z.OSSCore;
using Z.OSSCore.EntityType;
using Z.OSSCore.Interface;
using Z.OSSCore.Models.Dto;

namespace Z.SunBlog.Core.Handlers.FileHandlers
{
    public class FileEventHandler : IEventHandler<FileEventDto>, ITransientDependency
    {
        private readonly IOSSService<OSSMinio> _minioService;
        private readonly OSSOptions _ossOptions;

        public FileEventHandler(IOSSService<OSSMinio> minioService, IOptions<OSSOptions> minioOptions)
        {
            _minioService = minioService;
            _ossOptions = minioOptions.Value;
        }

        public async Task HandelrAsync(FileEventDto eto)
        {
            Log.Information("FileEventHandler: {0}", eto.File);
            var obj = new UploadObjectInput(_ossOptions.DefaultBucket
                , eto.File
            , eto.ContentType
                , eto.Stream);

            await _minioService.UploadObjectAsync(obj);
        }
    }
}
