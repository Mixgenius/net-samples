namespace Model
{
    public abstract class NullableEntity : Entity
    {
        public new long? Id { get; set; }
    }
}
