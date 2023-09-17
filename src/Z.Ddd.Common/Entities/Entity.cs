using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Ddd.Common.Entities
{
    /// <summary>
    /// 自定义实体无主键实体
    /// </summary>
    [Serializable]
    public abstract class Entity : IEntity
    {

        protected Entity(Guid id)
        {
            Id = id;
        }


        public Guid Id { get; }

        protected Entity()
        {
        }
        public override string ToString()
        {
            return $"[Entity: {GetType().Name}] Keys = {string.Join(",", GetKeys())}";
        }

        public abstract object[] GetKeys();
    }

    [Serializable]
    public abstract class Entity<Tkey> 
        : IEntity<Tkey>
    {
        protected Entity()
        {
        }
        /// <summary>
        /// ID
        /// </summary>
        public Tkey Id { get; protected set; }

        protected Entity(Tkey id) => Id = id;

        public override string ToString()
        {
            return $"[Entity:{GetType().Name}] Id = {Id}";
        }
    }
}
