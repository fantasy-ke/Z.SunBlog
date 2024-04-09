### 🎨模块化类库，参照AbpVnext实现，现已正常使用

- Z.OSSCore`1.0.0`
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
              },
      }

        ```
    