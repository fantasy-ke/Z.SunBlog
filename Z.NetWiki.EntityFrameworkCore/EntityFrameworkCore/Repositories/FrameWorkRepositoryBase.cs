using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.Entities.Repositories;
using Z.Ddd.Common.Entities;
using Z.EntityFrameworkCore.Core;

namespace Z.NetWiki.EntityFrameworkCore.EntityFrameworkCore.Repositories;

public abstract class FrameWorkRepositoryBase<TEntity, TKey> : EfCoreRepository<NetWikiDbContext, TEntity, TKey>
        where TEntity : class, IEntity<TKey>
{
    protected FrameWorkRepositoryBase(NetWikiDbContext dbContextProvider)
        : base(dbContextProvider)
    {
    }

}

public abstract class FrameWorkRepositoryBase<TEntity> : FrameWorkRepositoryBase<TEntity, int>, IRepository<TEntity>
    where TEntity : class, IEntity<int>
{
    protected FrameWorkRepositoryBase(NetWikiDbContext dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
