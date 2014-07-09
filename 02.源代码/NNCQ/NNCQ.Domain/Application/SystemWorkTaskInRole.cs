using Microsoft.AspNet.Identity.EntityFramework;
using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Application
{
    public class SystemWorkTaskInRole : IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }                    
        [StringLength(500)]
        public string Description { get; set; }             
        [StringLength(100)]
        public string SortCode { get; set; }                

        public virtual SystemWorkTask SystemWorkTask { get; set; }
        public virtual ApplicationRole SystemRole { get; set; }

        public SystemWorkTaskInRole() 
        {
            this.ID = Guid.NewGuid();
            
        }

    }
}
