using NNCQ.UI.Models.Web.MetroUICSS.SubControlModels;
using NNCQ.UI.ViewModelAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models.Web.MetroUICSS
{
    /// <summary>
    /// 定义通用的页面组件获取方式，在本架构中，这部分的用途是基于 ViewModel 来实现的。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommonSpecificationValue<T> : ISpecificationValue<T> where T : class ,new()
    {
        //public CommonSpecificationValue() { }

        //Muc_MainWorkPlaceHeadBar BoListHead(T boVM);

        public Muc_MainWorkPlaceHeadBar BoListHead() 
        {
            var head = new Muc_MainWorkPlaceHeadBar();
            var headTitle = new HeadBarTitle();
            var headOperation = new HeadBarOperation();
            head.HeadBarTitle = headTitle;
            head.HeadBarOperation = headOperation;

            var boType = typeof(T);

            // 提取为 T 定义的约束特性集合
            Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
            var headAttribute = boVMAttributes.Where(n => n.GetType().Name == "ListHeadSpecification").FirstOrDefault();
            if (headAttribute != null) 
            {
                var ha = headAttribute as ListHeadSpecification;

                headTitle.ID            = "HeadTitle_" + boType.Name;
                headTitle.Name          = "HeadTiTle_" + boType.Name;
                headTitle.Title         = ha.Title;
                headTitle.HasBackAction = false;

                headOperation.ID               = "HeadOperation_" + boType.Name;
                headOperation.Name             = "HeadOperation_" + boType.Name;
                headOperation.SearchActionPath = ha.SearchActionPath;
                headOperation.CreateActionPath = ha.CreateActionPath;

                head.ID   = "Head_" + boType.Name;
                head.Name = "Head_" + boType.Name;
            }

            // PropertyInfo[] p = boType.GetProperties();

            return head;
        }

        public Muc_MainWorkPlaceDataGridView BoListDataGridView() 
        {
            var dgv = new Muc_MainWorkPlaceDataGridView();
            var boType = typeof(T);

            // 提取为 T 定义的约束特性集合
            Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
            var dgvAttribute = boVMAttributes.Where(n => n.GetType().Name == "ListDataGridViewSpecification").FirstOrDefault();
            if (dgvAttribute != null) 
            {
                var dgvAttr = dgvAttribute as ListDataGridViewSpecification;

                dgv.ID               = "DataGridView_" + boType.Name;
                dgv.Name             = "DataGridView_" + boType.Name;
                dgv.rows             = dgvAttr.Rows;
                dgv.EditActionPath   = dgvAttr.EditActionPath;
                dgv.DetailActionPath = dgv.DetailActionPath;
                dgv.DeleteActionPath = dgv.DeleteActionPath;

            }

            // 提取列表的表格规格数据
            // PropertyInfo[] p = boType.GetProperties();

            return dgv;
        }
    }
}
