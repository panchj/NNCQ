using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Application
{
    public class ApplicationUserInApplicationRole:IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }         
        [StringLength(100)]
        public string Description { get; set; }
        [StringLength(100)]
        public string SortCode { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole RoleGroup { get; set; }

        public ApplicationUserInApplicationRole() 
        {
            this.ID = Guid.NewGuid();
        }
    }
}
