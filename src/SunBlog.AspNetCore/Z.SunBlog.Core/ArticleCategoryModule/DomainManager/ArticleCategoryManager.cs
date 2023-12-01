using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.DomainServiceRegister.Domain;
using Z.Ddd.Common.Entities.Users;
using Z.Ddd.Common.Exceptions;
using Z.SunBlog.Core.ArticleCategoryModule;

namespace Z.SunBlog.Core.ArticleCategoryModule.DomainManager
{
    public class ArticleCategoryManager : BusinessDomainService<ArticleCategory>, IArticleCategoryManager
    {
        public ArticleCategoryManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override async Task ValidateOnCreateOrUpdate(ArticleCategory entity)
        {
            await Task.CompletedTask;
        }

    }
}
