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
    public class BusinessImage : IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(1000)]
        public string Name { get; set; }             // 图片显示名称
        [StringLength(1000)]
        public string Description { get; set; }      // 图片说明
        [StringLength(100)]
        public string SortCode { get; set; }         // 内部业务编码

        public string DisplayName { get; set; }      // 图片显示名称
        [StringLength(256)]
        public string OriginalFileName { get; set; } // 图片原始文件
        public DateTime UploadedTime { get; set; }   // 图片上传时间
        [StringLength(256)]
        public string UploadPath { get; set; }       // 图片上传保存路径
        [StringLength(256)]
        public string UploadFileSuffix { get; set; } // 上传文件的后缀名

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Guid RelevanceObjectID { get; set; }  // 使用该图片的业务对象的 id

        public BusinessImage()
        {
            this.ID = Guid.NewGuid();
            this.SortCode = BusinessEntityComponentsFactory.SortCodeByDefaultDateTime<BusinessImage>();
            this.UploadedTime = DateTime.Now;
        }
    }
}
