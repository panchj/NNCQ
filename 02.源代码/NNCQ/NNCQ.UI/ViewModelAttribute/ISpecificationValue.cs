using NNCQ.UI.Models.Web.MetroUICSS.SubControlModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.ViewModelAttribute
{
    /// <summary>
    /// 用于反射业务对象的视图、校验以及其它相关处理所需要的规格数据的接口
    /// </summary>
    public interface ISpecificationValue<T> where T : class, new()
    {
        /// <summary>
        /// 通过反射的方式，提取 boVM 类定义中的 ListSpecification 特性的值，并为响应的列表的表头部分
        /// </summary>
        /// <param name="boVM"></param>
        /// <returns></returns>
        Muc_MainWorkPlaceHeadBar BoListHead();

    }
}
