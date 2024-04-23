namespace Z.Foundation.Core.Entities.IAuditing
{
    public interface IDeletionAuditedObject : IHasDeletionTime
    {
        string DeleterId { get; }
    }
}
