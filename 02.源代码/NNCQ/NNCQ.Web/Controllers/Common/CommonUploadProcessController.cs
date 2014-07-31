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
        public async Task<HttpResponseMessage> CommonUploadProcess(bool isSingle, string fileType, string relID, string fileName)
        {
            var defaultUploadFilesUrl = System.Web.HttpContext.Current.Server.MapPath(HttpContext.Current.Request["folder"] + "\\");
            var imgaeRecord = new BusinessImage();
            var fileRecord = new BusinessFile();

            var mainID = Guid.NewGuid();

            switch (fileType)
            {
                case "commonFile":
                    mainID = fileRecord.ID;
                    defaultUploadFilesUrl += System.Configuration.ConfigurationManager.AppSettings["DefaultCommonFileUploadUrl"];
                    break;
                case "imageFile":
                    mainID = imgaeRecord.ID;
                    defaultUploadFilesUrl += System.Configuration.ConfigurationManager.AppSettings["DefaultCommonImageUploadUrl"];
                    break;
                default:
                    mainID = fileRecord.ID;
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
            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(defaultUploadFilesUrl, mainID + "_"); //mainID + "_"

            var initialFileName = fileName.ToLower();
            var fileSize = (long)Request.Content.Headers.ContentLength;
            var tempFileName = provider.TempFileName;
            if (tempFileName != null)
                initialFileName = tempFileName;

            List<string> files = new List<string>();

            if (initialFileName.ToLower() == fileName.ToLower())
            {
                try
                {
                    //await _ProcessUploadFile(provider, files, fileType, imgaeRecord, fileRecord, defaultUploadFilesUrl, mainID, relID, fileSize);
                    await Request.Content.ReadAsMultipartAsync(provider);
                    foreach (MultipartFileData file in provider.FileData)
                    {
                        files.Add(Path.GetFileName(file.LocalFileName));

                        #region 持久化关系处理

                        var localFileName = Path.GetFileName(file.LocalFileName).ToLower();
                        var saveFileSuffix = Path.GetExtension(file.LocalFileName).ToLower();
                        var saveFileName = localFileName.Substring((mainID + "_").Length, localFileName.Length - (mainID + "_").Length - saveFileSuffix.Length);
                        switch (fileType)
                        {
                            case "imageFile":
                                imgaeRecord.Name = saveFileName;
                                imgaeRecord.UploadPath = defaultUploadFilesUrl;
                                imgaeRecord.Description = "图形文件";
                                imgaeRecord.OriginalFileName = localFileName;
                                imgaeRecord.UploadFileSuffix = saveFileSuffix;
                                imgaeRecord.UploadedTime = DateTime.Now;
                                imgaeRecord.FileSize = (long)fileSize;
                                imgaeRecord.RelevanceObjectID = Guid.Parse(relID);
                                _ImageService.SaveSingleWithUniquenessRelevanceID(imgaeRecord);
                                break;
                            default:
                                fileRecord.Name = saveFileName;
                                fileRecord.UploadPath = defaultUploadFilesUrl;
                                fileRecord.Description = "普通文件";
                                fileRecord.OriginalFileName = localFileName;
                                fileRecord.UploadFileSuffix = saveFileSuffix;
                                fileRecord.AttachmentTimeUploaded = DateTime.Now;
                                fileRecord.FileSize = (long)fileSize;
                                fileRecord.RelevanceObjectID = Guid.Parse(relID);
                                _FileService.SaveSingleWithUniquenessRelevanceID(fileRecord);
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
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, files);
            }
        }

        // 下面的方法暂时不需要处理
        private Task<string> _ProcessUploadFile(
            CustomMultipartFormDataStreamProvider provider,
            List<string> files,
            string fileType, 
            BusinessImage imgaeRecord,
            BusinessFile fileRecord,
            string defaultUploadFilesUrl,
            Guid mainID,
            string relID,
            long fileSize
            ) 
        {

            Request.Content.ReadAsMultipartAsync(provider);
            foreach (MultipartFileData file in provider.FileData)
            {
                files.Add(Path.GetFileName(file.LocalFileName));

                #region 持久化关系处理

                var localFileName = Path.GetFileName(file.LocalFileName).ToLower();
                var saveFileSuffix = Path.GetExtension(file.LocalFileName).ToLower();
                var saveFileName = localFileName.Substring((mainID + "_").Length, localFileName.Length - (mainID + "_").Length - saveFileSuffix.Length);
                switch (fileType)
                {
                    case "imageFile":
                        imgaeRecord.Name = saveFileName;
                        imgaeRecord.UploadPath = defaultUploadFilesUrl;
                        imgaeRecord.Description = "图形文件";
                        imgaeRecord.OriginalFileName = localFileName;
                        imgaeRecord.UploadFileSuffix = saveFileSuffix;
                        imgaeRecord.UploadedTime = DateTime.Now;
                        imgaeRecord.FileSize = (long)fileSize;
                        imgaeRecord.RelevanceObjectID = Guid.Parse(relID);
                        _ImageService.SaveSingleWithUniquenessRelevanceID(imgaeRecord);
                        break;
                    default:
                        fileRecord.Name = saveFileName;
                        fileRecord.UploadPath = defaultUploadFilesUrl;
                        fileRecord.Description = "普通文件";
                        fileRecord.OriginalFileName = localFileName;
                        fileRecord.UploadFileSuffix = saveFileSuffix;
                        fileRecord.AttachmentTimeUploaded = DateTime.Now;
                        fileRecord.FileSize = (long)fileSize;
                        fileRecord.RelevanceObjectID = Guid.Parse(relID);
                        _FileService.SaveSingleWithUniquenessRelevanceID(fileRecord);
                        break;
                }
                #endregion
            }
            return null;
        }
    }


    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider 
    {
        private string _AppendString = "";
        public string TempFileName { get; set; }
        public CustomMultipartFormDataStreamProvider(string path, string appendString) : base(path) { _AppendString = appendString; } 
        public override string GetLocalFileName(HttpContentHeaders headers) 
        {
            var fileName = headers.ContentDisposition.FileName.Replace("\"", String.Empty);
            TempFileName = fileName;
            return _AppendString + fileName; ;
        }
    }

}