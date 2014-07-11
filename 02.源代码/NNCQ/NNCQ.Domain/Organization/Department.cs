using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Organization
{
    public class Department : IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [StringLength(50)]
        public string SortCode { get; set; }
        public bool IsActiveDepartment { get; set; }

        public virtual Department ParentDapartment { get; set; }

        [NotMapped]
        public virtual ICollection<DepartmentType> Types { get; set; }
        [NotMapped]
        public virtual ICollection<Person> Leaders { get; set; }
        [NotMapped]
        public virtual ICollection<Person> Perosns { get; set; }
        [NotMapped]
        public virtual ICollection<JobLevel> JobLevels { get; set; }
        [NotMapped]
        public virtual ICollection<JobTitle> JobTitles { get; set; }

        public Department() 
        {
            this.ID = Guid.NewGuid();
            this.IsActiveDepartment = true;
        }

    }
}
