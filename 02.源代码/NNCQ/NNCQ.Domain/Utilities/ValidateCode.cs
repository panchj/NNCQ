using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNNCQ.Domain.Utilities
{
    /// <summary>
    /// 验证码：用于存放系统生成的验证码
    /// </summary>
   public class ValidateCode : IEntity
    {
       [Key]
       public Guid ID { get; set; }
       [StringLength(100)]
       public string Name { get; set; }
       [StringLength(50)]
       public string SortCode { get; set; }
       [StringLength(50)]
       public string Description { get; set; }

       public ValidateCode()
       {
           this.ID = Guid.NewGuid(); 
       }

       public void SetSortCode() 
       {
           this.SortCode = "";// 这里创建生成验证码的代码
       }
    }
}
