using System;

namespace Model
{
    public abstract class Entity : IEntity, IEquatable<Entity>
    {
        public long Id { get; set; }

        public bool Equals(Entity other)
        {
            if (null == other)
            {
                return false;
            }

            return Id == other.Id;
        }
    }
}
