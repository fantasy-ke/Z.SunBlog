using Microsoft.AspNetCore.Http;
using Z.Fantasy.Core.DomainServiceRegister.Domain;
using Z.Foundation.Core.Entities.Files;

namespace Z.SunBlog.Core.FileModule.FileManager
{
    public interface IFileInfoManager : IBusinessDomainService<ZFileInfo>
    {
        Task<string> UploadFileAsync(IFormFile file, string objectName);
    }
}
