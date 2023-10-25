// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using Z.SunBlog.Application.MenuModule.Dto;
using Z.SunBlog.Core.MenuModule;
using Z.SunBlog.Core.SharedDto;

namespace Z.SunBlog.Application.MenuModule.MapperConfig
{
    public static class MenuAutoMapper
    {
        /// <summary>
        /// 具体映射规则
        /// </summary>
        /// <param name="configuration"></param>
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Menu, MenuPageOutput>().ReverseMap();
            configuration.CreateMap<Menu, AddSysMenuInput>().ReverseMap();
            configuration.CreateMap<UpdateSysMenuInput, Menu>().ReverseMap();
            configuration.CreateMap<Menu, TreeSelectOutput>()
                .ForMember(dest => dest.Value, src => src.MapFrom(c => c.Id))
                .ForMember(dest => dest.Label, src => src.MapFrom(c => c.Name));

            configuration.CreateMap<Menu, RouterOutput>()
                .ForMember(dest => dest.Name, src => src.MapFrom(c => c.RouteName))
                .ForMember(dest => dest.Component, src => src.MapFrom(c => c.Component))
                .ForMember(dest => dest.Path, src => src.MapFrom(c => c.Path))
                .ForMember(dest => dest.Meta, src => src.MapFrom(c => new RouterMetaOutput()
                {
                    Type = c.Type,
                    IsKeepAlive = c.IsKeepAlive,
                    Icon = c.Icon,
                    IsAffix = c.IsFixed,
                    IsHide = !c.IsVisible,
                    IsLink = c.Link,
                    Title = c.Name
                }));
        }
    }
}
