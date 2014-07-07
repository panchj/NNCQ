using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Application
{
    /// <summary>
    /// 应用类型定义说明
    /// </summary>
    public class ApplicationBusinessType:IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(50)]
        public string SortCode { get; set; }
        public bool IsDefault { get; set; }

        public ApplicationBusinessType() 
        {
            this.ID = Guid.NewGuid();
        }

    }
}
