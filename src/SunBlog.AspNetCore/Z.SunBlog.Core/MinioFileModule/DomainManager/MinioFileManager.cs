using Microsoft.Extensions.Options;
using Z.Fantasy.Core.DomainServiceRegister.Domain;
using Z.OSSCore;
using Z.OSSCore.EntityType;
using Z.OSSCore.Interface;
using Z.OSSCore.Models.Dto;
using GetObjectInput = Z.OSSCore.Models.Dto.GetObjectInput;
using ObjectOutPut = Z.OSSCore.Models.Dto.ObjectOutPut;
using UploadObjectInput = Z.OSSCore.Models.Dto.UploadObjectInput;

namespace Z.SunBlog.Core.MinioFileModule.DomainManager
{
    public class MinioFileManager : DomainService, IMinioFileManager
    {
        private readonly IOSSService<OSSMinio> _minioService;
        private readonly OSSOptions _ossOptions;
        public MinioFileManager(IServiceProvider serviceProvider, IOSSService<OSSMinio> minioService, IOptions<OSSOptions> minioOptions) : base(serviceProvider)
        {
            _minioService = minioService;
            _ossOptions = minioOptions.Value;
        }

        public async Task DeleteMinioFileAsync(OperateObjectInput input)
        {
            await _minioService.RemoveObjectAsync(input);
        }

        public async Task<ObjectOutPut> GetFile(string fileUrl)
        {
            var obj = new GetObjectInput()
            {
                ObjectName = fileUrl,
                BucketName = _ossOptions.DefaultBucket
            };

            return await _minioService.GetObjectAsync(obj);
        }

        public async Task UploadMinio(Stream stream, string file, string contentType)
        {
            var obj = new UploadObjectInput(_ossOptions.DefaultBucket
                , file
            , contentType
                , stream);

            await _minioService.UploadObjectAsync(obj);
        }
    }
}
