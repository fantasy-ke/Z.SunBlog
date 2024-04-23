﻿﻿﻿### 🎨OSS类库，现已实现ALiyun和Minio正常使用

- 基于扩展包[OnceMi.AspNetCore.OSS](https://github.com/oncemi/OnceMi.AspNetCore.OSS)修改了一下工厂实现，传入指定类型使用指定的储存

  - 新增了三个方法

  - ```C#
    //获取
    public async Task<ObjectOutPut> GetObjectAsync(GetObjectInput input)
    {
    	//.......
    }
    //上传
    public async Task<bool> UploadObjectAsync(UploadObjectInput input)
    {
        //.......
    }
    //删除
    public Task<bool> RemoveObjectAsync(OperateObjectInput input)
    {
        //.......
    }
    ```

- Z.OSSCore`1.0.1`

  - 加入腾讯云OSS `OSSQCloud`

- Z.OSSCore`1.0.0`
  - 基础Options实体 以及类型枚举
    ``` C# 
    public enum OSSProvider
    {
        /// <summary>
        /// 无效
        /// </summary>
        Invalid = 0,
    
        /// <summary>
        /// Minio自建对象储存
        /// </summary>
        Minio = 1,
    
        /// <summary>
        /// 阿里云OSS
        /// </summary>
        Aliyun = 2,
    
        /// <summary>
        /// 腾讯云OSS
        /// </summary>
        QCloud = 3,
    }
    
    public class OSSOptions
    {
        /// <summary>
        /// BucketName
        /// </summary>
        public string DefaultBucket { get; set; }
        /// <summary>
        /// 枚举，OOS提供商
        /// </summary>
        public OSSProvider Provider { get; set; }
        
        //.......
    }
    ```
  - 服务注册
    ``` C# 
        /// <summary>
        /// 配置默认配置
        /// 使用key默认"App:SSOConfig"，可以自己配置不同的路径获取
        /// </summary>
        public static IServiceCollection AddOSSService(this IServiceCollection services, string key = "App:SSOConfig", Action<OSSOptions> oSSOptions = null)
        {
    
        }
    
        /// <summary>
        /// 配置默认配置
        /// </summary>
        public static IServiceCollection AddOSSService(this IServiceCollection services, Action<OSSOptions> option)
        {
            return services.AddOSSService(oSSOptions: option);
        }
    ```
  - json配置
    ``` C#
    "App": {
          "SSOConfig": {
              "Enable": false,//是否开启
              "Endpoint": "oss-cn-guangzhou.aliyuncs.com",//桶的地址
              "AccessKey": "**********",
              "SecretKey": "***********",
              "DefaultBucket": "sunblog",//默认Bucket名称
              "IsEnableHttps": true,//开启Https
              "IsEnableCache": true,
              "Provider": "Minio"
          },
    }
    
    ```
    
  - 依赖注入使用
    ``` C#
    public class MinioFileManager : DomainService, IMinioFileManager
    {
        private readonly IOSSService<OSSAliyun> _ossService;
        private readonly OSSOptions _ossOptions;
        public MinioFileManager(IServiceProvider serviceProvider, IOptions<OSSOptions> minioOptions, IOSSService<OSSAliyun> ossService = null) : base(serviceProvider)
        {
            _ossService = ossService;
            _ossOptions = minioOptions.Value;
        }
    」
    
    ```
    - 其中`OSSAliyun`是不同类型使用的泛型
      - 现有`OSSAliyun`和`OSSMinio`

    - 安装以上的步骤服务注册以及注入，就可正常使用
      - 常用部分方法
      - ``` C#
        namespace Z.OSSCore.Interface
         {
             public interface IOSSService<T>
             {
                  /// <summary>
                  /// 检查存储桶是否存在。
                  /// </summary>
                  /// <param name="bucketName">存储桶名称。</param>
                  /// <returns></returns>
                  Task<bool> BucketExistsAsync(string bucketName);
         
                 /// <summary>
                  /// 上传文件对象
                  /// </summary>
                  /// <param name="input"></param>
                  /// <returns></returns>
                  Task<bool> UploadObjectAsync(UploadObjectInput input);
         
               /// <summary>
                  /// 返回文件数据
                  /// </summary>
                  /// <param name="input"></param>
                  /// <returns></returns>
                  Task<ObjectOutPut> GetObjectAsync(GetObjectInput input);
         
                 /// <summary>
                  /// 删除一个对象。
                  /// </summary>
                  /// <param name="bucketName">存储桶名称。</param>
                  /// <param name="objectName">存储桶里的对象名称。</param>
                  /// <returns></returns>
                  Task<bool> RemoveObjectAsync(OperateObjectInput input);
         
                 /// <summary>
                  /// 清除Presigned Object缓存
                  /// </summary>
                  /// <param name="bucketName"></param>
                  /// <param name="objectName"></param>
                  Task RemovePresignedUrlCache(OperateObjectInput input);
          	}
         }
        