using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Ddd.Common.AutoMapper;

/// <summary>
/// 静态全局 AutoMapper 配置文件
/// </summary>
public class AutoMapperConfig
{
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoConfigProfile());
        });
    }
}