using LNNCQ.Domain.Utilities;
using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Application
{
    public class ApplicaitionUserInApplication:IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(10)]
        public string Description { get; set; }            
        [StringLength(50)]
        public string SortCode { get; set; }

        public virtual ApplicationInformation AppInfo { get; set; }
        public virtual ApplicationUser User { get; set; }

        public ApplicaitionUserInApplication() 
        {
            this.ID = Guid.NewGuid();
            this.SortCode = BusinessEntityComponentsFactory.SortCodeByDefaultDateTime<ApplicationInformation>();

        }
    }
}
