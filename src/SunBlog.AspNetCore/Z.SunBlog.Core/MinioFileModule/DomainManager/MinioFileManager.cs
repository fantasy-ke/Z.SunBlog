using Microsoft.Extensions.Options;
using Z.Fantasy.Core.DomainServiceRegister.Domain;
using Z.Fantasy.Core.Minio;
using Z.OSSCore.Interface.Service;
using Z.OSSCore.Models.Dto;
using GetObjectInput = Z.OSSCore.Models.Dto.GetObjectInput;
using ObjectOutPut = Z.OSSCore.Models.Dto.ObjectOutPut;
using UploadObjectInput = Z.OSSCore.Models.Dto.UploadObjectInput;

namespace Z.SunBlog.Core.MinioFileModule.DomainManager
{
    public class MinioFileManager : DomainService, IMinioFileManager
    {
        private readonly IMinioOSSService _minioService;
        private readonly MinioConfig _minioOptions;
        public MinioFileManager(IServiceProvider serviceProvider, IMinioOSSService minioService, IOptions<MinioConfig> minioOptions) : base(serviceProvider)
        {
            _minioService = minioService;
            _minioOptions = minioOptions.Value;
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
                BucketName = _minioOptions.DefaultBucket
            };

            return await _minioService.GetObjectAsync(obj);
        }

        public async Task UploadMinio(Stream stream, string file, string contentType)
        {
            var obj = new UploadObjectInput(_minioOptions.DefaultBucket
                , file
            , contentType
                , stream);

            await _minioService.UploadObjectAsync(obj);
        }
    }
}
