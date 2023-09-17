using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Ddd.Common.Entities.Repositories;

public interface IBasicRepository<TEntity> : IReadOnlyBasicRepository<TEntity>
        where TEntity : class, IEntity
{
    /// <summary>
    /// Inserts a new entity.
    /// </summary>
    /// <param name="autoSave">
    /// Set true to automatically save changes to database.
    /// This is useful for ORMs / database APIs those only save changes with an explicit method call, but you need to immediately save changes to the database.
    /// </param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <param name="entity">Inserted entity</param>
    [NotNull]
    Task<TEntity> InsertAsync([NotNull] TEntity entity,  CancellationToken cancellationToken = default);

    /// <summary>
    /// Inserts multiple new entities.
    /// </summary>
    /// <param name="autoSave">
    /// Set true to automatically save changes to database.
    /// This is useful for ORMs / database APIs those only save changes with an explicit method call, but you need to immediately save changes to the database.
    /// </param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <param name="entities">Entities to be inserted.</param>
    /// <returns>Awaitable <see cref="Task"/>.</returns>
    Task InsertManyAsync([NotNull] IEnumerable<TEntity> entities,  CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="autoSave">
    /// Set true to automatically save changes to database.
    /// This is useful for ORMs / database APIs those only save changes with an explicit method call, but you need to immediately save changes to the database.
    /// </param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <param name="entity">Entity</param>
    [NotNull]
    Task<TEntity> UpdateAsync([NotNull] TEntity entity);

    /// <summary>
    /// Updates multiple entities.
    /// </summary>
    /// <param name="entities">Entities to be updated.</param>
    /// <param name="autoSave">
    /// Set true to automatically save changes to database.
    /// This is useful for ORMs / database APIs those only save changes with an explicit method call, but you need to immediately save changes to the database.</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Awaitable <see cref="Task"/>.</returns>
    Task UpdateManyAsync([NotNull] IEnumerable<TEntity> entities);

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="entity">Entity to be deleted</param>
    /// <param name="autoSave">
    /// Set true to automatically save changes to database.
    /// This is useful for ORMs / database APIs those only save changes with an explicit method call, but you need to immediately save changes to the database.
    /// </param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    Task DeleteAsync([NotNull] TEntity entity,  CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple entities.
    /// </summary>
    /// <param name="entities">Entities to be deleted.</param>
    /// <param name="autoSave">
    /// Set true to automatically save changes to database.
    /// This is useful for ORMs / database APIs those only save changes with an explicit method call, but you need to immediately save changes to the database.
    /// </param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Awaitable <see cref="Task"/>.</returns>
    Task DeleteManyAsync([NotNull] IEnumerable<TEntity> entities,  CancellationToken cancellationToken = default);
}

public interface IBasicRepository<TEntity, TKey> : IBasicRepository<TEntity>, IReadOnlyBasicRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteManyAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);
}