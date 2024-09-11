namespace Edupocket.Domain.SeedWork
{
    public class BaseEntity<TId>: AuditableEntity, IEquatable<BaseEntity<TId>>
    {
        public TId Id { get; protected set; }
       

        protected BaseEntity(TId id) 
        {
            if (object.Equals(id, default(TId)))
                throw new ArgumentException("The ID cannot be type's default value.", "id");

            Id = id;
        }

        public bool Equals(BaseEntity<TId>? other)
        {
            if(other == null)
                return false;

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        // EF requires an empty constructor
        protected BaseEntity()
        {
        }
    }
}
