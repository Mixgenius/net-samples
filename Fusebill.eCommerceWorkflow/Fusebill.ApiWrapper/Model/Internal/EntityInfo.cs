namespace Model.Internal
{
    public class EntityInfo
    {
        public EntityInfo(EntityType type, long id, EntityType? parentEntityType, long? parentId)
        {
            Type = type;
            Id = id;
            ParentType = parentEntityType;
            ParentId = parentId;
        }

        public EntityType Type { get; set; }
        public long Id { get; set; }
        public EntityType? ParentType { get; set; }
        public long? ParentId { get; set; }
    }
}
