namespace Z.Foundation.Core.Entities.IAuditing;

public interface IHasDeletionTime : ISoftDelete
{
    DateTime? DeletionTime { get; }
}
