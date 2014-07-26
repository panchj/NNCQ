using NNCQ.Domain.Common;
using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NNCQ.Web.Controllers.Common
{
    /// <summary>
    /// 用于处理公共上传文件的控制器
    /// </summary>
    public class CommonUploadController : Controller
    {
        private readonly IEntityRepository<BusinessFile> _FileService;
        private readonly IEntityRepository<BusinessImage> _ImageService;

        public CommonUploadController(IEntityRepository<BusinessFile> fService, IEntityRepository<BusinessImage> iService)
        {
            this._FileService = fService;
            this._ImageService = iService;

        }

        /// <summary>
        /// 示范的前端方法
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("../../Views/Common/Upload/Index");
        }

        #region 提取上传组件的前端显示页面数据
        /// <summary>
        /// 单一文件上传
        /// </summary>
        /// <param name="objectID">关联业务对象的ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SingleFile(Guid objectID)
        {
            ViewBag.ObjectID = objectID;
            return PartialView("../../Views/Common/Upload/_BusinessSingleFileUpload");
        }

        /// <summary>
        /// 多文件上传
        /// </summary>
        /// <param name="objectID">关联业务对象的ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MultiFile(Guid objectID)
        {
            ViewBag.ObjectID = objectID;
            return PartialView("../../Views/Common/Upload/_BusinessMultiFileUpload");
        }

        /// <summary>
        /// 单一图片上传
        /// </summary>
        /// <param name="objectID">关联业务对象的ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SingleImage(Guid objectID)
        {
            ViewBag.ObjectID = objectID;
            return PartialView("../../Views/Common/Upload/_BusinessSingleImageUpload");
        }

        /// <summary>
        /// 多图片上传
        /// </summary>
        /// <param name="objectID">关联业务对象的ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MultiImage(Guid objectID)
        {
            ViewBag.ObjectID = objectID;
            return PartialView("../../Views/Common/Upload/_BusinessMultiImageUpload");
        } 
        #endregion

        #region 提取文件上传之后的持久化数据，供前端呈现使用
        [HttpPost]
        public ActionResult GetSingleFileStatus(Guid objectID)
        {
            var file = _FileService.GetSingleBy(x => x.RelevanceObjectID == objectID);
            var status = new UploadedStatus();
            if (file == null)
            {
                status.IsSucceded = false;
                status.InnerHtml = "<span style='color:red'>上传数据错误或者数据保存错误，请联系有关技术人员。</span>";
            }
            else
            {
                var tempHtmlString = new StringBuilder();
                tempHtmlString.Append("<table class='table' style='width:100%'>");
                tempHtmlString.Append("<tr>");
                tempHtmlString.Append("<td style='width:20px'>" + _GetFileTypeString(file.UploadFileSuffix));
                tempHtmlString.Append("</td>");
                tempHtmlString.Append("<td>" + file.Name + file.UploadFileSuffix);
                tempHtmlString.Append("</td>");
                tempHtmlString.Append("<td style='width:40px'>");
                tempHtmlString.Append("<a href='javascript:deleteUploadFile(\""+file.ID+"\")'><i class='icon-cancel-2 fg-red'></i></a>");
                tempHtmlString.Append("</td>");
                tempHtmlString.Append("</table>");
                status.IsSucceded = true;
                status.InnerHtml = tempHtmlString.ToString();

            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult GetMutilFileStatus(Guid objectID)
        {
            return Json("");
        }

        [HttpPost]
        public ActionResult GetSingleImageStatus(Guid objectID)
        {
            return Json("");
        }

        [HttpPost]
        public ActionResult GetMutilImageStatus(Guid objectID)
        {
            return Json("");
        } 
        #endregion

        #region 删除已经上传的文件
        [HttpPost]
        public ActionResult DeleteSingleFile(Guid fileID)
        {
            var file = _FileService.GetSingle(fileID);
            var filePath = file.AttachmentUploadPath + file.Name + file.ID+"_"+ file.UploadFileSuffix;
            // 清除数据库记录
            _FileService.DeleteAndSave(file);
            // 清除物理文件
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            return Json("删除成功。");
        } 
        #endregion

        // 根据文件的后缀，生成显示用的象形符号
        private string _GetFileTypeString(string tpyeNameString) 
        {
            var typeString = "";
            switch (tpyeNameString)
            {
                case ".doc":
                    typeString = "<i class='icon-file-word'></i>";
                    break;
                case ".docx":
                    typeString = "<i class='icon-file-word'></i>";
                    break;
                case ".ppt":
                    typeString = "<i class='icon-file-powerpoint'></i>";
                    break;
                case ".pptx":
                    typeString = "<i class='icon-file-powerpoint'></i>";
                    break;
                case ".xls":
                    typeString = "<i class='icon-file-excel'></i>";
                    break;
                case ".xlsx":
                    typeString = "<i class='icon-file-excel'></i>";
                    break;
                case ".pdf":
                    typeString = "<i class='icon-file-pdf'></i>";
                    break;
                case ".mp4":
                    typeString = "<i class='icon-icon-film'></i>";
                    break;
                default:
                    typeString = "<i class='icon-attachment'></i>";
                    break;
            }
            return typeString;

        }

    }

    public class UploadedStatus 
    {
        public bool IsSucceded { get; set; }
        public string InnerHtml { get; set; }
    }
}