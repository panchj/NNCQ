using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Organization
{
    /// <summary>
    /// 个人身份证件类型
    /// </summary>
    public class CredentialsType : IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [StringLength(50)]
        public string SortCode { get; set; }

        public CredentialsType() 
        {
            this.ID = Guid.NewGuid();
        }
    }
}
