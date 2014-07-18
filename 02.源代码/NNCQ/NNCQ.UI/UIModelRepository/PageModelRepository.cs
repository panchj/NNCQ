using NNCQ.UI.Models;
using NNCQ.UI.Models.Web.MetroUICSS;
using NNCQ.UI.Models.Web.MetroUICSS.SubControlModels;
using NNCQ.UI.ViewModelAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.UIModelRepository
{
    /// <summary>
    /// 根据业务实体的视图模型的所有规格方面的定义，生成基于整个页面的组件
    /// </summary>
    /// <typeparam name="T">需要处理的实体视图模型泛型定义</typeparam>
    public class PageModelRepository<T> where T:class
    {
        /// <summary>
        /// 根据前端提供的实体视图类的集合直接生成全部的列表页面的方法
        /// </summary>
        /// <param name="boVMCollection">前端的业务对象视图模型集合</param>
        /// <param name="treeNodes">左侧导航树的自引用模式的节点集合，如果为空值，则不不使用左侧导航节点空间</param>
        /// <param name="paginate">分页器，如果为空，直接将集合全部列出，并且不显示分页指示条</param>
        /// <param name="typeID">用于关联导航树指向节点的链接</param>
        /// <returns></returns>
        public static MucPage_List GetPageMode(List<T> boVMCollection, List<SelfReferentialItem> treeNodes = null, MucPaginate paginate = null,string typeID=null) 
        {
            var pageModel = new MucPage_List();

            var boType = typeof(T);
            Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);

            #region 创建左侧导航页面数据，导航树的方式，根据 ListNavigatorType 来确定。
            if (treeNodes != null)
            {
                var leftNavigatorTitle = "分类导航";
                var leftNavigatorAttribute = boVMAttributes.Where(n => n.GetType().Name == "ListNavigator").FirstOrDefault();
                if (leftNavigatorAttribute != null)
                {
                    var nvAttribute = leftNavigatorAttribute as ListNavigator;
                    leftNavigatorTitle = nvAttribute.Title;
                    if (nvAttribute.NavigatorType == ListNavigatorType.TreeView)
                    {
                        pageModel.HasLeftNavigator = true;
                        pageModel.LeftNavigator = LeftNavigatorWithTreeViewInitializer.GetLeftNavigator(treeNodes, leftNavigatorTitle);
                    }
                    else
                    {
                        if (nvAttribute.NavigatorType == ListNavigatorType.SideBar)
                        {
                            pageModel.HasLeftNavigator = true;
                            pageModel.LeftNavigator = LeftNavigatorWithSideBarInitializer.GetLeftNavigator(treeNodes, leftNavigatorTitle,typeID);
                        }
                    }
                }
            } 
            #endregion

            // 主工作区
            pageModel.MainWorkPlace = MainWorkPlaceInitializer.GetMainWorkPlace(boVMCollection, paginate);

            var extensionJavaScript = boVMAttributes.Where(n => n.GetType().Name == "ExtensionJavaScriptFile");
            var extensionJavaScriptString = "";
            foreach (var jItem in extensionJavaScript) 
            {
                var jfile = "<script type=\"text/javascript\" src=\"" + (jItem as ExtensionJavaScriptFile).FileSource + "\"></script>";
                extensionJavaScriptString = extensionJavaScriptString + jfile;
            }

            // 封装的Jscript脚本库
            pageModel.AdditionScriptContent =AdditionScriptContent.Get()+ extensionJavaScriptString;

            return pageModel;
        }

        /// <summary>
        /// 重载的根据前端提供的业务对象集合与视图模型直接生成全部页面的方法
        /// </summary>
        /// <param name="boVMCollection">前端的业务对象视图模型集合</param>
        /// <param name="leftStatusString">直接在前端定义好的链接 Html 代码字符串</param>
        /// <param name="paginate">分页器</param>
        /// <param name="typeID">用于关联导航树指向节点的链接</param>
        /// <returns></returns>
        public static MucPage_List GetPageModel(List<T> boVMCollection, string leftStatusString, MucPaginate paginate = null, string typeID = null) 
        {
            var boType = typeof(T);
            Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);

            var pageModel = new MucPage_List();

            var leftNavigator = new Muc_LeftNavigator();
            leftNavigator.Name = "";
            leftNavigator.InnerHtmlContent = leftStatusString;

            pageModel.MainWorkPlace = MainWorkPlaceInitializer.GetMainWorkPlace(boVMCollection, paginate);

            pageModel.AdditionScriptContent = AdditionScriptContent.Get();

            return pageModel;
        }

        /// <summary>
        /// 用于更新列表数据内容页面局部内容的方法
        /// </summary>
        /// <param name="boVMCollection">前端的业务对象视图模型集合，一般是根据某些条件处理之后的对象集合</param>
        /// <param name="keyword">关键词</param>
        /// <param name="paginate">分页器</param>
        /// <returns></returns>
        public static ListPartialPageUpdateInfo PageUpdate(List<T> boVMCollection, string keyword = null, MucPaginate paginate = null) 
        {
            var boType = typeof(T);
            Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
            var sAttribute = boVMAttributes.Where(n => n.GetType().Name == "ListDataGridViewSpecification").FirstOrDefault() as ListDataGridViewSpecification;

            var updatePage = new ListPartialPageUpdateInfo();
            updatePage.Header = MainWorkPlaceInitializer.GetHeader(keyword);
            updatePage.DataGridView = MainWorkPlaceInitializer.GetDataGridViewContent(boVMCollection);
            if (paginate!=null)
                updatePage.Bottom = MainWorkPlaceInitializer.GetBottom(sAttribute.ControllerName, sAttribute.ListActionPath, paginate);
            return updatePage;
        }

        /// <summary>
        /// 根据 SelfReferentialItem 节点集合创建导航树的 Html 代码
        /// </summary>
        /// <param name="treeNodes">SelfReferentialItem 节点集合</param>
        /// <param name="title">标题</param>
        /// <param name="typeID">指定的类型ID名称</param>
        /// <returns></returns>
        public static Muc_LeftNavigator GetSideBarNavigator(List<SelfReferentialItem> treeNodes, string title, string typeID = null) 
        {
            return LeftNavigatorWithSideBarInitializer.GetLeftNavigator(treeNodes, title, typeID);
        }

        public static Muc_LeftNavigator GetTreeViewNavigator(List<SelfReferentialItem> treeNodes, string title, string typeID = null)
        {
            return LeftNavigatorWithTreeViewInitializer.GetLeftNavigator(treeNodes, title);
        }

        /// <summary>
        /// 层次导航树生成器
        /// </summary>
        class LeftNavigatorWithTreeViewInitializer
        {
            public static Muc_LeftNavigator GetLeftNavigator(List<SelfReferentialItem> treeNodes, string title)
            {
                var leftNavigator = new Muc_LeftNavigator();
                leftNavigator.Name = "";
                leftNavigator.InnerHtmlContent = _GetInnerHtmlContent(treeNodes, title);
                return leftNavigator;
            }

            private static string _GetInnerHtmlContent(List<SelfReferentialItem> treeNodes, string title)
            {
                var treeViewContent = new StringBuilder();

                treeViewContent.Append("<table style='width:100%'>");


                #region 1. 标题
                treeViewContent.Append("<tr>");
                treeViewContent.Append("<td style='width:10px;'>");
                treeViewContent.Append("</td>");
                treeViewContent.Append("<td>");
                treeViewContent.Append("<div style='padding:5px;font-size:16px'>" + title + "</div>");
                treeViewContent.Append("</td>");
                treeViewContent.Append("</tr>");
                #endregion

                #region 2. 横线
                treeViewContent.Append("<tr>");
                treeViewContent.Append("<td colspan='2' style='height:2px'><div style='background-color:#808080; height:1px'></div></td>");
                treeViewContent.Append("</tr>");
                #endregion

                #region 3. TreeView 节点正文
                treeViewContent.Append("<tr>");
                treeViewContent.Append("<td style='width:10px;background-color:whitesmoke'>");
                treeViewContent.Append("</td>");
                treeViewContent.Append("<td style='padding:3px'>");
                treeViewContent.Append("<div id='' style='font-size:14px'>");
                treeViewContent.Append("<ul class='treeview' data-role='treeview'>");

                //! 提取树节点数据
                treeViewContent.Append(_GetRootNodes(treeNodes));

                treeViewContent.Append("</ul>");
                treeViewContent.Append("</div>");
                treeViewContent.Append("</td>");
                treeViewContent.Append("</tr>");
                #endregion
                treeViewContent.Append("</table>");

                return treeViewContent.ToString();
            }

            #region 创建常规的树节点的代码
            /// <summary>
            /// 创建根节点
            /// </summary>
            /// <returns></returns>
            private static string _GetRootNodes(List<SelfReferentialItem> treeNodes)
            {
                var boType = typeof(T);
                Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
                var sAttribute = boVMAttributes.Where(n => n.GetType().Name == "ListDataGridViewSpecification").FirstOrDefault() as ListDataGridViewSpecification;
                var nAttribute = boVMAttributes.Where(n => n.GetType().Name == "ListNavigator").FirstOrDefault() as ListNavigator;

                var rootNodesContent = new StringBuilder();
                var rootNodes = treeNodes.Where(rn => rn.ID == rn.ParentID || rn.ParentID == "");
                foreach (var rootNode in rootNodes)
                {
                    var alinkString = "javascript:boNavigator(\"" + rootNode.ID + "\", \"" + sAttribute.ControllerName + "\",\"" + nAttribute.NavigatorAction + "\")";

                    var liCssClass = "";
                    // 提取扩展点图标
                    var collapseMark = _GetCollapseMark(rootNode.ID, treeNodes);
                    if (!String.IsNullOrEmpty(collapseMark))
                        liCssClass = "class='node'";
                    rootNodesContent.Append("<li " + liCssClass + ">");

                    rootNodesContent.Append("<a href='#' onclick='" + alinkString + "'>" + collapseMark);
                    rootNodesContent.Append("<span style='font-size:15px'>"+rootNode.ItemName+"</span>");
                    rootNodesContent.Append("</a>");

                    // 提取子节点
                    rootNodesContent.Append(_GetChildNodes(rootNode, treeNodes));

                    rootNodesContent.Append("</li>");
                }
                return rootNodesContent.ToString();
            }

            /// <summary>
            /// 根据指定的节点，创建其下的子节点
            /// </summary>
            /// <param name="rootNode"></param>
            /// <returns></returns>
            private static string _GetChildNodes(SelfReferentialItem rootNode, List<SelfReferentialItem> treeNodes)
            {
                var boType = typeof(T);
                Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
                var sAttribute = boVMAttributes.Where(n => n.GetType().Name == "ListDataGridViewSpecification").FirstOrDefault() as ListDataGridViewSpecification;
                var nAttribute = boVMAttributes.Where(n => n.GetType().Name == "ListNavigator").FirstOrDefault() as ListNavigator;

                var childNodesContent = new StringBuilder();
                var childNodes = treeNodes.Where(sn => sn.ParentID == rootNode.ID && sn.ID != sn.ParentID);
                if (childNodes.Count() > 0)
                {
                    childNodesContent.Append("<ul>");
                    foreach (var childNode in childNodes)
                    {
                        var alinkString = "javascript:boNavigator(\"" + childNode.ID + "\", \"" + sAttribute.ControllerName + "\",\"" + nAttribute.NavigatorAction + "\")";
                        var liCssClass = "";
                        // 提取扩展点图标
                        var collapseMark = _GetCollapseMark(childNode.ID, treeNodes);
                        if (!String.IsNullOrEmpty(collapseMark))
                            liCssClass = "class='node collapsed'";

                        childNodesContent.Append("<li " + liCssClass + ">");
                        childNodesContent.Append("<a href='#' onclick='" + alinkString + "'>" + collapseMark);
                        childNodesContent.Append("<span style='font-size:15px'>" + childNode.ItemName+"</span>");
                        childNodesContent.Append("</a>");
                        childNodesContent.Append(_GetChildNodes(childNode, treeNodes));
                        childNodesContent.Append("</li>");
                    }
                    childNodesContent.Append("</ul>");
                }
                return childNodesContent.ToString();
            }

            /// <summary>
            /// 通过判断是否有下级节点来返回一个用于展开的操作导航符号
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            private static string _GetCollapseMark(string id, List<SelfReferentialItem> treeNodes)
            {
                string cMark = "";
                var childNodeCount = treeNodes.Where(rn => rn.ParentID == id && rn.ID != rn.ParentID).Count();
                if (childNodeCount > 0)
                    cMark = "<span class='node-toggle'></span>";

                return cMark;
            }

            #endregion

            public string GetInnerHtmlContentAsTreeViewWithPlainFacadeItems(List<PlainFacadeItem> plainFacadeItems)
            {
                return "";
            }

        }

        /// <summary>
        /// 基于 Metro UI CSS 的 SideBar 样式的导航清单生成器
        /// </summary>
        class LeftNavigatorWithSideBarInitializer 
        {
            public static Muc_LeftNavigator GetLeftNavigator(List<SelfReferentialItem> treeNodes, string title,string typeID)
            {
                var leftNavigator = new Muc_LeftNavigator();
                leftNavigator.InnerHtmlContent = _GetInnerHtmlContent(treeNodes, title, typeID);
                return leftNavigator;
            }


            private static string _GetInnerHtmlContent(List<SelfReferentialItem> treeNodes, string title,string typeID)
            {
                var sideBarContent = new StringBuilder();

                if (String.IsNullOrEmpty(title))
                {
                    sideBarContent.Append("<h3>" + title + "</h3>");
                }
                sideBarContent.Append("<nav class='sidebar light fixed-top'>");
                sideBarContent.Append("<ul>");

                sideBarContent.Append(_GetRootNodes(treeNodes));

                sideBarContent.Append("</ul>");
                sideBarContent.Append("</nav>");

                return sideBarContent.ToString();
            }

            private static string _GetRootNodes(List<SelfReferentialItem> treeNodes)
            {
                var boType = typeof(T);
                Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
                var sAttribute = boVMAttributes.Where(n => n.GetType().Name == "ListDataGridViewSpecification").FirstOrDefault() as ListDataGridViewSpecification;
                var nAttribute = boVMAttributes.Where(n => n.GetType().Name == "ListNavigator").FirstOrDefault() as ListNavigator;

                var rootNodesContent = new StringBuilder();
                var titleNodes = treeNodes.Where(rn => rn.ID == rn.ParentID);
                foreach (var titleNode in titleNodes)
                {

                    rootNodesContent.Append("<li class='title' style='background-color:whitesmoke'>" + titleNode.ItemName + "</li>");

                    // 提取与标题关联的根节点
                    var titleSubItems = treeNodes.Where(x => x.ParentID == titleNode.ID && x.ParentID != x.ID);

                    foreach (var tsItem in titleSubItems) 
                    {
                        var alinkString = "javascript:boNavigator(\"" + tsItem.ID + "\", \"" + sAttribute.ControllerName + "\",\"" + nAttribute.NavigatorAction + "\")";
                        var liCssString = "";
                        if (tsItem.IsActive)
                            liCssString = "stick bg-green active ";
                        if (tsItem.FacadeStyle == FacadeStyle.Normal)
                            liCssString = liCssString + "";
                        if (tsItem.FacadeStyle == FacadeStyle.Title)
                            liCssString = liCssString + "title ";
                        if (tsItem.FacadeStyle == FacadeStyle.Disabled)
                            liCssString = liCssString + "disabled ";
                        if (tsItem.FacadeStyle == FacadeStyle.Divider)
                            liCssString = liCssString + "divider ";

                        var subItems = treeNodes.Where(x => x.ParentID == tsItem.ID && x.ParentID != x.ID);

                        var iconString = "";
                        if (!String.IsNullOrEmpty(titleNode.IconString))
                            iconString = "<i class='" + titleNode.IconString + "'></i>";

                        var liContent = "<li class='" + liCssString + "'><a class='' href='" + alinkString + "'>" + iconString + tsItem.ItemName + "</a></li>";
                        if (subItems.Count() > 0) 
                        {
                            liContent = "<li class='" + liCssString + "'>";
                            liContent = liContent + "<a class='dropdown-toggle' href='" + alinkString + "'>" + iconString + tsItem.ItemName + "</a>";
                            liContent = liContent + "<ul class='dropdown-menu' data-role='dropdown'>";
                            liContent = liContent + _GetChildNodes(tsItem,treeNodes);
                            liContent = liContent + "</ul>";
                            liContent = liContent + "</li>";
                        }
                        rootNodesContent.Append(liContent);


                    }
                }
                return rootNodesContent.ToString();
            }

            private static string _GetChildNodes(SelfReferentialItem rootNode, List<SelfReferentialItem> treeNodes)
            {
                var boType = typeof(T);
                Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
                var sAttribute = boVMAttributes.Where(n => n.GetType().Name == "ListDataGridViewSpecification").FirstOrDefault() as ListDataGridViewSpecification;
                var nAttribute = boVMAttributes.Where(n => n.GetType().Name == "ListNavigator").FirstOrDefault() as ListNavigator;

                var childNodesContent = new StringBuilder();
                var childNodes = treeNodes.Where(sn => sn.ParentID == rootNode.ID && sn.ID != sn.ParentID);
                if (childNodes.Count() > 0)
                {
                    foreach (var childNode in childNodes)
                    {
                        var alinkString = "javascript:boNavigator(\"" + childNode.ID + "\", \"" + sAttribute.ControllerName + "\",\"" + nAttribute.NavigatorAction + "\")";
                        var liCssString = "";
                        if (childNode.IsActive)
                            liCssString = "stick bg-green active ";
                        if (childNode.FacadeStyle == FacadeStyle.Normal)
                            liCssString = liCssString + "";
                        if (childNode.FacadeStyle == FacadeStyle.Title)
                            liCssString = liCssString + "title ";
                        if (childNode.FacadeStyle == FacadeStyle.Disabled)
                            liCssString = liCssString + "disabled ";
                        if (childNode.FacadeStyle == FacadeStyle.Divider)
                            liCssString = liCssString + "divider ";

                        var iconString = "";
                        if (!String.IsNullOrEmpty(childNode.IconString))
                            iconString = "<i class='" + childNode.IconString + "'></i>";

                        var liContent = "<li class='" + liCssString + "'><a class='' href='" + alinkString + "'>" + iconString + childNode.ItemName + "</a></li>";
                        var subItems = treeNodes.Where(x => x.ParentID == childNode.ID && x.ParentID != x.ID);
                        if (subItems.Count() > 0)
                        {
                            liContent = "<li class='" + liCssString + "'>";
                            liContent = liContent + "<a class='dropdown-toggle' href='" + alinkString + "'>" + iconString + childNode.ItemName + "</a>";
                            liContent = liContent + "<ul class='dropdown-menu' data-role='dropdown'>";
                            liContent = liContent + _GetChildNodes(childNode, treeNodes);
                            liContent = liContent + "</ul>";
                            liContent = liContent + "</li>";
                        }

                        childNodesContent.Append(liContent);

                    }
                }
                return childNodesContent.ToString();
            }

            public string GetInnerHtmlContentAsSideBarWithPlainFacadeItems(List<PlainFacadeItem> plainFacadeItems)
            {
                return "";
            }

        }

        /// <summary>
        /// 另外一组树结构视图的组件
        /// </summary>
        class JQueryTreeViewInitializer
        {

        }

        /// <summary>
        /// 主工作区生成器,用于创建呈现业务对象视图模型集合的列表
        /// </summary>
        public class MainWorkPlaceInitializer
        {
            public static Muc_MainWorkPlace GetMainWorkPlace(List<T> boVMCollection, MucPaginate paginate, bool? userUDCollumn = null) 
            {

                var mainWorkPLace = new Muc_MainWorkPlace();
                var boType = typeof(T);

                var headerDivID = "Header_" + boType.Name;     // 顶部 div 
                var dgvDivID = "DataGridView_" + boType.Name;  // 中间类似 DataGrid 用途的 div
                var bottomDivID = "Bottom_" + boType.Name;     // 底部 div

                #region 提取对应列表数据的的特性
                Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
                var sAttribute = boVMAttributes.Where(n => n.GetType().Name == "ListDataGridViewSpecification").FirstOrDefault() as ListDataGridViewSpecification;
                #endregion 
               
                // 生成列表的表头
                mainWorkPLace.MainWorkPlaceHeadBar = _GetHeadbar();
                // 生成列表的表体
                mainWorkPLace.MainWorkPlaceDataGridView = _GetDataGridView(boVMCollection, userUDCollumn);
                // 根据分页器的状态生成列表底部
                if (paginate != null)
                    mainWorkPLace.MainWorkPlaceBottomBar = _GetBottomBar(sAttribute.ControllerName, sAttribute.ListActionPath, paginate);
                else
                {
                    mainWorkPLace.MainWorkPlaceBottomBar = new Muc_MainWorkPlaceBottomBar();
                    mainWorkPLace.MainWorkPlaceBottomBar.InnerHtmlContent = "";
                }
                //! 合成主工作区局部页 Html 字符串代码
                mainWorkPLace.InnerHtmlContent =
                    "<div id='" + headerDivID + "'name='" + headerDivID + "'>" + mainWorkPLace.MainWorkPlaceHeadBar.InnerHtmlContent + "</div>" +
                    "<div id='" + dgvDivID + "'name='" + dgvDivID + "'>" + mainWorkPLace.MainWorkPlaceDataGridView.InnerHtmlContent + "</div>" +
                    "<div id='" + bottomDivID + "'name='" + bottomDivID + "'>" + mainWorkPLace.MainWorkPlaceBottomBar.InnerHtmlContent + "</div>";
                
                return mainWorkPLace;
            }

            /// <summary>
            /// 公开的，直接供前端控制器调用的列表页面表头方法
            /// </summary>
            /// <param name="keyword"></param>
            /// <returns></returns>
            public static string GetHeader(string keyword = null) 
            {
                return _GetHeadbar(keyword).InnerHtmlContent;
            }

            /// <summary>
            /// 公开的，直接供前端控制器调用的列表页面表体的方法
            /// </summary>
            /// <param name="boVMCollection"></param>
            /// <returns></returns>
            public static string GetDataGridViewContent(List<T> boVMCollection,bool ? userUDCollumn = null) 
            {
                return _GetDataGridView(boVMCollection,userUDCollumn).InnerHtmlContent;
            }

            /// <summary>
            /// 公开的公开的，直接供前端控制器调用的列表页面底部的方法
            /// </summary>
            /// <param name="controller"></param>
            /// <param name="listAction"></param>
            /// <param name="paginate"></param>
            /// <param name="typeID"></param>
            /// <returns></returns>
            public static string GetBottom(string controller, string listAction, MucPaginate paginate,string typeID = null) 
            {
                return _GetBottomBar(controller,listAction,paginate,typeID).InnerHtmlContent;
            }

            /// <summary>
            /// 创建表头部件
            /// </summary>
            /// <param name="keyword"></param>
            /// <returns></returns>
            private static Muc_MainWorkPlaceHeadBar _GetHeadbar(string keyword = null) 
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

                    if (additonButtonAttribute.Count()>0)
                    {
                        headBar.HeadBarOperation.ButtonItems = new List<CommonButtonItem>();
                        foreach (var item in additonButtonAttribute)
                        {
                            var tItem = item as ListHeaderAdditionalButton;
                            var bItem = new CommonButtonItem() { DisplayName = tItem.DisplayName, OnClickFunction = tItem.OnClickFunction, Width=tItem.Width };
                            headBar.HeadBarOperation.ButtonItems.Add(bItem);
                        }
                    }

                }

                headBar.ID = "Header_"+boType.Name;
                headBar.Name = "Header_" + boType.Name;

                var serchFunction = "javascript:boSearch(\"" + headBar.HeadBarOperation.ControllerName + "\",\"" + headBar.HeadBarOperation.SearchActionPath + "\")";
                var createFunction = "javascript:boCreateOrEdit(\""+Guid.NewGuid().ToString()+"\",\"" + headBar.HeadBarOperation.ControllerName + "\",\"" + headBar.HeadBarOperation.CreateActionPath + "\")";

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
                            htmlString.Append("<td style='width:"+bItem.Width+"px; vertical-align:top;text-align:center'>");
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
                return headBar;
            }

            /// <summary>
            /// 创建列表数据处理模型
            /// </summary>
            /// <param name="boVMCollection">业务实体视图模型对象集合</param>
            /// <param name="userUDCollumn">是否使用带有 编辑、明细、删除 的数据操作导航行列</param>
            /// <returns></returns>
            private static Muc_MainWorkPlaceDataGridView _GetDataGridView(List<T> boVMCollection ,bool ? userUDCollumn = null) 
            {
                var useUD = userUDCollumn ?? true;

                var dgv = new Muc_MainWorkPlaceDataGridView();
                var boType = typeof(T);

                Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
                var dgvAttribute = boVMAttributes.Where(n => n.GetType().Name == "ListDataGridViewSpecification").FirstOrDefault();
                if (dgvAttribute != null)
                {
                    var dgvAttr = dgvAttribute as ListDataGridViewSpecification;

                    dgv.ID = "DataGridView_" + boType.Name;
                    dgv.Name = "DataGridView_" + boType.Name;
                    dgv.ControllerName = dgvAttr.ControllerName;
                    dgv.rows = dgvAttr.Rows;
                    dgv.EditActionPath = dgvAttr.EditActionPath;
                    dgv.DetailActionPath = dgvAttr.DetailActionPath;
                    dgv.DeleteActionPath = dgvAttr.DeleteActionPath;

                }

                #region 表头数据规格
                var listColumnHeaderItems = new List<ListColumnHeader>();
                PropertyInfo[] properties = boType.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    #region ViewModel定义的的列表项
                    var propertyAttribute = property.GetCustomAttribute(typeof(ListItemSpecification), false) as ListItemSpecification;
                    if (propertyAttribute != null)
                    {
                        var ct = new ListColumnHeader()
                        {
                            Title = propertyAttribute.ListName,
                            Width = propertyAttribute.Width.ToString(),
                            OrderSort = propertyAttribute.OrderSort,
                            UseSortIndicator = propertyAttribute.UseSortIndicator,
                            PropertyName = property.Name
                        };
                        listColumnHeaderItems.Add(ct);
                    } 
                    #endregion

                    #region 附加列
                    var addColAttribute = property.GetCustomAttribute(typeof(AdditionOperationItem), false) as AdditionOperationItem;
                    if (addColAttribute != null)
                    {
                        var ct = new ListColumnHeader()
                        {
                            Title = addColAttribute.Title,
                            Width = addColAttribute.Width.ToString(),
                            OrderSort = addColAttribute.SortCode,
                            UseSortIndicator = false,
                            PropertyName = ""
                        };
                        listColumnHeaderItems.Add(ct);
                    } 
                    #endregion

                }
                if (properties.Where(x => x.Name.ToLower() == "id").FirstOrDefault()!=null)
                    listColumnHeaderItems.Add(_GetDefaultOperationCol());

                #endregion

                var htmlString = new StringBuilder();

                htmlString.Append("<table class='table bordered hovered' style='word-break:break-all'>");

                #region 表头数据
                htmlString.Append("<thead><tr style='background-color:whitesmoke'>");
                foreach (var colItem in listColumnHeaderItems.OrderBy(x => x.OrderSort))
                {
                    var widthString = "";
                    if (colItem.Width != "0")
                        widthString = "width:" + colItem.Width + "px";
                    htmlString.Append("<th class='text-left' style='" + widthString + "'>" + colItem.Title + "</th>");
                }
                htmlString.Append("</tr></thead>");
                #endregion

                #region 表体数据
                htmlString.Append("<tbody>");

                var boValueString = "";
                foreach (var boVMItem in boVMCollection)
                {
                    htmlString.Append("<tr>");

                    #region 业务实体视图模型对象数据
                    foreach (var colItem in listColumnHeaderItems.OrderBy(x => x.OrderSort))
                    {
                        if (!String.IsNullOrEmpty(colItem.PropertyName))
                        {
                            var tdCssClass = "text-left";
                            htmlString.Append("<td class='" + tdCssClass + "'>");
                            var property = properties.Where(x => x.Name == colItem.PropertyName).FirstOrDefault();
                            var propertyValue = property.GetValue(boVMItem);
                            var valueString = "";
                            if (propertyValue != null)
                            {
                                valueString = propertyValue.ToString();
                                if (propertyValue.GetType().Name == "PlainFacadeItem")
                                {
                                    var tempValue = propertyValue as PlainFacadeItem;
                                    valueString = tempValue.Name;
                                }
                                if (propertyValue.GetType().Name == "SelfReferentialItem")
                                {
                                    var tempValue = propertyValue as SelfReferentialItem;
                                    valueString = tempValue.ItemName;
                                }
                            }

                            htmlString.Append(valueString);
                            htmlString.Append("</td>");
                            if (colItem.PropertyName == "Name")
                                boValueString = propertyValue.ToString();
                        }
                    } 
                    #endregion

                    #region 附加操作导航定义
                    foreach (var addItemPrperty in properties)
                    {
                        var displayString = "";
                        var addColAttribute = addItemPrperty.GetCustomAttribute(typeof(AdditionOperationItem), false) as AdditionOperationItem;
                        if (addColAttribute != null)
                        {
                            var propertyValue = addItemPrperty.GetValue(boVMItem) as List<CommonAlinkItem>;
                            if (propertyValue != null)
                            {
                                foreach (var item in propertyValue)
                                {
                                    var itemUrl = "";
                                    itemUrl = "<a href='" + item.OnClickFunction + "'>" + item.DisplayName + "</a>";
                                    displayString = displayString + itemUrl + " ";
                                }
                            }
                            htmlString.Append("<td class='text-left'>");
                            htmlString.Append(displayString);
                            htmlString.Append("</td>");
                        }
                    } 
                    #endregion

                    #region 编辑、明细、删除操作导航
                    if (useUD)
                    {
                        var keyProperty = properties.Where(x => x.Name.ToLower() == "id").FirstOrDefault();
                        if (keyProperty != null)
                        {
                            var KeyPropertyValue = keyProperty.GetValue(boVMItem);
                            var objID = KeyPropertyValue.ToString();
                            htmlString.Append(_GetDefaultOperationColValue(objID, dgv.ControllerName, dgv.EditActionPath, dgv.DetailActionPath, dgv.DeleteActionPath, boValueString));
                        }
                    }
                    #endregion

                    htmlString.Append("</tr>");
                }

                if (dgv.rows != 0)
                {
                    for (int i = 0; i < dgv.rows - boVMCollection.Count(); i++)
                    {
                        htmlString.Append("<tr>");
                        foreach (var colItem in listColumnHeaderItems.OrderBy(x => x.OrderSort))
                        {
                            if (!String.IsNullOrEmpty(colItem.PropertyName))
                            {
                                var tdCssClass = "text-left";
                                htmlString.Append("<td class='" + tdCssClass + "'>　");
                                htmlString.Append("</td>");
                            }
                        }
                        foreach (var addItemPrperty in properties)
                        {
                            var addColAttribute = addItemPrperty.GetCustomAttribute(typeof(AdditionOperationItem), false) as AdditionOperationItem;
                            if (addColAttribute != null)
                            {
                                htmlString.Append("<td class='text-center'>");
                                htmlString.Append("</td>");
                            }
                        }

                        htmlString.Append("<td> </td>");
                        htmlString.Append("</tr>");

                    }
                }

                htmlString.Append("</tbody>");  
                #endregion

                htmlString.Append("</table>");

                // htmlString.Append("</div>");

                dgv.InnerHtmlContent = htmlString.ToString();

                return dgv;
            }

            private static ListColumnHeader _GetDefaultOperationCol() 
            {
                return new ListColumnHeader() { Title = "数据操作", Width = "80", OrderSort = "99", PropertyName = "", UseSortIndicator = false };
            }

            private static string _GetDefaultOperationColValue(string id, string controller,string edit,string detail,string delete,string bovalue)
            {
                var deleteOperationModel = new MucDialogue_Delete(id, "删除数据标题", controller, delete);
                var htmlString = new StringBuilder();
                htmlString.Append("<td class='text-center'>");

                #region 编辑操作导航
                htmlString.Append("<a href='javascript:boCreateOrEdit(\"" + id + "\",\"" + controller + "\",\"" + edit + "\")'>");
                htmlString.Append("<i class='icon-pencil'  style='color:gray' onmouseover=\"this.style.color='red'\" onmouseout=\"this.style.color='gray'\"></i> </a>"); 
                #endregion

                #region 明细操作导航
                var detailString01 = "<a href='javascript:boDetail(\"" + id + "\",\"" + controller + "\",\"" + detail + "\")'>";
                var detailString02 = "<i class='icon-file' style='color:gray' onmouseover=\"this.style.color='red'\" onmouseout=\"this.style.color='gray'\"></i>  </a>";
                string[] detailItems = detail.Split('|');
                if (detailItems.Count() == 2)
                {
                    if (detailItems[1].Contains("javascript"))
                    {
                        detailString01="<a href='"+detailItems[1]+"(\"" + id + "\")'>";
                        detailString02="<i class='icon-file' style='color:gray' onmouseover=\"this.style.color='red'\" onmouseout=\"this.style.color='gray'\"></i>  </a>";
                    }
                }
                htmlString.Append(detailString01);
                htmlString.Append(detailString02);
                #endregion

                #region 删除操作导航
                htmlString.Append("<a href=\"javascript:boDelete(" +
                           "'" + deleteOperationModel.IconName + "'," +
                           "'" + deleteOperationModel.CaptionName + "'," +
                           "" + deleteOperationModel.Width + "," +
                           "" + deleteOperationModel.Height + "," +
                           "'" + id + "'," +
                           "'" + bovalue + "'," +
                           "'" + controller + "'," +
                           "'" + delete + "')\">");
                htmlString.Append("<i class='icon-remove'  style='color:gray' onmouseover=\"this.style.color='red'\" onmouseout=\"this.style.color='gray'\"></i>  </a>"); 
                #endregion
                
                htmlString.Append("</td>");

                return htmlString.ToString();
            }

            private static Muc_MainWorkPlaceBottomBar _GetBottomBar(string controller, string listAction, MucPaginate paginate,string typeID = null) 
            {
                
                var boType = typeof(T);

                var bottomBar = new Muc_MainWorkPlaceBottomBar();
                var startPage = 1;
                var endPage = 8;

                var typeIDString = "";
                if (!String.IsNullOrEmpty(typeID))
                    typeIDString = ",\""+typeID+"\"";

                var totalBarSection = (int)Math.Ceiling(paginate.TotalPageCount / (double)8);

                if (paginate.TotalPageCount % (double)8 > 0.0)
                    totalBarSection = totalBarSection + 1;
                if (totalBarSection == 0)
                    totalBarSection = totalBarSection + 1;

                for (int i = 0; i < totalBarSection; i++)
                {
                    var s = i * 8 + 1;
                    var e = (i + 1) * 8;

                    if (paginate.PageIndex >= s && paginate.PageIndex <= e)
                    {
                        startPage = s;
                        endPage = e;
                        break;
                    }
                }

                if (endPage > paginate.TotalPageCount)
                    endPage = paginate.TotalPageCount;
                
                bottomBar.ID = "Bottom_" + boType.Name;
                bottomBar.Name = "Bottom_" + boType.Name;

                var htmlString = new StringBuilder();

                // htmlString.Append("<div id='" + bottomBar.ID + "' name='" + bottomBar.Name + "'>");
                htmlString.Append("<table style='width:100%'><tr>");
                htmlString.Append("<td>");
                htmlString.Append("<div class='pagination text-right'><ul>");
                if(paginate.PageIndex > 1)
                    htmlString.Append("<li class='first'><a href='javascript:boGotoPage(\"1\",\"" + controller + "\",\"" + listAction + "\""+typeIDString+")' ><i class='icon-first-2'></i></a></li>");
                else
                    htmlString.Append("<li class='first disabled'><a><i class='icon-first-2'></i></a></li>");

                if ((paginate.PageIndex-1) > 1)
                    htmlString.Append("<li class='prev'><a href='javascript:boGotoPage(\"" + (paginate.PageIndex - 1) + "\",\"" + controller + "\",\"" + listAction + "\"" + typeIDString + ")'><i class='icon-previous'></i></a></li>");
                else
                    htmlString.Append("<li class='prev disabled'><a><i class='icon-previous'></i></a></li>");

                for (int i = startPage; i < endPage+1; i++)
                {
                    if(i == paginate.PageIndex)
                        htmlString.Append("<li class='active'><a>" + i + "</a></li>");
                    else
                        htmlString.Append("<li><a href='javascript:boGotoPage(\"" + i + "\",\"" + controller + "\",\"" + listAction + "\"" + typeIDString + ")' >" + i + "</a></li>");
                }

                if(endPage<paginate.TotalPageCount)
                    htmlString.Append("<li class='spaces'><a>...</a></li>");

                if ((paginate.PageIndex+1) < paginate.TotalPageCount)
                    htmlString.Append("<li class='next'><a href='javascript:boGotoPage(\"" + (paginate.PageIndex + 1) + "\",\"" + controller + "\",\"" + listAction + "\"" + typeIDString + ")'><i class='icon-next'></i></a></li>");
                else
                    htmlString.Append("<li class='next disabled'><a><i class='icon-next'></i></a></li>");

                if(paginate.PageIndex < paginate.TotalPageCount)
                    htmlString.Append("<li class='last'><a href='javascript:boGotoPage(\"" + paginate.TotalPageCount + "\",\"" + controller + "\",\"" + listAction + "\"" + typeIDString + ")'><i class='icon-last-2'></i></a></li>");
                else
                    htmlString.Append("<li class='last disabled'><a><i class='icon-last-2'></i></a></li>");

                htmlString.Append("</ul></div>");
                htmlString.Append("</td>");
                htmlString.Append("<td style='text-align:right'>");
                htmlString.Append("第 " + paginate.PageIndex + " 页  每页 " + paginate.PageSize + " 条，共 " + paginate.TotalPageCount + " 页，数据总数：" + paginate.TotalCount + "条 ");
                htmlString.Append("</td>");
                htmlString.Append("</tr></table>");
                // htmlString.Append("</div>");

                bottomBar.InnerHtmlContent = htmlString.ToString();
                return bottomBar;
            }

        }

        /// <summary>
        /// 定义缺省模型下的 javascript 脚本
        /// </summary>
        class AdditionScriptContent
        {
            public static string Get() 
            {
                var htmlString = new StringBuilder();

                htmlString.Append("<script>");

                htmlString.Append(_GetSearch());
                htmlString.Append(_GetNavigator());
                htmlString.Append(_GetCreateOrEdit());
                htmlString.Append(_GetGotoPage());
                htmlString.Append(_GetGotoPageFromExtenal());
                htmlString.Append(_GetDetail());
                htmlString.Append(_GetDialog());
                htmlString.Append(_RedirectPage());

                htmlString.Append(_GetValidator());
                htmlString.Append(_GetDelete());
                htmlString.Append(_GetAdditionColOperationByPageFunction());
                htmlString.Append(_GetLeftNavigatorRefreshFunction());

                htmlString.Append("</script>");



                return htmlString.ToString();
            }

            private static string _GetSearch() 
            {
                var boType = typeof(T);
                var headerDivID = "Header_" + boType.Name;
                var dgvDivID = "DataGridView_" + boType.Name;
                var bottomDivID = "Bottom_" + boType.Name;

                var htmlString = new StringBuilder();

                htmlString.Append("function boSearch(controllerName,searchAction){");

                htmlString.Append("var keyword = document.getElementById('serchKeyword').value;");
                htmlString.Append("$.ajax({");
                htmlString.Append("cache: true,");
                htmlString.Append("type: 'POST',");
                htmlString.Append("async: true,");
                htmlString.Append("url: '../../' + controllerName + '/' + searchAction + '?keyword=' + escape(keyword),");
                htmlString.Append("dataType: 'json',");
                htmlString.Append("success: function (data) {");
                htmlString.Append("$('#" + headerDivID + "').html(data.Header);");
                htmlString.Append("$('#" + dgvDivID + "').html(data.DataGridView);");
                htmlString.Append("$('#" + bottomDivID + "').html(data.Bottom);");
                htmlString.Append("}});");

                htmlString.Append("}");

                return htmlString.ToString();
            }

            private static string _GetNavigator()
            {
                var boType = typeof(T);
                var headerDivID = "Header_" + boType.Name;
                var dgvDivID = "DataGridView_" + boType.Name;
                var bottomDivID = "Bottom_" + boType.Name;

                var htmlString = new StringBuilder();

                htmlString.Append("function boNavigator(typeID, controllerName,searchAction){");

                htmlString.Append("$.ajax({");
                htmlString.Append("cache: true,");
                htmlString.Append("type: 'POST',");
                htmlString.Append("async: true,");
                htmlString.Append("url: '../../' + controllerName + '/' + searchAction + '?typeID=' + typeID,");
                htmlString.Append("dataType: 'json',");
                htmlString.Append("success: function (data) {");
                htmlString.Append("$('#" + headerDivID + "').html(data.Header);");
                htmlString.Append("$('#" + dgvDivID + "').html(data.DataGridView);");
                htmlString.Append("$('#" + bottomDivID + "').html(data.Bottom);");
                htmlString.Append("}});");

                htmlString.Append("}");

                return htmlString.ToString();
            }

            private static string _GetCreateOrEdit() 
            {
                var boType = typeof(T);
                Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
                var editorSpecification = boVMAttributes.Where(n => n.GetType().Name == "EditorSpecification").FirstOrDefault() as EditorSpecification;
                var useDialog = editorSpecification.UseDialogue;

                var htmlString = new StringBuilder();

                htmlString.Append("function boCreateOrEdit(id,controllerName,createOrEditAction){");
                htmlString.Append("$.ajax({");
                htmlString.Append("cache: true,");
                htmlString.Append("type: 'POST',");
                htmlString.Append("async: true,");
                htmlString.Append("url: '../../' + controllerName + '/' + createOrEditAction + '?id=' + id,");
                htmlString.Append("dataType: 'json',");
                htmlString.Append("success: function (data) {");
                if (useDialog)
                    htmlString.Append("boCreateOrEditDialog(data.IconName,data.CaptionName,data.Width,data.Height,data.InnerHtmlContent);");
                else
                    htmlString.Append("document.getElementById('divBoMainArea').innerHTML = data.InnerHtmlContent;");
                htmlString.Append("}});");
                htmlString.Append("}");

                return htmlString.ToString();
            }

            private static string _GetDetail() 
            {
                var htmlString = new StringBuilder();

                htmlString.Append("function boDetail(id,controllerName,detailAction){");
                htmlString.Append("$.ajax({");
                htmlString.Append("cache: true,");
                htmlString.Append("type: 'POST',");
                htmlString.Append("async: true,");
                htmlString.Append("url: '../../' + controllerName + '/' + detailAction + '?id=' + id,");
                htmlString.Append("dataType: 'json',");
                htmlString.Append("success: function (data) {");
                htmlString.Append("boCreateOrEditDialog(data.IconName,data.CaptionName,data.Width,data.Height,data.InnerHtmlContent);");
                htmlString.Append("}});");
                htmlString.Append("}");

                return htmlString.ToString();
            }

            private static string _GetDelete()//string id, string boName, string controllername, string deleteName
            {
                var htmlString = new StringBuilder();

                htmlString.Append(_GetDeleteDialog());//id,boName,controllername,deleteName

                return htmlString.ToString();
            }

            /// <summary>
            /// 直接通过重新定位页面
            /// </summary>
            /// <returns></returns>
            private static string _RedirectPage()
            {

                var htmlString = new StringBuilder();

                htmlString.Append("function GotoPage(url){");
                htmlString.Append("alert(url);");
                htmlString.Append(" window.location.href = url;");
                htmlString.Append("}");
                return htmlString.ToString();
            }

            private static string _GetGotoPage()
            {
                var boType = typeof(T);
                var headerDivID = "Header_" + boType.Name;
                var dgvDivID = "DataGridView_" + boType.Name;
                var bottomDivID = "Bottom_" + boType.Name;

                var htmlString = new StringBuilder();

                htmlString.Append("function boGotoPage(page,controllerName,listAction,typeID){");
                htmlString.Append("$.ajax({");
                htmlString.Append("cache: true,");
                htmlString.Append("type: 'POST',");
                htmlString.Append("async: true,");
                htmlString.Append("url: '../../' + controllerName + '/' + listAction + '?page=' + page+'&typeID='+typeID,");
                htmlString.Append("dataType: 'json',");
                htmlString.Append("success: function (data) {");
                htmlString.Append("$('#" + headerDivID + "').html(data.Header);");
                htmlString.Append("$('#" + dgvDivID + "').html(data.DataGridView);");
                htmlString.Append("$('#" + bottomDivID + "').html(data.Bottom);");
                htmlString.Append("}});");

                htmlString.Append("}");
                //htmlString.Append("}");

                return htmlString.ToString();
            }

            private static string _GetGotoPageFromExtenal()
            {
                var htmlString = new StringBuilder();

                htmlString.Append("function boGotoPageFromExtenal(page,controllerName,listAction){");
                htmlString.Append("$.ajax({");
                htmlString.Append("cache: true,");
                htmlString.Append("type: 'POST',");
                htmlString.Append("async: true,");
                htmlString.Append("url: '../../' + controllerName + '/' + listAction + '?page=' + page,");
                htmlString.Append("dataType: 'json',");
                htmlString.Append("success: function (data) {");
                htmlString.Append("$('#divBoMainArea').html(data.Header+data.DataGridView+data.Bottom);");

                htmlString.Append("}});");

                htmlString.Append("}");

                return htmlString.ToString();
            }

            private static string _GetValidator() 
            {
                var htmlString = new StringBuilder();

                htmlString.Append("function initialValidateStatus(div0,div1,div2,normalCss){");
                htmlString.Append("document.getElementById(div0).className=normalCss;");
                htmlString.Append("document.getElementById(div1).innerHTML = '';");
                htmlString.Append("document.getElementById(div2).innerHTML = '<i class=\"icon-pencil\"></i>';");
                htmlString.Append("}");

                return htmlString.ToString();

            }

            private static string _GetDialog() 
            {

                var htmlString = new StringBuilder();

                htmlString.Append("function boCreateOrEditDialog(tIcon,tTitle,tWidth,tHeight,contentString){");
                htmlString.Append(_GetDialogInitialContent());
                htmlString.Append("}");
                return htmlString.ToString();

            }

            private static string _GetDialogInitialContent() 
            {
                var htmlString = new StringBuilder();

                htmlString.Append("$.Dialog({");
                htmlString.Append("flat: false,");
                htmlString.Append("overlay: true,");
                htmlString.Append("shadow: true,");
                htmlString.Append("overlay: true,");
                htmlString.Append("draggable: true,");
                htmlString.Append("overlayClickClose: false,");
                htmlString.Append("icon: '<span class=\"'+tIcon+'\"></span>',");
                htmlString.Append("title: '数据处理窗口',");
                htmlString.Append("padding: 10,");
                htmlString.Append("height: tHeight,");
                htmlString.Append("width: tWidth,");
                htmlString.Append("onShow: function (_dialog) {");
                htmlString.Append("$.Dialog.title(tTitle);");
                htmlString.Append("$.Dialog.content(contentString);");
                htmlString.Append("$.Metro.initInputs();");
                htmlString.Append("}");
                htmlString.Append("})");

                return htmlString.ToString();
            }

            private static string _GetDeleteDialog() // string id,string boName,string controllername, string deleteName 
            {

                var htmlString = new StringBuilder();

                htmlString.Append("function boDelete(tIcon,tTitle,tWidth,tHeight,id,boName,controllerName,deletePath){");

                htmlString.Append("var contentString=\"");
                htmlString.Append("<table><tr><td style='vertical-align:top'><i class='icon-info-2 on-left' style='background: red;color: white;padding: 10px;border-radius:25%'></i></td><td><p>您的操作将永久性删除以下数据，请确认是否继续？</p>");
                htmlString.Append("<p>数据：\"+boName+ \"</p></td></tr></table>");//boName
                htmlString.Append("<button class='button primary' style='height:30px' onclick='excutedDelete(\\\"\"+id+\"\\\",\\\"\"+controllerName+\"\\\",\\\"\"+deletePath+\"\\\")'> 确 定 </button> ");
                htmlString.Append("<button class='button' type='button' onclick='$.Dialog.close()' style='height:30px'> 取 消 </button>");
                htmlString.Append("<div id='delete_Status'></div>");
                htmlString.Append("\";");

                htmlString.Append("$.Dialog({");
                htmlString.Append("flat: false,");
                htmlString.Append("overlay: true,");
                htmlString.Append("shadow: true,");
                htmlString.Append("icon: '<span class=\"'+tIcon+'\"></span>',");
                htmlString.Append("title: '数据删除窗口',");
                htmlString.Append("padding: 10,");
                htmlString.Append("height:tHeight,");
                htmlString.Append("width:tWidth,");
                htmlString.Append("onShow: function (_dialog) {");
                htmlString.Append("$.Dialog.title(tTitle);");
                htmlString.Append("$.Dialog.content(contentString);");
                htmlString.Append("$.Metro.initInputs();");
                htmlString.Append("}");
                htmlString.Append("})");
                htmlString.Append("}");

                htmlString.Append("function excutedDelete(id,controllerName,deletePath){");//
                htmlString.Append("var OperationStatus  = false;");
                htmlString.Append("var OperationMessage = '';");
                htmlString.Append("$.ajax( {");
                htmlString.Append("cache:true,");
                htmlString.Append("type:'POST',");
                htmlString.Append("async:true,");
                htmlString.Append("url:'../../'+controllerName+'/'+deletePath+'/'+id,");
                htmlString.Append("dataType:'json',");

                htmlString.Append("success: function (DeleteActionStatus) {");

                htmlString.Append("if (!DeleteActionStatus.IsOK) {");
                htmlString.Append("document.getElementById('delete_Status').innerHTML = '<p style=\"color:red\">删除错误信息：'+DeleteActionStatus.ErrorMassage+'</p>';");
                htmlString.Append("}else {");
                htmlString.Append("$.Dialog.close();");
                htmlString.Append("boGotoPage(DeleteActionStatus.PageIndex,controllerName,'List',DeleteActionStatus.TypeID);");
                htmlString.Append("if(DeleteActionStatus.ExtenssionFunctionString!=''){");
                htmlString.Append("refreshDepartmentTreeView(controllerName,DeleteActionStatus.ExtenssionFunctionString);");
                htmlString.Append("}");

                htmlString.Append("}");
                htmlString.Append("}");

                htmlString.Append("});");

                htmlString.Append("}");

                return htmlString.ToString();

            }

            private static string _GetDeleteDialogInitialContent() 
            {
                var htmlString = new StringBuilder();

                htmlString.Append("$.Dialog({");
                htmlString.Append("flat: false,");
                htmlString.Append("overlay: true,");
                htmlString.Append("shadow: true,");
                htmlString.Append("icon: '<span class=\"'+tIcon+'\"></span>',");
                htmlString.Append("title: '数据删除窗口',");
                htmlString.Append("padding: 10,");
                htmlString.Append("height: tHeight,");
                htmlString.Append("width: tWidth,");
                htmlString.Append("onShow: function (_dialog) {");
                htmlString.Append("$.Dialog.title(tTitle);");
                htmlString.Append("$.Dialog.content(contentString);");
                htmlString.Append("$.Metro.initInputs();");
                htmlString.Append("}");
                htmlString.Append("})");

                return htmlString.ToString();
            }

            private static string _GetAjaxFormFunction() 
            {
                var boType = typeof(T);
                Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
                var editorSpecification = boVMAttributes.Where(n => n.GetType().Name == "EditorSpecification").FirstOrDefault() as EditorSpecification;

                var htmlString = new StringBuilder();
                //htmlString.Append("<script>");
                htmlString.Append("var options = {");
                htmlString.Append("dataType: 'json',");
                htmlString.Append("success: function (data) {");
                htmlString.Append("document.getElementById('Editor_" + editorSpecification.ControllerName + "').innerHTML = data;");
                htmlString.Append("}");
                htmlString.Append("};");
                htmlString.Append("$('#EditorForm_" + editorSpecification.ControllerName + "').ajaxForm(options);");
                //htmlString.Append("</script>");

                return htmlString.ToString();
            }

            private static string _GetAdditionColOperationByPageFunction() 
            {
                var htmlString = new StringBuilder();

                htmlString.Append("function gotoAddtionColPage(id,controllerName,additionAction){");
                htmlString.Append("$.ajax({");
                htmlString.Append("type: 'POST',");
                htmlString.Append("url: '../../' + controllerName + '/' + additionAction + '?id=' + id,");
                htmlString.Append("success: function (data) {");
                htmlString.Append("document.getElementById('divBoMainArea').innerHTML = data;");
                htmlString.Append("}});");
                htmlString.Append("}");

                return htmlString.ToString();

            }

            private static string _GetLeftNavigatorRefreshFunction() 
            {
                var htmlString = new StringBuilder();
                htmlString.Append("function refreshDepartmentTreeView(controllerName,refreshAction){");
                htmlString.Append("$.ajax({");
                htmlString.Append("cache: true,");
                htmlString.Append("type: 'POST',");
                htmlString.Append("async: true,");
                htmlString.Append("url: '../../' + controllerName + '/' + refreshAction,");
                htmlString.Append("dataType: 'json',");
                htmlString.Append("success: function (data) {");
                htmlString.Append("document.getElementById('divLeftNavigator').innerHTML = data;");
                htmlString.Append("}});");
                htmlString.Append("}");
                return htmlString.ToString();

            }
        }

        /// <summary>
        /// 表头参数
        /// </summary>
        class ListColumnHeader 
        {
            public string Title { get; set; }
            public string Width { get; set; }
            public string OrderSort { get; set; }
            public bool UseSortIndicator { get; set; }
            public string PropertyName { get; set; }

        }
    }

    public class ListPartialPageUpdateInfo 
    {
        public string Header { get; set; }
        public string DataGridView { get; set; }
        public string Bottom { get; set; }

    }
}
