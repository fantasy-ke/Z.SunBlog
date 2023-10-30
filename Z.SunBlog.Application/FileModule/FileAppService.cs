
using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using Yitter.IdGenerator;
using Z.Ddd.Common.Attributes;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.Minio;
using Z.Module.DependencyInjection;
using Z.SunBlog.Application.FileModule.Dto;
using Z.SunBlog.Core.Const;
using Z.SunBlog.Core.MinioFileModule.DomainManager;
using static System.Net.WebRequestMethods;

namespace Z.SunBlog.Application.FileModule;

public interface IFileAppService : IApplicationService, ITransientDependency
{
    Task<List<UploadFileOutput>> UploadFile(IFormFile file);
}

public class FileAppService : ApplicationService, IFileAppService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IMinioFileManager _minioFileManager;
    private readonly MinioConfig _minioOptions;

    public FileAppService(IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment webHostEnvironment,
        IMinioFileManager minioFileManager,
         IOptions<MinioConfig> minioOptions) : base(serviceProvider)
    {
        _httpContextAccessor = httpContextAccessor;
        _webHostEnvironment = webHostEnvironment;
        _minioFileManager = minioFileManager;
        _minioOptions = minioOptions.Value;
    }

    /// <summary>
    /// 上传附件
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public async Task<List<UploadFileOutput>> UploadFile(IFormFile file)
    {
        if (file is null or { Length: 0 })
        {
            throw new Exception("请上传文件");
        }
        string extension = Path.GetExtension(file.FileName);
        if (string.IsNullOrWhiteSpace(extension))
        {
            throw new Exception("无效文件");
        }
        //文件路径

        var minioname = $"{ZSunBlogConst.MinioAvatar}_{Guid.NewGuid().ToString("N")}{extension}";
        // 文件完整名称
        var now = DateTime.Today;
        string filePath = $"/{now.Year}/{now.Month:D2}-{now.Day:D2}/";
        var fileUrl = $"{filePath}{minioname}";
        var request = _httpContextAccessor.HttpContext!.Request;
        if (!_minioOptions.Enable)
        {
            filePath = string.Concat(_minioOptions.DefaultBucket!.TrimEnd('/'), filePath);
            var webrootpath = _webHostEnvironment.WebRootPath;
            string s = Path.Combine(webrootpath, filePath);
            if (!Directory.Exists(s))
            {
                Directory.CreateDirectory(s);
            }

            var stream = System.IO.File.Create($"{s}{minioname}");
            await file.CopyToAsync(stream);
            await stream.DisposeAsync();
            fileUrl = string.Concat(_minioOptions.DefaultBucket.TrimEnd('/'), fileUrl);
            string url = $"{request.Scheme}://{request.Host.Value}/{fileUrl}";
            return new List<UploadFileOutput>()
            {
                new()
                {
                    Name = minioname,
                    Url = url
                }
            };
        }

        await _minioFileManager.UploadMinio(file.OpenReadStream(), fileUrl, file.ContentType);
        return new List<UploadFileOutput>()
        {
            new()
            {
                Name = minioname,
                Url = $"{request.Scheme}://{_minioOptions.Host!.TrimEnd('/')}/{string.Concat(_minioOptions.DefaultBucket!.TrimEnd('/'), fileUrl)}"
            }
        };
    }
}