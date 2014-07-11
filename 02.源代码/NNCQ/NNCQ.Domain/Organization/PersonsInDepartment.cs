using LNNCQ.Domain.Utilities;
using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Organization
{
    public class PersonsInDepartment:IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(10)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [StringLength(50)]
        public string SortCode { get; set; } // 在实际处理的时候，这项数据与Person的SortCode一致。

        public virtual Department Department { get; set; }
        public virtual Person Person { get; set; }

        public PersonsInDepartment() 
        {
            this.ID = Guid.NewGuid();
            this.SortCode = BusinessEntityComponentsFactory.SortCodeByDefaultDateTime<PersonsInDepartment>();
        }

    }
}
