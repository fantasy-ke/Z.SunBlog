using Z.Module.DependencyInjection;
using Z.OSSCore.Models;

namespace Z.OSSCore.Interface.Service
{
    public interface IAliyunOSSService<T> : IOSSService<T>
    {
        /// <summary>
        /// 获取储存桶地域
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        Task<string> GetBucketLocationAsync(string bucketName);

        /// <summary>
        /// 管理桶跨域访问
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="rules"></param>
        /// <returns></returns>
        Task<bool> SetBucketCorsRequestAsync(string bucketName, List<BucketCorsRule> rules);

        /// <summary>
        /// 获取桶外部访问URL
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        Task<string> GetBucketEndpointAsync(string bucketName);
    }
}
