using NNCQ.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NNCQ.Web.ViewModels.Common
{
    public class BusinessImageVM
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(1000)]
        public string Name { get; set; }            
        [StringLength(1000)]
        public string Description { get; set; }     
        [StringLength(100)]
        public string SortCode { get; set; }        
        [StringLength(100)]
        public string DisplayName { get; set; }     
        [StringLength(256)]
        public string OriginalFileName { get; set; }
        public DateTime UploadedTime { get; set; }  
        [StringLength(256)]
        public string UploadPath { get; set; }      
        [StringLength(256)]
        public string UploadFileSuffix { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Guid RelevanceObjectID { get; set; }

        public BusinessImageVM() { }
        public BusinessImageVM(BusinessImage bo) 
        {
            this.ID                = bo.ID;
            this.Name              = bo.Name;
            this.Description       = bo.Description;
            this.SortCode          = bo.SortCode;
            this.DisplayName       = bo.DisplayName;
            this.OriginalFileName  = bo.OriginalFileName;
            this.UploadedTime      = bo.UploadedTime;
            this.UploadPath        = bo.UploadPath;
            this.UploadFileSuffix  = bo.UploadFileSuffix;
            this.X                 = bo.X;
            this.Y                 = bo.Y;
            this.Width             = bo.Width;
            this.Height            = bo.Height;
            this.RelevanceObjectID = bo.RelevanceObjectID;
        }

        public void MapToBo(BusinessImage bo) 
        {
            bo.Name              = this.Name;
            bo.Description       = this.Description;
            bo.SortCode          = this.SortCode;
            bo.DisplayName       = this.DisplayName;
            bo.OriginalFileName  = this.OriginalFileName;
            bo.UploadedTime      = this.UploadedTime;
            bo.UploadPath        = this.UploadPath;
            bo.UploadFileSuffix  = this.UploadFileSuffix;
            bo.X                 = this.X;
            bo.Y                 = this.Y;
            bo.Width             = this.Width;
            bo.Height            = this.Height;
            bo.RelevanceObjectID = this.RelevanceObjectID;

        }
    }
}