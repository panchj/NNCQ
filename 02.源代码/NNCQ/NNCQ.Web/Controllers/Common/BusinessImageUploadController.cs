using NNCQ.Domain.Common;
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
    public class BusinessImageUploadController : ApiController
    {
        [HttpGet]
        [HttpPost]
        public async Task<HttpResponseMessage> BusinessImageUpload(string relID)
        {
            var defaultUploadFilesUrl = System.Web.HttpContext.Current.Server.MapPath(HttpContext.Current.Request["folder"] + "\\");
            defaultUploadFilesUrl += System.Configuration.ConfigurationManager.AppSettings["DefaultCommonImageUploadUrl"];
            if (!Directory.Exists(defaultUploadFilesUrl))
            {
                Directory.CreateDirectory(defaultUploadFilesUrl);
            }

            if (!Request.Content.IsMimeMultipartContent()) 
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType); 
            } 
            
            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(defaultUploadFilesUrl); 
            List<string> files = new List<string>(); 
            try
            { 
                await Request.Content.ReadAsMultipartAsync(provider); 
                foreach (MultipartFileData file in provider.FileData) 
                {
                    files.Add(Path.GetFileName(file.LocalFileName));
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
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { } 
        public override string GetLocalFileName(HttpContentHeaders headers) 
        {
            return headers.ContentDisposition.FileName.Replace("\"", string.Empty); 
        }
    }
}