namespace Z.Foundation.Core.Entities.IAuditing
{
    public interface IMayHaveCreator
    {
        /// <summary>
        /// Id of the creator.
        /// </summary>
        string CreatorId { get; }
    }
}
