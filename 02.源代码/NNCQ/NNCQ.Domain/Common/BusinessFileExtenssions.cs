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
