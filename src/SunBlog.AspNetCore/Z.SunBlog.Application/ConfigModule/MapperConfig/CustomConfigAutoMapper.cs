// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using Z.SunBlog.Application.ArticleModule.BlogClient.Dto;
using Z.SunBlog.Application.CommentsModule.BlogClient.Dto;
using Z.SunBlog.Application.ConfigModule.Dto;
using Z.SunBlog.Core.ArticleModule;
using Z.SunBlog.Core.CommentsModule;
using Z.SunBlog.Core.CustomConfigModule;

namespace Z.SunBlog.Application.ConfigModule.MapperConfig
{
    public static class CustomConfigAutoMapper
    {
        /// <summary>
        /// 具体映射规则
        /// </summary>
        /// <param name="configuration"></param>
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CustomConfig, AddCustomConfigInput>().ReverseMap();
            configuration.CreateMap<CustomConfig, UpdateCustomConfigInput>().ReverseMap();
            configuration.CreateMap<CustomConfigItem, AddCustomConfigItemInput>().ReverseMap();
            //configuration.CreateMap<Article, AddArticleInput>().ReverseMap();
        }
    }
}
