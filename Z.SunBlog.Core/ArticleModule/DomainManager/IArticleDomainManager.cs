﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.DomainServiceRegister.Domain;
using Z.Ddd.Common.Entities.Users;

namespace Z.SunBlog.Core.ArticleModule.DomainManager
{
    public interface IArticleDomainManager : IBusinessDomainService<Article>
    {
    }
}
