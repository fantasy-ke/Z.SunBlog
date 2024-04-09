using Microsoft.Extensions.Options;
using Z.Fantasy.Core.DomainServiceRegister.Domain;
using Z.OSSCore;
using Z.OSSCore.EntityType;
using Z.OSSCore.Interface;
using Z.OSSCore.Models.Dto;

namespace Z.SunBlog.Core.MinioFileModule.DomainManager
{
    public class MinioFileManager : DomainService, IMinioFileManager
    {
        private readonly IOSSService<OSSAliyun> _ossService;
        private readonly OSSOptions _ossOptions;
        public MinioFileManager(IServiceProvider serviceProvider, IOptions<OSSOptions> minioOptions, IOSSService<OSSAliyun> ossService = null) : base(serviceProvider)
        {
            _ossService = ossService;
            _ossOptions = minioOptions.Value;
        }

        public async Task DeleteMinioFileAsync(OperateObjectInput input)
        {
            await _ossService.RemoveObjectAsync(input);
        }

        public async Task<ObjectOutPut> GetFile(string fileUrl)
        {
            var obj = new GetObjectInput()
            {
                ObjectName = fileUrl,
                BucketName = _ossOptions.DefaultBucket
            };

            return await _ossService.GetObjectAsync(obj);
        }

        public async Task UploadMinio(Stream stream, string file, string contentType)
        {
            var obj = new UploadObjectInput(_ossOptions.DefaultBucket
                , file
            , contentType
                , stream);

            await _ossService.UploadObjectAsync(obj);
        }
    }
}
