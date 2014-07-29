using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Common
{
    public static class BusinessImageExtenssions
    {
        public static void SaveSingleWithUniquenessRelevanceID(this IEntityRepository<BusinessImage> BusinessImageRepository, BusinessImage file)
        {
            var pFile = BusinessImageRepository.GetSingleBy(x => x.RelevanceObjectID == file.RelevanceObjectID);

            if (pFile == null)
            {
                BusinessImageRepository.AddAndSave(file);
            }
            else
            {
                var filePath = file.UploadPath + file.ID + "_" + file.Name + file.UploadFileSuffix;
                _DeleteFile(filePath);

                pFile.Name = file.Name;
                pFile.Description = file.Description;
                pFile.SortCode = file.SortCode;
                pFile.OriginalFileName = file.OriginalFileName;
                pFile.UploadedTime = file.UploadedTime;
                pFile.UploadPath = file.UploadPath;
                pFile.RelevanceObjectID = file.RelevanceObjectID;
                pFile.UploadFileSuffix = file.UploadFileSuffix;

                BusinessImageRepository.EditAndSave(pFile);
                file.ID = pFile.ID;
            }
        }

        private static void _DeleteFile(string filePath)
        {
            // 清除物理文件
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
