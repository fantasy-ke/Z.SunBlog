using Minio;

namespace Z.OSSCore.Interface
{
    public interface IOSSServiceFactory<T>
    {
        IOSSService<T> Create();
    }
}