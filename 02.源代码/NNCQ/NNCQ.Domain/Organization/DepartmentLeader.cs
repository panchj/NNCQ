using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Organization
{
    public class DepartmentLeader:IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [StringLength(50)]
        public string SortCode { get; set; }

        public virtual Department Department { get; set; }
        public virtual Person Person { get; set; }
        
        public DepartmentLeader() 
        {
            this.ID = Guid.NewGuid();
        }
    }
}
