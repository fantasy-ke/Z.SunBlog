using Z.Module.DependencyInjection;

namespace Z.Foundation.Core.Entities.IAuditing
{
    public interface IAuditPropertySetter : ITransientDependency
    {
        void SetCreationProperties(object targetObject);


        void SetDeletionProperties(object targetObject);

    }
}
