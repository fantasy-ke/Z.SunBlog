// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using Z.NetWiki.Application.TagsModule.BlogServer.Dto;
using Z.NetWiki.Domain.ArticleModule;
using Z.NetWiki.Domain.TagModule;

namespace Z.NetWiki.Application.TagsModule.BlogServer.MapperConfig
{
    public static class TagsAutoMapper
    {
        /// <summary>
        /// 具体映射规则
        /// </summary>
        /// <param name="configuration"></param>
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Tags, CreateOrUpdateTagInput>().ReverseMap();
            configuration.CreateMap<Article, AddTagInput>().ReverseMap();
        }
    }
}
