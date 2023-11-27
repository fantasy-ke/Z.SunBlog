// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using Z.SunBlog.Application.ArticleModule.BlogClient.Dto;
using Z.SunBlog.Application.CommentsModule.BlogClient.Dto;
using Z.SunBlog.Core.ArticleModule;
using Z.SunBlog.Core.CommentsModule;

namespace Z.SunBlog.Application.CommentsModule.BlogClient.MapperConfig
{
    public static class CommentsCAutoMapper
    {
        /// <summary>
        /// 具体映射规则
        /// </summary>
        /// <param name="configuration"></param>
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comments, AddCommentInput>().ReverseMap();
            //configuration.CreateMap<Article, AddArticleInput>().ReverseMap();
        }
    }
}
