// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using Z.NetWiki.Core.ArticleModule;

namespace Z.NetWiki.Application.ArticleModule
{
    public static class ArticleCAutoMapper
    {
        /// <summary>
        /// 具体映射规则
        /// </summary>
        /// <param name="configuration"></param>
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //configuration.CreateMap<Article, CreateOrUpdateArticleInput>().ReverseMap();
            //configuration.CreateMap<Article, AddArticleInput>().ReverseMap();
        }
    }
}
