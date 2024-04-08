using Minio;

namespace Z.OSSCore.Interface
{
    public interface IOSSServiceFactory
    {
        IOSSService Create();

        IOSSService Create(string name);
    }
}