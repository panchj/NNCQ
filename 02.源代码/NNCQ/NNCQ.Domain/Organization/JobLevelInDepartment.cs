using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Organization
{
    public class JobLevelInDepartment:IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(10)]
        public string Name { get; set; }
        [StringLength(10)]
        public string Description { get; set; }
        [StringLength(10)]
        public string SortCode { get; set; }

        public virtual JobLevel JobLevel { get; set; }
        public virtual Department Department { get; set; }

        public JobLevelInDepartment() 
        {
            this.ID = Guid.NewGuid();
        }
    }
}
