using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edupocket.Domain.SeedWork
{
    public class BaseEntity<TId>: IEquatable<BaseEntity<TId>>
    {
        public TId Id { get; protected set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? CreatedByIp { get; set; }
        public string? ModifiedByIp { get; set; }

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
