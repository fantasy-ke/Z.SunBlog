using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Ddd.Common.AutoMapper;

public class AutoConfigProfile : Profile
{
    public AutoConfigProfile()
    {
        //博文章
        //CreateMap<BlogArticle, BlogViewModels>().ReverseMap();
    }
}
