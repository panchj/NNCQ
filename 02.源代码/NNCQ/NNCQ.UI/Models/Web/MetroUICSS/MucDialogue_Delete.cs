using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models.Web.MetroUICSS
{
    public class MucDialogue_Delete:IViewComponentModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CssClass { get; set; }
        public string CssStyle { get; set; }

        public string CaptionName { get; set; }  // 对话框标题
        public string IconName { get; set; }     // 对话框图标
        public int Width { get; set; }
        public int Height { get; set; }

        public string InnerHtmlContent { get; set; }

        public MucDialogue_Delete(string id,string boName,string controllername, string deleteName) 
        {

            this.CaptionName = "数据删除："+boName;
            this.IconName = "icon-remove";
            this.Width = 500;
            this.Height = 300;
            //var htmlString = new StringBuilder();

            //htmlString.Append("<i class=' icon-info-2 on-right on-left' style='background: red;color: white;padding: 10px; border-radius: 50%'>");
            //htmlString.Append("提醒：你现在选的操作将删除以下数据，删除操作后数据将永久性清除，请您确认是否继续？");
            //htmlString.Append("<div>数据：</div>");

            //htmlString.Append("");
            //htmlString.Append("<button class='button primary' style='height:30px' onclick='javascript:excutedDelete(\"" + id + "\",\"" + controllername + "\",\"" + deleteName + "\")'> 确 定 </button> ");
            //htmlString.Append("<button class='button' type='button' onclick='$.Dialog.close()'  style='height:30px'> 取 消 </button>");
            //htmlString.Append("<div id='delete_"+id+"'></div>");

            //htmlString.Append("function excutedDelete(id,controllerName,deleteAction){");
            //htmlString.Append("$.ajax({");
            //htmlString.Append("cache: true,");
            //htmlString.Append("type: 'POST',");
            //htmlString.Append("async: true,");
            //htmlString.Append("url: '../../' + controllerName + '/' + detailAction + '?id=' + id,");
            //htmlString.Append("dataType: 'json',");
            //htmlString.Append("success: function (data) {");
            //htmlString.Append("");
            //htmlString.Append("}});");
            //htmlString.Append("}");

            //this.InnerHtmlContent = htmlString.ToString();

        }

    }
}
