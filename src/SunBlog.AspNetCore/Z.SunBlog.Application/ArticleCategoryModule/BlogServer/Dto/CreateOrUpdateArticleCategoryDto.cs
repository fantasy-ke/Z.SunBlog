using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.SunBlog.Application.ArticleCategoryModule.BlogServer.Dto
{
    public class CreateOrUpdateArticleCategoryDto
    {
        public Guid ArticleId {  get; set; }
        public Guid CategoryId {  get; set; }
    }
}
