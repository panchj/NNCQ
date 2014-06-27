using LNNCQ.Domain.Utilities;
using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Common
{
    public class BusinessFile:IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(50)]
        public string Name { get; set; } // 附件的显示名称
        [StringLength(1000)]
        public string Description { get; set; } // 
        [StringLength(50)]
        public string SortCode { get; set; }

        public DateTime AttachmentTimeUploaded { get; set; }           // 附件上传时间
        [StringLength(500)]
        public string AttachmentOriginalFileName { get; set; }         // 附件原始文件名称
        [StringLength(500)]
        public string AttachmentDescription { get; set; }              // 附件说明
        [StringLength(500)]
        public string AttachmentUploadPath { get; set; }               // 附件上传保存路径
        public bool IsInDB { get; set; }                               // 附件存放格式，如果使用二进制方式存在数据库中，则使用下一个属性进行处理
        public byte[] BinaryContent { get; set; }                      // 附件存放的二进制内容
        public Guid RelevanceObjectID { get; set; }                    // 关联对象ID

        public BusinessFile() 
        {
            this.ID = Guid.NewGuid();
            this.SortCode = BusinessEntityComponentsFactory.SortCodeByDefaultDateTime<BusinessImage>();
            this.AttachmentTimeUploaded = DateTime.Now;
        }
    }
}
