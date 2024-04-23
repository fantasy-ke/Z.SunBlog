namespace Z.Foundation.Core.Entities
{

    /// <summary>
    /// 万物实体的接口
    /// </summary>
    public interface IEntity
    {
    }

    /// <summary>
    /// 万物实体主键的接口
    /// </summary>
    public interface IEntity<Tkey> : IEntity
    {
        Tkey Id { get; }
    }
}
