using NNCQ.UI.Models.Web.MetroUICSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NNCQ.UI.ExtensionsOfMvcHelper
{
    public static class MucBoList
    {
        public static System.Web.Mvc.MvcHtmlString ForCommonPageView(this HtmlHelper html, MucPage_List pageModel) 
        {

            var htmlContentBuilder = new StringBuilder();

            htmlContentBuilder.Append("<div class='grid fluid'><div class='row'>");

            if (pageModel.LeftNavigator != null)
            { 
                #region 创建左侧工作区内容（一般是导航内容）
                htmlContentBuilder.Append("<div class='bs-docs-sidebar span3'>");
                htmlContentBuilder.Append("<div id='" + pageModel.LeftNavigator.ID + "'  style='" + pageModel.LeftNavigator.CssStyle + "'>");
                htmlContentBuilder.Append(pageModel.LeftNavigator.InnerHtmlContent);
                htmlContentBuilder.Append("</div></div>");
                #endregion
            }

            #region 创建右侧主工作区
            if (pageModel.LeftNavigator != null)
                htmlContentBuilder.Append("<div class='span9'>");
            else
                htmlContentBuilder.Append("<div class='span12'>");

            htmlContentBuilder.Append("<div id='" + pageModel.MainWorkPlace.ID + "' style='" + pageModel.MainWorkPlace.CssStyle + "'>");
            htmlContentBuilder.Append(pageModel.MainWorkPlace.InnerHtmlContent);
            htmlContentBuilder.Append("</div></div></div></div>");
            #endregion

            #region 创建附加工作区组件
            htmlContentBuilder.Append(pageModel.AdditionHtmlContent);
            #endregion

            #region 创建附件的脚本组件
            htmlContentBuilder.Append("");
            htmlContentBuilder.Append(pageModel.AdditionScriptContent);
            htmlContentBuilder.Append("");
            #endregion

            return System.Web.Mvc.MvcHtmlString.Create(htmlContentBuilder.ToString());
        }


    }
}
