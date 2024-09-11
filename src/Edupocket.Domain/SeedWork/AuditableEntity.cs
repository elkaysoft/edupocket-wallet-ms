using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edupocket.Domain.SeedWork
{
    public class AuditableEntity
    {
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? CreatedByIp { get; set; }
        public string? ModifiedByIp { get; set; }
    }
}
