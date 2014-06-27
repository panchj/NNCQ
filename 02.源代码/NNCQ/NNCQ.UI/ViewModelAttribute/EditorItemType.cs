using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.ViewModelAttribute
{
    /// <summary>
    /// 定义最基本的数据（对象属性数据）编辑UI类型
    /// </summary>
    public enum EditorItemType
    {
        TextBox, 
        TextArea, 
        DorpdownOptionWithSelfReferentialItem,
        DorpdownOptionWithPlainFacadeItem,
        DorpdownOptionWithEnum,
        ComboBox, 
        CheckBox,
        YesNo, 
        Sex, 
        Date, 
        DateTime, 
        Time, 
        Hidden,
        SelfReferentialItem,
        PlainFacadeItem
    }
}
