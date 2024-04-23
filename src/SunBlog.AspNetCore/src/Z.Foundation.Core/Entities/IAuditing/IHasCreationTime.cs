namespace Z.Foundation.Core.Entities.IAuditing
{
    public interface IHasCreationTime
    {
        /// <summary>
        /// Creation time.
        /// </summary>
        DateTime? CreationTime { get; }
    }
}
