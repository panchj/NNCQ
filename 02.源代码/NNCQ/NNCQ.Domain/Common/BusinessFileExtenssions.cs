using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Common
{
    public static class BusinessFileExtenssions
    {
        /// <summary>
        /// 针对单一文件的存储处理方法
        /// </summary>
        /// <param name="BusinessFileRepository"></param>
        /// <param name="file"></param>
        public static void SaveSingleWithUniquenessRelevanceID(this IEntityRepository<BusinessFile> BusinessFileRepository, BusinessFile file) 
        {
            var pFile = BusinessFileRepository.GetSingleBy(x => x.RelevanceObjectID == file.RelevanceObjectID);
            // var fFileCollection = BusinessFileRepository.GetAll().Where(x => x.RelevanceObjectID == file.RelevanceObjectID);

            if (pFile == null)
            {
                BusinessFileRepository.AddAndSave(file);
            }
            else 
            {
                var filePath = file.AttachmentUploadPath + file.ID + "_" + file.Name + file.UploadFileSuffix;
                _DeleteFile(filePath);

                BusinessFileRepository.DeleteAndSave(pFile);
                BusinessFileRepository.AddAndSave(file);
                //pFile.Name                       = file.Name;
                //pFile.Description                = file.Description;
                //pFile.SortCode                   = file.SortCode;
                //pFile.AttachmentOriginalFileName = file.AttachmentOriginalFileName;
                //pFile.AttachmentTimeUploaded     = file.AttachmentTimeUploaded;
                //pFile.AttachmentUploadPath       = file.AttachmentUploadPath;
                //pFile.BinaryContent              = file.BinaryContent;
                //pFile.IsInDB                     = file.IsInDB;
                //pFile.FileSize                   = file.FileSize;
                //pFile.RelevanceObjectID          = file.RelevanceObjectID;
                //pFile.UploadFileSuffix           = file.UploadFileSuffix;

                //BusinessFileRepository.EditAndSave(pFile);
                //file.ID = pFile.ID;
            }
        }

        private static void _DeleteFile(string filePath) 
        {
            // 清除物理文件
            if (System.IO.File.Exists(filePath))
            {
                //System.IO.File.Delete(filePath);
            }
        }
    }
}
