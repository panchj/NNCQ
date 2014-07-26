using NNCQ.Domain.Common;
using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace NNCQ.Web.Controllers.Common
{
    /// <summary>
    /// 上传文件存储处理
    /// </summary>
    public class CommonUploadProcessController : ApiController
    {
        private readonly IEntityRepository<BusinessFile> _FileService;  
        private readonly IEntityRepository<BusinessImage> _ImageService;

        public CommonUploadProcessController(IEntityRepository<BusinessFile> fService, IEntityRepository<BusinessImage> iService)
        {
            this._FileService = fService;
            this._ImageService = iService;
        }

        [HttpGet]
        [HttpPost]
        public async Task<HttpResponseMessage> CommonUploadProcess(bool isSingle, string fileType, string relID)
        {
            var defaultUploadFilesUrl = System.Web.HttpContext.Current.Server.MapPath(HttpContext.Current.Request["folder"] + "\\");
            var imgaeRecord = new BusinessImage();
            var fileRecord = new BusinessFile();

            var rID = Guid.NewGuid();

            switch (fileType)
            {
                case "commonFile": 
                    rID = fileRecord.ID;
                    defaultUploadFilesUrl += System.Configuration.ConfigurationManager.AppSettings["DefaultCommonFileUploadUrl"];
                    break;
                case "imageFile":
                    rID = imgaeRecord.ID;
                    defaultUploadFilesUrl += System.Configuration.ConfigurationManager.AppSettings["DefaultCommonImageUploadUrl"];
                    break;
                default:
                    rID = fileRecord.ID;
                    defaultUploadFilesUrl += System.Configuration.ConfigurationManager.AppSettings["DefaultCommonUploadUrl"];
                    break;
            }

            if (!Directory.Exists(defaultUploadFilesUrl))
            {
                Directory.CreateDirectory(defaultUploadFilesUrl);
            }

            if (!Request.Content.IsMimeMultipartContent()) 
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType); 
            }

            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(defaultUploadFilesUrl, rID+"_"); 
            
            List<string> files = new List<string>(); 
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (MultipartFileData file in provider.FileData) 
                {
                    files.Add(Path.GetFileName(file.LocalFileName));

                    #region 持久化关系处理


                    var localFileName = Path.GetFileName(file.LocalFileName).ToLower();
                    var saveFileSuffix = Path.GetExtension(file.LocalFileName).ToLower();
                    var saveFileName = localFileName.Substring((rID + "_").Length, localFileName.Length - (rID + "_").Length - saveFileSuffix.Length);

                    switch (fileType)
                    {
                        case "imageFile":
                            // 对于单一文件的处理，先清理当前数据库的数据和物理磁盘文件
                            if (isSingle)
                            {
                                var iFiles = _ImageService.GetAll().Where(x => x.RelevanceObjectID == Guid.Parse(relID));
                                foreach (var iItem in iFiles)
                                {
                                    _ImageService.Delete(iItem);
                                    var iFilePath = iItem.UploadPath + iItem.Name + iItem.UploadFileSuffix;
                                    if (File.Exists(iFilePath))
                                    {
                                        File.Delete(iFilePath);
                                    }
                                }
                                _ImageService.Save();
                            }
                            imgaeRecord.Name = saveFileName;
                            imgaeRecord.UploadPath = defaultUploadFilesUrl;
                            imgaeRecord.Description = "图形文件。";
                            imgaeRecord.OriginalFileName = localFileName;
                            imgaeRecord.UploadFileSuffix = saveFileSuffix;
                            imgaeRecord.UploadedTime = DateTime.Now;
                            imgaeRecord.RelevanceObjectID = Guid.Parse(relID);
                            _ImageService.AddOrEditAndSave(imgaeRecord);
                            break;
                        default:
                            // 对于单一文件的处理，先清理当前数据库的数据和物理磁盘文件
                            //if (isSingle)
                            //{
                            //    var iFiles = _FileService.GetAll().Where(x => x.RelevanceObjectID == Guid.Parse(relID));
                            //    foreach (var iItem in iFiles)
                            //    {
                            //        _FileService.DeleteAndSave(iItem);
                            //        var iFilePath = iItem.AttachmentUploadPath + iItem.Name + iItem.UploadFileSuffix;
                            //        if (File.Exists(iFilePath))
                            //        {
                            //            File.Delete(iFilePath);
                            //        }
                            //    }
                            //}

                            fileRecord.Name = saveFileName;
                            fileRecord.AttachmentUploadPath = defaultUploadFilesUrl;
                            fileRecord.Description = "普通文件。";
                            fileRecord.AttachmentOriginalFileName = localFileName;
                            fileRecord.UploadFileSuffix = saveFileSuffix;
                            fileRecord.AttachmentTimeUploaded = DateTime.Now;
                            fileRecord.RelevanceObjectID = Guid.Parse(relID);
                            _FileService.AddOrEditAndSave(fileRecord);

                            break;
                    } 
                    #endregion
                }
                return Request.CreateResponse(HttpStatusCode.OK, files); 
            }
            catch (System.Exception e) 
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

        }
    }

    [DataContract]
    public class FileDesc 
    {
        
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string path { get; set; }
        [DataMember]
        public long size { get; set; }
        public FileDesc(string n, string p, long s)
        {
            name = n;
            path = p;
            size = s;
        }
    }


    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider 
    {
        private string _AppendString = "";
        public CustomMultipartFormDataStreamProvider(string path, string appendString) : base(path) { _AppendString = appendString; } 
        public override string GetLocalFileName(HttpContentHeaders headers) 
        {
            var fileName = headers.ContentDisposition.FileName.Replace("\"", String.Empty);
            return _AppendString + fileName; ;
        }
    }
}