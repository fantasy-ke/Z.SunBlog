using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.Entities.Users;
using Z.NetWiki.Domain.ArticleCategoryModule;

namespace Z.NetWiki.Domain.ArticleCategoryModule.DomainManager
{
    public interface IArticleCategoryManager : IBusinessDomainService<ArticleCategory>
    {
    }
}
