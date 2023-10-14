using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.Entities.Users;
using Z.NetWiki.Core.ArticleCategoryModule;

namespace Z.NetWiki.Core.ArticleCategoryModule.DomainManager
{
    public interface IArticleCategoryManager : IBusinessDomainService<ArticleCategory>
    {
    }
}
