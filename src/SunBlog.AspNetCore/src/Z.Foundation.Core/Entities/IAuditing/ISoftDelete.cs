namespace Z.Foundation.Core.Entities.IAuditing;

public interface ISoftDelete
{
    /// <summary>
    /// 软删除状态
    /// </summary>
    bool? IsDeleted { get; }
}
