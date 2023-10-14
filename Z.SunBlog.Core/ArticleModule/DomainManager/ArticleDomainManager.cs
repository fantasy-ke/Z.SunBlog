using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.Entities.Users;
using Z.Ddd.Common.Exceptions;

namespace Z.SunBlog.Core.ArticleModule.DomainManager
{
    public class ArticleDomainManager : BusinessDomainService<Article>, IArticleDomainManager
    {
        public ArticleDomainManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override async Task ValidateOnCreateOrUpdate(Article entity)
        {
            var count = await Query
                .Where(a => a.Title == entity.Title && a.Id != entity.Id).CountAsync();

            if (count > 0)
            {
                this.ThrowRepetError(entity.Title);
            }
        }

    }
}
