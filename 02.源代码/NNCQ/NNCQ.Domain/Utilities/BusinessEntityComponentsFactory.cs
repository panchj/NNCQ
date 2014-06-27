using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LNNCQ.Domain.Utilities
{
    public class BusinessEntityComponentsFactory
    {
        /// <summary>
        /// 提取根据系统时间生成 SortCode 所需要的字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string SortCodeByDefaultDateTime<T>()
        {
            var result = "Default";
            var timeStampString = "";

            var nowTime = DateTime.Now;
            timeStampString = nowTime.ToString("yyyy-MM-dd-hh-mm-ss-ffff", DateTimeFormatInfo.InvariantInfo);

            var entityName = typeof(T).Name;
            result = entityName + "_" + timeStampString;
            return result;
        }

        public static void SetDeleteStatus<T>(
            Guid objectID,
            DeleteStatus deleteStatus,
            List<object> relevanceOperations)
        {

            var _DbContext = new EntityDbContext();

            var dbSet = _DbContext.Set(typeof(T));
            var returnStatus = true;
            var returnMessage = "";
            var bo = dbSet.Find(objectID);

            if (bo == null)
            {
                returnStatus = false;
                returnMessage = "<li>你所删除的数据不存在，如果确定不是数据逻辑错误原因，请将本情况报告系统管理人员。</li>";
                deleteStatus.Initialize(returnStatus, returnMessage);
            }
            else
            {
                #region 处理关联关系
                foreach (var deleteOperationObject in relevanceOperations)
                {
                    var deleteProperty = deleteOperationObject.GetType().GetProperties().Where(pn => pn.Name == "CanDelete").FirstOrDefault();
                    var itCanDelete = (bool)deleteProperty.GetValue(deleteOperationObject);

                    var messageProperty = deleteOperationObject.GetType().GetProperties().Where(pn => pn.Name == "OperationMessage").FirstOrDefault();
                    var messageValue = messageProperty.GetValue(deleteOperationObject) as string;

                    if (!itCanDelete)
                    {
                        returnStatus = false;
                        returnMessage += "<li>" + messageValue + "</li>";
                    }
                }
                #endregion

                if (returnStatus)
                {
                    try
                    {
                        dbSet.Remove(bo);
                        _DbContext.SaveChanges();
                        deleteStatus.Initialize(returnStatus, returnMessage);
                    }
                    catch (System.Data.Entity.Core.EntityException)
                    {
                        returnStatus = false;
                        returnMessage = "<li>无法删除所选数据，其信息正被使用，如果确定不是数据逻辑错误原因，请将本情况报告系统管理人员。</li>";
                        deleteStatus.Initialize(returnStatus, returnMessage);
                    }
                }
                else
                    deleteStatus.Initialize(returnStatus, returnMessage);
            }
        }

        public static string GetRandomCode(int num)
        {
            string[] source = { "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string code = "";
            int p = int.Parse(DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString());
            Thread.Sleep(1);
            Random rd = new Random(p);
            for (int i = 0; i < num; i++)
            {
                code += source[rd.Next(0, source.Length)];
            }
            return code;
        }
        public static string  GetValidateCode(int number)
        {
         //number :验证码的位数
         //amount :生成的验证码个数
            int amount = 200;
            int a;
            List<ValidateCode> valiCodes = new List<ValidateCode>();
            string[] codeArray = new string[amount];
            Random rd = new Random();
            var i = 0;
            do
            {
                string[] source = { "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                string code = "";

                for (var j = 0; j < number; j++)
                {
                    code += source[rd.Next(0, source.Length)];
                }

                a = 0;
                foreach (var valicode in valiCodes)
                {
                    if (valicode.Name == code)
                    {
                        a = 1;
                        break;
                    }
                }
                if (a != 1)
                {
                    var valicode = new ValidateCode();
                    valicode.Name = code;
                    codeArray[i] = code;
                    valiCodes.Add(valicode);
                    i++;
                }
            } while (i != amount);
            System.Threading.Thread.Sleep(10);
            string singleCode = codeArray[rd.Next(0, codeArray.Length)];
            return singleCode;
        }


    }
}
