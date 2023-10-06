using Microsoft.Extensions.DependencyInjection;
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
using Microsoft.EntityFrameworkCore;
using Z.Ddd.Common.Exceptions;

namespace Z.Ddd.Common.DomainServiceRegister;

public abstract class BasicDomainService<TEntity, TPrimaryKey> : DomainService, IBasicDomainService<TEntity, TPrimaryKey>, IDomainService, ITransientDependency where TEntity : class, IEntity<TPrimaryKey>
{
    public virtual IServiceProvider ServiceProvider { get; }

    //public virtual IAbpSession AbpSession { get; }

    public virtual IBasicRepository<TEntity, TPrimaryKey> EntityRepo { get; }

    public virtual IQueryable<TEntity> Query => EntityRepo.GetQueryAll();

    public virtual IQueryable<TEntity> QueryAsNoTracking => Query.AsNoTracking();

    public BasicDomainService(IServiceProvider serviceProvider):base(serviceProvider)
    {
        ServiceProvider = serviceProvider;
        EntityRepo = serviceProvider.GetRequiredService<IBasicRepository<TEntity, TPrimaryKey>>();
        //AbpSession = serviceProvider.GetRequiredService<IAbpSession>();
    }

    public abstract Task ValidateNdoOnCreateOrUpdate(TEntity entity);

    public virtual async Task<TEntity?> FindByIdAsync(TPrimaryKey id)
    {
        return await EntityRepo.GetAsync(id);
    }

    public virtual async Task<TEntity> Create(TEntity entity)
    {
        await ValidateNdoOnCreateOrUpdate(entity);
        return await EntityRepo.InsertAsync(entity);
    }

    public async Task Create(IEnumerable<TEntity> entities)
    {
        await EntityRepo.InsertManyAsync(entities);
    }

    public virtual async Task<TEntity> Update(TEntity entity)
    {
        await ValidateNdoOnCreateOrUpdate(entity);
        return await EntityRepo.UpdateAsync(entity);
    }

    public virtual async Task Update(IEnumerable<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            await Update(entity);
        }
    }

    public virtual async Task Delete(TPrimaryKey id)
    {
        await EntityRepo.DeleteIDAsync(id);
    }

    public virtual async Task Delete(TEntity entity)
    {
        await EntityRepo.DeleteAsync(entity);
    }

    public virtual async Task Delete(List<TPrimaryKey> idList)
    {
        if (idList != null && idList.Count != 0)
        {
            await EntityRepo.DeleteManyAsync(idList);
        }
    }

    public async Task Delete(Expression<Func<TEntity, bool>> predicate)
    {
        await EntityRepo.DeleteAsync(predicate);
    }

    public async Task<bool> Exist(TPrimaryKey id)
    {
        return await EntityRepo.CountAsync((TEntity o) => o.Id.Equals(id)) > 0;
    }

    public async Task<TEntity> CreateOrUpdate(Expression<Func<TEntity, bool>> predicate,
        TEntity entity)
    {
        return await EntityRepo.InsertOrUpdateAsync(predicate, entity);
    }

    public async Task CreateOrUpdate(Expression<Func<TEntity, bool>> predicate, IEnumerable<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            await EntityRepo.InsertOrUpdateAsync(predicate, entity);
        }
    }

    protected virtual void ThrowDeleteError(string? def, string? defRef1, string? defRef2)
    {
        throw new UserFriendlyException($"错误! {def}【{defRef1}】 被 {defRef2} 引用。  时间：{ DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
    }

    //
    // 摘要:
    //     抛出 RepetError 异常
    //
    // 参数:
    //   name:
    //     ndo的名称
    protected virtual void ThrowRepetError(string? name)
    {
        throw new UserFriendlyException($"错误! 数据名称【{name}】重复。 时间：{DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
    }

    //
    // 摘要:
    //     抛出 ThrowUserFriendlyError 异常
    protected virtual void ThrowUserFriendlyError(string? reason)
    {
        throw new UserFriendlyException($"错误! {reason}。 时间：{ DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
    }


    //
    // 摘要:
    //     获取服务实例
    //
    // 类型参数:
    //   T:
    protected T GetService<T>()
    {
        return ServiceProvider.GetRequiredService<T>();
    }
}
