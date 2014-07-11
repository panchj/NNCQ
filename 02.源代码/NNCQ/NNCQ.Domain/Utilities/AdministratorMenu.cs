using NNCQ.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Utilities
{
    public class AdministratorMenu
    {
        public static List<SelfReferentialItem> CommonTopMenu()
        {
            var topMenuItemCollection = new List<SelfReferentialItem>();

            var m01 = new SelfReferentialItem() { ID = "001", ParentID = "001", FacadeStyle = FacadeStyle.Title, SortCode = "001", ItemName = "", OperateName = "", IconString = "icon-cog", IsActive = true };
            var m0101 = new SelfReferentialItem() { ID = "001001", ParentID = "001", FacadeStyle = FacadeStyle.Normal, SortCode = "001001", ItemName = "系统角色管理", OperateName = "ApplicationRole", IconString = "", IsActive = true };
            var m0102 = new SelfReferentialItem() { ID = "001002", ParentID = "001", FacadeStyle = FacadeStyle.Normal, SortCode = "001002", ItemName = "系统用户管理", OperateName = "ApplicationUser", IconString = "", IsActive = true };
            var m0103 = new SelfReferentialItem() { ID = "001003", ParentID = "001", FacadeStyle = FacadeStyle.Normal, SortCode = "001003", ItemName = "系统工作日志", OperateName = "SystemLog", IconString = "", IsActive = true };
            var m0104 = new SelfReferentialItem() { ID = "001004", ParentID = "001", FacadeStyle = FacadeStyle.Divider, SortCode = "001004", ItemName = "", OperateName = "", IconString = "", IsActive = true };
            var m0105 = new SelfReferentialItem() { ID = "001005", ParentID = "001", FacadeStyle = FacadeStyle.Normal, SortCode = "001005", ItemName = "系统类型定义", OperateName = "ApplicationBusinessType", IconString = "", IsActive = true };
            var m0106 = new SelfReferentialItem() { ID = "001006", ParentID = "001", FacadeStyle = FacadeStyle.Divider, SortCode = "001006", ItemName = "", OperateName = "", IconString = "", IsActive = true };
            
            var m0110 = new SelfReferentialItem() { ID = "001010", ParentID = "001", FacadeStyle = FacadeStyle.Normal, SortCode = "001010", ItemName = "退出系统", OperateName = "Home/Logout", IconString = "", IsActive = true };

            var m02 = new SelfReferentialItem() { ID = "002", ParentID = "002", FacadeStyle = FacadeStyle.Title, SortCode = "002", ItemName = "部门与人员管理", OperateName = "", IconString = "", IsActive = true };
            var m0201 = new SelfReferentialItem() { ID = "002001", ParentID = "002", FacadeStyle = FacadeStyle.Normal, SortCode = "002001", ItemName = "部门类型定义", OperateName = "DepartmentType", IconString = "", IsActive = true };
            var m0202 = new SelfReferentialItem() { ID = "002002", ParentID = "002", FacadeStyle = FacadeStyle.Normal, SortCode = "002002", ItemName = "部门职位定义", OperateName = "JobTitle", IconString = "", IsActive = true };
            var m0203 = new SelfReferentialItem() { ID = "002003", ParentID = "002", FacadeStyle = FacadeStyle.Normal, SortCode = "002003", ItemName = "部门职级定义", OperateName = "JobLevel", IconString = "", IsActive = true };
            var m0204 = new SelfReferentialItem() { ID = "002004", ParentID = "002", FacadeStyle = FacadeStyle.Divider, SortCode = "002004", ItemName = "", OperateName = "", IconString = "", IsActive = true };
            var m0205 = new SelfReferentialItem() { ID = "002005", ParentID = "002", FacadeStyle = FacadeStyle.Normal, SortCode = "002005", ItemName = "证件类型", OperateName = "CredentialsType", IconString = "", IsActive = true };
            var m0208 = new SelfReferentialItem() { ID = "002008", ParentID = "002", FacadeStyle = FacadeStyle.Divider, SortCode = "002008", ItemName = "", OperateName = "", IconString = "", IsActive = true };
            var m0209 = new SelfReferentialItem() { ID = "002009", ParentID = "002", FacadeStyle = FacadeStyle.Normal, SortCode = "002009", ItemName = "部门管理", OperateName = "Department", IconString = "", IsActive = true };
            var m0210 = new SelfReferentialItem() { ID = "002010", ParentID = "002", FacadeStyle = FacadeStyle.Normal, SortCode = "002010", ItemName = "人员管理", OperateName = "Person", IconString = "", IsActive = true };

            topMenuItemCollection.AddRange(new List<SelfReferentialItem>() { 
                m01,m0101,m0102, m0102, m0104,m0105,m0106, m0110, 
                m02,m0201, m0202, m0203,m0204,m0205, m0208,m0209, m0210, 
                //m03,m0301, m0302, m0303,m0304,
                //m04,m0401,m0403,m0404,m0405,
                //m05 ,m0501,m0502,m0503,m0504,m0505,m0506
            });
            return topMenuItemCollection;
        }
    }
}
