using Z.Foundation.Core.Entities;
using Z.Module.DependencyInjection;

namespace Z.Fantasy.Core.DomainServiceRegister.Domain;

public interface IBusinessDomainService<TEntity> : IBasicDomainService<TEntity, Guid>, IDomainService, ITransientDependency where TEntity : class, IEntity<Guid>
{
    Guid NewGuid();
}
