using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.DomainServiceRegister.Domain;
using Z.Ddd.Common.Entities.Repositories;
using Z.Ddd.Common.Entities;
using Z.Module.DependencyInjection;

namespace Z.Ddd.Common.DomainServiceRegister;

public interface IBasicDomainService<TEntity, TPrimaryKey> : IDomainService, ITransientDependency where TEntity : class, IEntity<TPrimaryKey>
{
    //
    // 摘要:
    //     实体
    IBasicRepository<TEntity, TPrimaryKey> EntityRepo { get; }

    //
    // 摘要:
    //     查询器
    IQueryable<TEntity> Query { get; }

    //
    // 摘要:
    //     查询器 - 不追踪
    IQueryable<TEntity> QueryAsNoTracking { get; }

    //
    // 摘要:
    //     根据id查找
    //
    // 参数:
    //   id:
    Task<TEntity> FindByIdAsync(TPrimaryKey id);

    //
    // 摘要:
    //     创建
    //
    // 参数:
    //   entity:
    //
    //   createAndGetId:
    //     是否获取id
    Task Create(TEntity entity);

    //
    // 摘要:
    //     批量创建
    //
    // 参数:
    //   entities:
    //     实体对象
    //
    //   createAndGetId:
    //     是否获取id
    Task Create(IEnumerable<TEntity> entities);

    //
    // 摘要:
    //     更新
    //
    // 参数:
    //   entity:
    Task Update(TEntity entity);

    //
    // 摘要:
    //     更新
    //
    // 参数:
    //   entities:
    //     实体对象
    Task Update(IEnumerable<TEntity> entities);

    //
    // 摘要:
    //     删除
    //
    // 参数:
    //   id:
    Task Delete(TPrimaryKey id);

    //
    // 摘要:
    //     删除
    //
    // 参数:
    //   entity:
    Task Delete(TEntity entity);

    //
    // 摘要:
    //     删除 - 按条件
    //
    // 参数:
    //   predicate:
    Task Delete(Expression<Func<TEntity, bool>> predicate);

    //
    // 摘要:
    //     批量删除
    //
    // 参数:
    //   idList:
    Task Delete(List<TPrimaryKey> idList);

    //
    // 摘要:
    //     是否存在
    //
    // 参数:
    //   id:
    Task<bool> Exist(TPrimaryKey id);

    //
    // 摘要:
    //     创建或更新
    //
    // 参数:
    //   entity:
    Task<TEntity> CreateOrUpdate(Expression<Func<TEntity, bool>> predicate,
        TEntity entity);

    //
    // 摘要:
    //     批量创建或更新
    //
    // 参数:
    //   entities:
    Task CreateOrUpdate(Expression<Func<TEntity, bool>> predicate, IEnumerable<TEntity> entities);
}
