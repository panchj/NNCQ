using NNCQ.UI.Models;
using NNCQ.UI.Models.Web.MetroUICSS;
using NNCQ.UI.Models.Web.MetroUICSS.SubControlModels;
using NNCQ.UI.ViewModelAttribute;
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
        public static MvcHtmlString ForCommonPageView(this HtmlHelper html, MucPage_List pageModel) 
        {

            var htmlContentBuilder = new StringBuilder();

            htmlContentBuilder.Append("<div class='grid fluid'><div class='row'>");

            if (pageModel.LeftNavigator != null)
            { 
                #region 创建左侧工作区内容（一般是导航内容）
                htmlContentBuilder.Append("<div class='span3'>");
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

            return MvcHtmlString.Create(htmlContentBuilder.ToString());
        }

        public static MvcHtmlString ForCommonPageJavaScript(this HtmlHelper html, MucPage_List pageModel) 
        {
            if (pageModel != null)
                return MvcHtmlString.Create(pageModel.AdditionScriptContent);
            else
                return MvcHtmlString.Create("");
        }

        /// <summary>
        /// 单一的标题组件，使用这个组件方式，需要自行定义以下两个脚本方法，一般是放在 Index 对应脚本区内。
        /// function boSearch(controllerName,searchAction)
        /// function boCreateOrEdit(id,controllerName,createOrEditAction)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static MvcHtmlString ForListTitleBar<T>(this HtmlHelper html,string keyword=null) 
        {
            var headBar = new Muc_MainWorkPlaceHeadBar();
            headBar.HeadBarTitle = new HeadBarTitle();
            headBar.HeadBarOperation = new HeadBarOperation();

            var boType = typeof(T);
            Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
            var headAttribute = boVMAttributes.Where(n => n.GetType().Name == "ListHeadSpecification").FirstOrDefault();
            var additonButtonAttribute = boVMAttributes.Where(n => n.GetType().Name == "ListHeaderAdditionalButton");
            if (headAttribute != null)
            {
                var hSpecification = headAttribute as ListHeadSpecification;

                headBar.HeadBarTitle.Title = hSpecification.Title;
                headBar.HeadBarOperation.ControllerName = hSpecification.ControllerName;
                headBar.HeadBarOperation.CreateActionPath = hSpecification.CreateActionPath;
                headBar.HeadBarOperation.SearchActionPath = hSpecification.SearchActionPath;

                if (additonButtonAttribute.Count() > 0)
                {
                    headBar.HeadBarOperation.ButtonItems = new List<CommonButtonItem>();
                    foreach (var item in additonButtonAttribute)
                    {
                        var tItem = item as ListHeaderAdditionalButton;
                        var bItem = new CommonButtonItem() { DisplayName = tItem.DisplayName, OnClickFunction = tItem.OnClickFunction, Width = tItem.Width };
                        headBar.HeadBarOperation.ButtonItems.Add(bItem);
                    }
                }

            }

            headBar.ID = "Header_" + boType.Name;
            headBar.Name = "Header_" + boType.Name;

            var serchFunction = "javascript:boSearch(\"" + headBar.HeadBarOperation.ControllerName + "\",\"" + headBar.HeadBarOperation.SearchActionPath + "\")";
            var createFunction = "javascript:boCreateOrEdit(\"" + Guid.NewGuid().ToString() + "\",\"" + headBar.HeadBarOperation.ControllerName + "\",\"" + headBar.HeadBarOperation.CreateActionPath + "\")";

            var seachValueString = "";
            if (!String.IsNullOrEmpty(keyword))
                seachValueString = "value='" + keyword + "'";

            var htmlString = new StringBuilder();
            htmlString.Append("<table style='width:100%'>");
            htmlString.Append("<tr>");
            htmlString.Append("<td><p class='subheader'>" + headBar.HeadBarTitle.Title + "</p></td>");

            //! 根据特性定义，判断是否创建检索组件
            if (!String.IsNullOrEmpty(headBar.HeadBarOperation.SearchActionPath))
            {
                htmlString.Append("<td style='width:250px'>");
                htmlString.Append("<div class='input-control text'><input id='serchKeyword' type='text' " + seachValueString + " /><button onclick='" + serchFunction + "' class='btn-search'></button></div>");
                htmlString.Append("</td>");
            }

            //! 根据特性定义，判断是否创建“新建数据”按钮
            if (!String.IsNullOrEmpty(headBar.HeadBarOperation.CreateActionPath))
            {
                htmlString.Append("<td style='width:110px;vertical-align:top; text-align:right'>");
                htmlString.Append("<button class='button info' type='button' onclick='" + createFunction + "' style='height:33px'>");
                htmlString.Append("<i class='icon-new icon-white'></i> 新建数据");
                htmlString.Append("</button>");
                htmlString.Append("</td>");
            }

            //! 判断是否有附加按钮
            if (headBar.HeadBarOperation.ButtonItems != null)
            {
                if (headBar.HeadBarOperation.ButtonItems.Count > 0)
                {
                    foreach (var bItem in headBar.HeadBarOperation.ButtonItems)
                    {
                        htmlString.Append("<td style='width:" + bItem.Width + "px; vertical-align:top;text-align:center'>");
                        htmlString.Append("<button class='button info' type='button' onclick='" + bItem.OnClickFunction + "' style='height:33px'>");
                        htmlString.Append(bItem.DisplayName);
                        htmlString.Append("</button>");
                        htmlString.Append("</td>");
                    }
                }
            }
            htmlString.Append("</tr>");
            htmlString.Append("</table>");
            headBar.InnerHtmlContent = htmlString.ToString();

            return MvcHtmlString.Create(headBar.InnerHtmlContent.ToString());
        }
    }
}
