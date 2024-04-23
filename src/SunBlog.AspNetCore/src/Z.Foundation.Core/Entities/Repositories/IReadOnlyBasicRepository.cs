using System.Linq.Expressions;

namespace Z.Foundation.Core.Entities.Repositories;

public interface IReadOnlyBasicRepository<TEntity> : IRepository
    where TEntity : class, IEntity
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="includeDetails"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<long> GetCountAsync(CancellationToken cancellationToken = default);

    //
    // 摘要:
    //     Gets count of all entities in this repository based on given predicate.
    //
    // 参数:
    //   predicate:
    //     A method to filter count
    //
    // 返回结果:
    //     Count of entities
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);


    Task<(List<TEntity>, int)> GetPagedListAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);

}

public interface IReadOnlyBasicRepository<TEntity, TKey> : IReadOnlyBasicRepository<TEntity>
        where TEntity : class, IEntity<TKey>
{
    Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default);

    Task<TEntity> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default);
}
