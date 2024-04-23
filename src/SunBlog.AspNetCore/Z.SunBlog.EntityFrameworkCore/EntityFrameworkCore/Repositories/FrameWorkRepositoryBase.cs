using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFrameworkCore.Core;
using Z.Foundation.Core.Entities;
using Z.Foundation.Core.Entities.Repositories;

namespace Z.SunBlog.EntityFrameworkCore.EntityFrameworkCore.Repositories;

public abstract class FrameWorkRepositoryBase<TEntity, TKey> : EfCoreRepository<SunBlogDbContext, TEntity, TKey>
        where TEntity : class, IEntity<TKey>
{
    protected FrameWorkRepositoryBase(SunBlogDbContext dbContextProvider)
        : base(dbContextProvider)
    {
    }

}

public abstract class FrameWorkRepositoryBase<TEntity> : FrameWorkRepositoryBase<TEntity, int>, IRepository<TEntity>
    where TEntity : class, IEntity<int>
{
    protected FrameWorkRepositoryBase(SunBlogDbContext dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
