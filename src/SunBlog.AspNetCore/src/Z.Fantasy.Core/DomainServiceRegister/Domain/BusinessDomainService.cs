using Z.Foundation.Core.Entities;
using Z.Module.DependencyInjection;

namespace Z.Fantasy.Core.DomainServiceRegister.Domain;

public abstract class BusinessDomainService<TEntity>(IServiceProvider serviceProvider)
    : BasicDomainService<TEntity, Guid>(serviceProvider),
        IBusinessDomainService<TEntity>
    where TEntity : class, IEntity<Guid>
{
    public Guid NewGuid()
    {
        return Guid.NewGuid();
    }
}

public abstract class BusinessIntDomainService<TEntity>(IServiceProvider serviceProvider)
    : BasicDomainService<TEntity, int>(serviceProvider),
        IBusinessIntDomainService<TEntity>
    where TEntity : class, IEntity<int>
{
    public Guid NewGuid()
    {
        return Guid.NewGuid();
    }
}

