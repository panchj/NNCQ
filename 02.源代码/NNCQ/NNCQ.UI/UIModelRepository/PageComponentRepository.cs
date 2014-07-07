﻿using NNCQ.UI.Models;
using NNCQ.UI.Models.Web.MetroUICSS;
using NNCQ.UI.ViewModelAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace NNCQ.UI.UIModelRepository
{
    public class PageComponentRepository<T> where T : class
    {

        public static MucDialogue_CreateOrEdit CreateOrEditDialog(T boVM, bool isNew) 
        {
            var preString = "编辑：";
            var preIcon = "icon-pencil";

            if (isNew) 
            {
                preString = "新建：";
                preIcon = "icon-new";
            }

            var boType = typeof(T);
            Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
            var editorSpecification = boVMAttributes.Where(n => n.GetType().Name == "EditorSpecification").FirstOrDefault() as EditorSpecification;

            var dialog = new MucDialogue_CreateOrEdit();
            dialog.CaptionName = preString + editorSpecification.Title;
            dialog.IconName = preIcon;
            dialog.Width = 500;
            dialog.Height = 300;

            var divName = boType.Name + editorSpecification.ControllerName;

            var htmlString = new StringBuilder();

            htmlString.Append("<script>");
            htmlString.Append("var options" + boType.Name + " = {");
            htmlString.Append("dataType: 'json',");
            htmlString.Append("success: function (data) {");
            htmlString.Append("if(data=='ok'){");
            htmlString.Append("$.Dialog.close();");
            htmlString.Append("boGotoPage(1,'" + editorSpecification.ControllerName + "','" + editorSpecification.ListString + "');");
            htmlString.Append("}else{");
            htmlString.Append("document.getElementById('Editor_" + divName + "').innerHTML = data;");
            htmlString.Append("}}");
            htmlString.Append("};");
            htmlString.Append("$('#EditorForm_" + divName + "').ajaxForm(options" + boType.Name + ");");
            htmlString.Append("</script>");

            htmlString.Append("<form action='" + editorSpecification.ControllerName + "/" + editorSpecification.SaveAction + "' method='post' id='EditorForm_" + divName + "' >");// data-ajax-update='#Editor_" + editorAttribute.ControllerName + "' data-ajax-mode='replace' data-ajax='true'>"); 

            htmlString.Append("<div id='Editor_" + divName + "' name='Editor_" + divName + "'>");
            htmlString.Append(_GetEditor(boVM));
            htmlString.Append("</div>");
            
            htmlString.Append("<div class='form-actions' style='text-align:right'><br/>");
            htmlString.Append("<input  type='submit' class='button primary' style='height:30px' value='确定' />  ");
            htmlString.Append("<button class='button' type='button' onclick='$.Dialog.close()'  style='height:30px'>取消</button>");
            htmlString.Append("</div>");
            htmlString.Append("</form>");



            dialog.InnerHtmlContent = htmlString.ToString();

            return dialog;
        }

        public static MucDialogue_CreateOrEdit CreateOrEditPage(T boVM, bool isNew)
        {
            var preString = "编辑：";
            var preIcon = "icon-pencil";

            if (isNew)
            {
                preString = "新建：";
                preIcon = "icon-new";
            }

            var boType = typeof(T);

            var boName = typeof(T).Name;

            Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
            var editorSpecification = boVMAttributes.Where(n => n.GetType().Name == "EditorSpecification").FirstOrDefault() as EditorSpecification;

            var dialog = new MucDialogue_CreateOrEdit();
            dialog.CaptionName = preString + editorSpecification.Title;
            dialog.IconName = preIcon;
            dialog.Width = 500;
            dialog.Height = 300;

            var htmlString = new StringBuilder();
            htmlString.Append("<p class='subheader'>" + preString + editorSpecification.Title + "</p>");

            htmlString.Append("<script>");
            htmlString.Append("var options = {");
            htmlString.Append("dataType: 'json',");
            htmlString.Append("success: function (data) {");
            htmlString.Append("if(data=='ok'){");
            //htmlString.Append("$.Dialog.close();");
            htmlString.Append("boGotoPage(1,'" + editorSpecification.ControllerName + "','" + editorSpecification.ListString + "');");
            htmlString.Append("}else{");
            htmlString.Append("document.getElementById('Editor_" + editorSpecification.ControllerName + "').innerHTML = data;");
            htmlString.Append("}}");
            htmlString.Append("};");
            htmlString.Append("$('#EditorForm_" + editorSpecification.ControllerName + "').ajaxForm(options);");
            htmlString.Append("</script>");

            htmlString.Append("<form action='" + editorSpecification.ControllerName + "/" + editorSpecification.SaveAction + "' method='post' id='EditorForm_" + editorSpecification.ControllerName + "' >");// data-ajax-update='#Editor_" + editorAttribute.ControllerName + "' data-ajax-mode='replace' data-ajax='true'>"); 

            htmlString.Append("<div id='Editor_" + editorSpecification.ControllerName + "' name='Editor_" + editorSpecification.ControllerName + "'>");
            htmlString.Append(_GetEditor(boVM));
            htmlString.Append("</div>");

            htmlString.Append("<div class='form-actions' style='text-align:right'><br/>");
            htmlString.Append("<input  type='submit' class='button primary' style='height:30px' value='确定' />  ");
            //htmlString.Append("<button class='button' type='button' onclick='$.Dialog.close()'  style='height:30px'>取消</button>");
            htmlString.Append("</div>");
            htmlString.Append("</form>");



            dialog.InnerHtmlContent = htmlString.ToString();

            return dialog;
        }

        public static MucDialogue_CreateOrEdit UpdateCreateOrEditDialog(T boVM, bool isNew,List<ValidatorResult> vItems)
        {
            var preString = "编辑：";
            var preIcon = "icon-pencil";

            if (isNew)
            {
                preString = "新建：";
                preIcon = "icon-new";
            }

            var boType = typeof(T);
            Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
            var editorSpecification = boVMAttributes.Where(n => n.GetType().Name == "EditorSpecification").FirstOrDefault() as EditorSpecification;

            var dialog = new MucDialogue_CreateOrEdit();
            dialog.CaptionName = preString + editorSpecification.Title;
            dialog.IconName = preIcon;
            dialog.Width = 500;
            dialog.Height = 300;

            var htmlString = new StringBuilder();

            htmlString.Append(_GetEditor(boVM, vItems));

            dialog.InnerHtmlContent = htmlString.ToString();

            return dialog;
        }

        public static MucDialogue_CreateOrEdit UpdateCreateOrEditPage(T boVM, bool isNew, List<ValidatorResult> vItems)
        {
            var preString = "编辑：";
            var preIcon = "icon-pencil";

            if (isNew)
            {
                preString = "新建：";
                preIcon = "icon-new";
            }

            var boType = typeof(T);
            Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
            var editorSpecification = boVMAttributes.Where(n => n.GetType().Name == "EditorSpecification").FirstOrDefault() as EditorSpecification;

            var dialog = new MucDialogue_CreateOrEdit();
            dialog.CaptionName = preString + editorSpecification.Title;
            dialog.IconName = preIcon;
            dialog.Width = 500;
            dialog.Height = 300;

            var htmlString = new StringBuilder();

            htmlString.Append(_GetEditor(boVM, vItems));

            dialog.InnerHtmlContent = htmlString.ToString();

            return dialog;
        }

        public static MucDialogue_Detail DetailDialog(T boVM) 
        {
            var boType = typeof(T);
            Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
            var editorSpecification = boVMAttributes.Where(n => n.GetType().Name == "EditorSpecification").FirstOrDefault() as EditorSpecification;

            var dialog = new MucDialogue_Detail();
            dialog.CaptionName = "明细数据："+editorSpecification.Title;
            // dialog.IconName = preIcon;
            dialog.Width = 500;
            dialog.Height = 300;
            dialog.InnerHtmlContent=_GetDetail(boVM);
            return dialog;

        }

        public static string SaveOK() 
        {
            return "ok";

        }

        private static string _GetEditor(T boVM, List<ValidatorResult> vItems = null) 
        {
            var keyValue = "";
            var keyName = "";
            var editItems = new List<EditItemSpecification>();
            var boType = typeof(T);
            Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
            var editorAttribute = boVMAttributes.Where(n => n.GetType().Name == "EditorSpecification").FirstOrDefault() as EditorSpecification;

            PropertyInfo[] properties = boType.GetProperties();

            foreach (PropertyInfo pItem in properties) 
            {
                var keyAttibute = Attribute.GetCustomAttributes(pItem).Where(n => n.GetType().Name == "KeyAttribute").FirstOrDefault();
                if (keyAttibute != null)
                {
                    keyValue = pItem.GetValue(boVM).ToString();
                    keyName = pItem.Name;
                }

                var displayAttibute = Attribute.GetCustomAttributes(pItem).Where(n => n.GetType().Name == "DisplayAttribute").FirstOrDefault();
                if (displayAttibute != null)
                {
                    var editItem = new EditItemSpecification();
                    var displayItem = displayAttibute as System.ComponentModel.DataAnnotations.DisplayAttribute;
                    var displayName = displayItem.Name;

                    var editAtrribute = Attribute.GetCustomAttributes(pItem).Where(n => n.GetType().Name == "EditorItemSpecification").FirstOrDefault() as EditorItemSpecification;

                    if (editAtrribute != null)
                    {
                        var itemValue = pItem.GetValue(boVM);
                        editItem.FieldName = pItem.Name;
                        editItem.FieldDisplayName = displayName;
                        editItem.FieldEditContent = _GetEditFieldContent(editAtrribute.ItemType, itemValue, editItem.FieldName, boVM, vItems);
                        if (editAtrribute.ItemType == EditorItemType.Hidden) 
                        {
                            editItem.FieldDisplayName = "Hidden";
                        }
                        editItems.Add(editItem);
                    }

                }

            }
            var maxLength = (editItems.Max(x => x.FieldDisplayName.Length)+1)*16;
            var htmlString = new StringBuilder();

            htmlString.Append("<input type='hidden' id='"+keyName+"' name='"+keyName+"' value='"+keyValue+"' />");

            foreach (var item in editItems.Where(d => d.FieldDisplayName == "Hidden"))
                htmlString.Append(item.FieldEditContent);

            htmlString.Append("<table style='width:100%'>");
            foreach (var item in editItems.Where(d => d.FieldDisplayName != "Hidden")) //
            {
                htmlString.Append("<tr>");
                htmlString.Append("<td style='width:"+maxLength+"px;text-align:right;vertical-align:top'>" + item.FieldDisplayName + "：</td><td>" + item.FieldEditContent + "</td>");
                var statusString = "<i class='icon-pencil'></>";
                var errStyle = "";
                var errMessage = "";
                if (vItems != null) 
                {
                    var eItems = vItems.Where(n => n.Name == item.FieldName);
                    if (eItems.Count() > 0)
                    {
                        foreach (var eItem in eItems)
                        {
                            errStyle = "error-state";
                            errMessage = errMessage + eItem.ErrorMessage + "\n";
                        }
                    }

                    if (!String.IsNullOrEmpty(errMessage))
                    {
                        statusString = "<i class='icon-cancel-2 fg-red'></i>";
                    }
                    else 
                    {
                        statusString = "<i class='icon-checkmark fg-green'></i>";
                    }
                }
                htmlString.Append("<td style='width:10px;vertical-align:top'><div id='validStatus_"+item.FieldName+"'>"+statusString+"</div></td>");
                
                htmlString.Append("</tr>");
            }
            htmlString.Append("</table>");

            return htmlString.ToString();
        }

        private static string _GetEditFieldContent(EditorItemType itemType, object itemValue, string name, T boVM, List<ValidatorResult> vItems = null) 
        {
            var valueString = "";
            if (itemValue != null)
                valueString = itemValue.ToString();
            var errStyle="";
            var errMessage="";
            var errMessageStyle="";
            if (vItems != null) 
            {
                var eItems=vItems.Where(n => n.Name == name);
                if (eItems.Count() > 0) 
                {
                    foreach (var eItem in eItems) 
                    {
                        errStyle = "error-state";
                        errMessage = errMessage + eItem.ErrorMessage + "\n";
                    }
                } 
            }


            var initialStattus = "javascript:initialValidateStatus(\"css_"+name+"\",\"errorMeessage_" + name + "\",\"validStatus_" + name + "\",\"input-control text\")";
            var htmlString = new StringBuilder();
            switch (itemType)
            {
                case EditorItemType.TextBox:
                    htmlString.Append(EditorItem.A01_TextBox(name, errStyle, valueString, initialStattus, errMessage));
                    break;
                case EditorItemType.TextArea:
                    htmlString.Append(EditorItem.A02_TextArea(name, errStyle, valueString, initialStattus, errMessage, errMessageStyle));
                    break;
                case EditorItemType.DorpdownOptionWithSelfReferentialItem:
                    htmlString.Append(EditorItem.A03_DorpdownOptionWithSelfReferentialItem(boVM, name, errStyle, valueString, initialStattus, errMessage, errMessageStyle));
                    break;
                case EditorItemType.DorpdownOptionWithPlainFacadeItem:
                    htmlString.Append(EditorItem.A04_DorpdownOptionWithPlainFacadeItem(boVM, name, errStyle, valueString, initialStattus, errMessage, errMessageStyle));
                    break;
                case EditorItemType.DorpdownOptionWithEnum:
                    htmlString.Append(EditorItem.A04_DorpdownOptionWithPlainFacadeItem(boVM, name, errStyle, valueString, initialStattus, errMessage, errMessageStyle));
                   break;
                case EditorItemType.ComboBox:
                    htmlString.Append(EditorItem.A06_ComboBox(boVM, name, errStyle, valueString, initialStattus, errMessage, errMessageStyle));
                    break;
                case EditorItemType.CheckBox:
                    htmlString.Append(EditorItem.A07_CheckBox(boVM, name, errStyle, valueString, initialStattus, errMessage, errMessageStyle));
                    break;
                case EditorItemType.YesNo:
                    //htmlString.Append(EditorItem.A08_YesNo(boVM, name, errStyle, valueString, initialStattus, errMessage, errMessageStyle));
                    break;
                case EditorItemType.Date:
                    htmlString.Append(EditorItem.A05_Date(name, errStyle, valueString, initialStattus, errMessage));
                    break;
                case EditorItemType.Hidden:
                    htmlString.Append(EditorItem.A08_Hidden(name,valueString));
                    break;
                default:
                    htmlString.Append(EditorItem.A01_TextBox(name, errStyle, valueString, initialStattus, errMessage));
                    break;
            }
            return htmlString.ToString();
        }

        private static string _GetDetail(T boVM) 
        {
            var editItems = new List<EditItemSpecification>();

            var boType = typeof(T);
            Attribute[] boVMAttributes = Attribute.GetCustomAttributes(boType);
            var editorAttribute = boVMAttributes.Where(n => n.GetType().Name == "EditorSpecification").FirstOrDefault() as EditorSpecification;

            PropertyInfo[] properties = boType.GetProperties();

            foreach (PropertyInfo pItem in properties)
            {
                var displayAttibute = Attribute.GetCustomAttributes(pItem).Where(n => n.GetType().Name == "DisplayAttribute").FirstOrDefault();
                if (displayAttibute != null)
                {
                    var editItem = new EditItemSpecification();
                    var displayItem = displayAttibute as System.ComponentModel.DataAnnotations.DisplayAttribute;
                    var displayName = displayItem.Name;

                    var detailAtrribute = Attribute.GetCustomAttributes(pItem).Where(n => n.GetType().Name == "DetailItemSpecification").FirstOrDefault() as DetailItemSpecification;

                    if (detailAtrribute != null)
                    {
                        var itemValue = pItem.GetValue(boVM);
                        editItem.FieldName = pItem.Name;
                        editItem.FieldDisplayName = displayName;
                        editItem.FieldEditContent = _GetDetailFieldContent(detailAtrribute.ItemType, itemValue);

                        editItems.Add(editItem);
                    }

                }

            }
            var maxLength = (editItems.Max(x => x.FieldDisplayName.Length) + 1) * 16;
            var htmlString = new StringBuilder();
            htmlString.Append("<table style='width:100%'>");
            foreach (var item in editItems)
            {
                htmlString.Append("<tr>");
                htmlString.Append("<td style='width:" + maxLength + "px;text-align:right;vertical-align:top'>" + item.FieldDisplayName + "：</td><td>" + item.FieldEditContent + "</td>");
                htmlString.Append("</tr>");
            }
            htmlString.Append("</table>");


            return htmlString.ToString();

        }

        private static string _GetDetailFieldContent(EditorItemType itemType, object itemValue) 
        {
            var htmlString = new StringBuilder();
            switch (itemType)
            {
                case EditorItemType.TextBox:
                    htmlString.Append(itemValue.ToString());
                    break;
                case EditorItemType.TextArea:
                    htmlString.Append(itemValue.ToString());
                   break;
                case EditorItemType.DorpdownOptionWithSelfReferentialItem:
                    break;
                case EditorItemType.ComboBox:
                    break;
                case EditorItemType.YesNo:
                    break;
                case EditorItemType.Sex:
                    break;
                case EditorItemType.Date:
                    break;
                case EditorItemType.DateTime:
                    break;
                case EditorItemType.Time:
                    break;
                case EditorItemType.SelfReferentialItem:
                    var sItem = itemValue as SelfReferentialItem;
                    htmlString.Append(sItem.ItemName);
                    break;
                case EditorItemType.PlainFacadeItem:
                    break;
                case EditorItemType.Hidden:
                    break;
                default:
                    break;
            }

            return htmlString.ToString();
        }
        
        class EditItemSpecification 
        {
            public string FieldName { get; set; }
            public string FieldDisplayName { get; set; }
            public string FieldEditContent { get; set; }
        }

        class EditorItem
        {
            public static string A01_TextBox(string name, string errStyle, string valueString, string initialStattus, string errMessage) 
            {
                var htmlString = new StringBuilder();
                htmlString.Append("<div id='css_" + name + "' class='input-control text " + errStyle + "' data-role='input-control'>");
                htmlString.Append("<input id='" + name + "' name='" + name + "' type='text' placeholder='' value='" + valueString + "'  onchange='" + initialStattus + "' />");
                htmlString.Append("<button class='btn-clear' tabindex='-1'></button>");
                htmlString.Append("<div id='errorMeessage_" + name + "'><small class='fg-red'>" + errMessage + "</small></div>");
                htmlString.Append("</div>");
                return htmlString.ToString();

            }

            public static string A02_TextArea(string name, string errStyle, string valueString, string initialStattus, string errMessage, string errMessageStyle)
            {
                var htmlString = new StringBuilder();
                initialStattus = "javascript:initialValidateStatus(\"css_" + name + "\",\"errorMeessage_" + name + "\",\"validStatus_" + name + "\",\"input-control textarea\")";
                htmlString.Append("<div id='css_" + name + "' class='input-control textarea " + errStyle + "' data-role='input-control' " + errMessageStyle + " >");
                htmlString.Append("<textarea id='" + name + "' name='" + name + "' style='font-family:\"Microsoft YaHei UI\"'   onchange='" + initialStattus + "'>" + valueString + "</textarea>");
                htmlString.Append("<div id='errorMeessage_" + name + "'><small class='fg-red'>" + errMessage + "</small></div>");
                htmlString.Append("</div>");
                return htmlString.ToString();

            }

            public static string A03_DorpdownOptionWithSelfReferentialItem(T boVM, string name, string errStyle, string valueString, string initialStattus, string errMessage, string errMessageStyle)
            {
                var htmlString = new StringBuilder();
                initialStattus = "javascript:initialValidateStatus(\"css_" + name + "\",\"errorMeessage_" + name + "\",\"validStatus_" + name + "\",\"input-control select\")";
                var sItemCollection = new List<SelfReferentialItem>();
                PropertyInfo[] properties = typeof(T).GetProperties();
                foreach (var pItem in properties)
                {
                    var pAtrribute = Attribute.GetCustomAttributes(pItem).Where(n => n.GetType().Name == "SelfReferentialItemSpecification");
                    foreach (var aItem in pAtrribute)
                    {
                        var sPropertyAttribute = aItem as SelfReferentialItemSpecification;
                        if (sPropertyAttribute.RelevanceID == name)
                        {
                            sItemCollection = pItem.GetValue(boVM) as List<SelfReferentialItem>;
                        }
                    }
                }
                htmlString.Append("<div id='css_" + name + "' class='input-control select " + errStyle + "' " + errMessageStyle + " >");
                htmlString.Append("<select name='" + name + "'  onchange='" + initialStattus + "'>");
                htmlString.Append("<option value=''></option>");
                foreach (var item in sItemCollection)
                {
                    var selected = "";
                    if (item.ID == valueString)
                        selected = "selected";
                    htmlString.Append("<option " + selected + " value='" + item.ID + "'>" + item.ItemName + "</option>");
                }
                htmlString.Append("</select>");
                htmlString.Append("<div id='errorMeessage_" + name + "'><small class='fg-red'>" + errMessage + "</small></div>");
                htmlString.Append("</div>");
                return htmlString.ToString();

            }

            public static string A04_DorpdownOptionWithPlainFacadeItem(T boVM, string name, string errStyle, string valueString, string initialStattus, string errMessage, string errMessageStyle) 
            {
                var htmlString = new StringBuilder();
                initialStattus = "javascript:initialValidateStatus(\"css_" + name + "\",\"errorMeessage_" + name + "\",\"validStatus_" + name + "\",\"input-control select\")";
                var sItemCollection = new List<PlainFacadeItem>();
                PropertyInfo[] properties = typeof(T).GetProperties();
                foreach (var pItem in properties)
                {
                    var pAtrribute = Attribute.GetCustomAttributes(pItem).Where(n => n.GetType().Name == "PlainFacadeItemSpecification");
                    foreach (var aItem in pAtrribute)
                    {
                        var sPropertyAttribute = aItem as PlainFacadeItemSpecification;
                        if (sPropertyAttribute.RelevanceID == name)
                        {
                            sItemCollection = pItem.GetValue(boVM) as List<PlainFacadeItem>;
                        }
                    }
                }
                htmlString.Append("<div id='css_" + name + "' class='input-control select " + errStyle + "' " + errMessageStyle + " >");
                htmlString.Append("<select name='" + name + "'  onchange='" + initialStattus + "'>");
                htmlString.Append("<option value=''></option>");
                foreach (var item in sItemCollection)
                {
                    var selected = "";
                    if (item.ID == valueString)
                        selected = "selected";
                    htmlString.Append("<option " + selected + " value='" + item.ID + "'>" + item.Name + "</option>");
                }
                htmlString.Append("</select>");
                htmlString.Append("<div id='errorMeessage_" + name + "'><small class='fg-red'>" + errMessage + "</small></div>");
                htmlString.Append("</div>");
                return htmlString.ToString();
            }

            public static string A05_Date(string name, string errStyle, string valueString, string initialStattus, string errMessage)
            {
                var htmlString = new StringBuilder();
                //htmlString.Append("<div id='css_" + name + "' class='input-control text " + errStyle + "' ");// 
                //htmlString.Append(" data-role='datepicker' ");
                //htmlString.Append(" data-date='2014-01-01' ");
                //htmlString.Append(" data-format='yyyy年yy月dd日' ");
                //htmlString.Append(" data-effect='slide' ");
                //htmlString.Append(" data-week-start='1' ");
                //htmlString.Append(" data-other-days='1' ");
                //htmlString.Append(">");
                //htmlString.Append("<input id='" + name + "' name='" + name + "' type='text' />");// value='" + valueString + "'   onchange='" + initialStattus + "'
                //htmlString.Append("<button class='btn-date'></button>"); //tabindex='-1'
                ////htmlString.Append("<div id='errorMeessage_" + name + "'><small class='fg-red'>" + errMessage + "</small></div>");
                //htmlString.Append("</div>");
                //htmlString.Append("<div class='input-control text' data-role='datepicker' data-week-start='1'><input type='text'><button class='btn btn-date'></button></div>");
                return htmlString.ToString();

            }

            public static string A06_ComboBox(T boVM, string name, string errStyle, string valueString, string initialStattus, string errMessage, string errMessageStyle) 
            {
                var htmlString = new StringBuilder();
                initialStattus = "javascript:initialValidateStatus(\"css_" + name + "\",\"errorMeessage_" + name + "\",\"validStatus_" + name + "\",\"input-control select\")";
                var sItemCollection = new List<PlainFacadeItem>();
                PropertyInfo[] properties = typeof(T).GetProperties();
                foreach (var pItem in properties)
                {
                    var pAtrribute = Attribute.GetCustomAttributes(pItem).Where(n => n.GetType().Name == "PlainFacadeItemSpecification");
                    foreach (var aItem in pAtrribute)
                    {
                        var sPropertyAttribute = aItem as PlainFacadeItemSpecification;
                        if (sPropertyAttribute.RelevanceID == name)
                        {
                            sItemCollection = pItem.GetValue(boVM) as List<PlainFacadeItem>;
                        }
                    }
                }

                foreach (var item in sItemCollection)
                {
                    var selected = "";
                    if (item.ID.ToLower() == valueString.ToLower())
                        selected = "checked";

                    htmlString.Append("<div class='input-control radio' data-role='input-control' style='margin-right:10px;margin-top:-2px'>");
                    htmlString.Append("<label><input type='radio' value='"+item.ID+"' name='"+name+"' "+selected+" /><span class='check'></span>"+item.Name+"</label></div>");
                }
                htmlString.Append("<div id='errorMeessage_" + name + "'><small class='fg-red'>" + errMessage + "</small></div>");
                //htmlString.Append("<div class='input-control checkbox margin10' data-role='input-control'><label>Check me out<input type='checkbox' disabled checked /><span class='check'></span></label></div>");
                //htmlString.Append("<div class='input-control radio margin10' data-role='input-control'><label><input type='radio' name='r1' checked /><span class='check'></span>Check me out</label></div>");
                //htmlString.Append("<div class='input-control text' data-role='datepicker' data-date='2013-11-13' data-effect='slide' data-other-days='1'><input type='text'><button class='btn-date'></button></div>");
                return htmlString.ToString();

            }

            public static string A07_CheckBox(T boVM, string name, string errStyle, string valueString, string initialStattus, string errMessage, string errMessageStyle)
            {
                var htmlString = new StringBuilder();
                initialStattus = "javascript:initialValidateStatus(\"css_" + name + "\",\"errorMeessage_" + name + "\",\"validStatus_" + name + "\",\"input-control select\")";
                var sItemCollection = new List<PlainFacadeItem>();
                PropertyInfo[] properties = typeof(T).GetProperties();
                foreach (var pItem in properties)
                {
                    var pAtrribute = Attribute.GetCustomAttributes(pItem).Where(n => n.GetType().Name == "PlainFacadeItemSpecification");
                    foreach (var aItem in pAtrribute)
                    {
                        var sPropertyAttribute = aItem as PlainFacadeItemSpecification;
                        if (sPropertyAttribute.RelevanceID == name)
                        {
                            sItemCollection = pItem.GetValue(boVM) as List<PlainFacadeItem>;
                        }
                    }
                }

                foreach (var item in sItemCollection)
                {
                    var selected = "";
                    if (item.ID == valueString)
                        selected = "checked";

                    htmlString.Append("<div class='input-control checkbox' data-role='input-control' style='margin-right:10px;margin-top:-2px'>");
                    htmlString.Append("<label><input type='checkbox' value='" + item.ID + "' name='" + name + "' " + selected + " /><span class='check'></span>" + item.Name + "</label></div>");
                }
                htmlString.Append("<div id='errorMeessage_" + name + "'><small class='fg-red'>" + errMessage + "</small></div>");
                //htmlString.Append("<div class='input-control checkbox margin10' data-role='input-control'><label>Check me out<input type='checkbox' disabled checked /><span class='check'></span></label></div>");
                //htmlString.Append("<div class='input-control radio margin10' data-role='input-control'><label><input type='radio' name='r1' checked /><span class='check'></span>Check me out</label></div>");
                //htmlString.Append("<div class='input-control text' data-role='datepicker' data-date='2013-11-13' data-effect='slide' data-other-days='1'><input type='text'><button class='btn-date'></button></div>");
                return htmlString.ToString();

            }

            //public static string A08_YesNo(T boVM, string name, string errStyle, string valueString, string initialStattus, string errMessage, string errMessageStyle)
            //{
            //    var htmlString = new StringBuilder();
            //    initialStattus = "javascript:initialValidateStatus(\"css_" + name + "\",\"errorMeessage_" + name + "\",\"validStatus_" + name + "\",\"input-control select\")";
            //    var sItemCollection = new List<PlainFacadeItem>();
            //    PropertyInfo[] properties = typeof(T).GetProperties();
            //    foreach (var pItem in properties)
            //    {
            //        var pAtrribute = Attribute.GetCustomAttributes(pItem).Where(n => n.GetType().Name == "PlainFacadeItemSpecification");
            //        foreach (var aItem in pAtrribute)
            //        {
            //            var sPropertyAttribute = aItem as PlainFacadeItemSpecification;
            //            if (sPropertyAttribute.RelevanceID == name)
            //            {
            //                sItemCollection = pItem.GetValue(boVM) as List<PlainFacadeItem>;
            //            }
            //        }
            //    }

            //    foreach (var item in sItemCollection)
            //    {
            //        var selected = "";
            //        if (item.ID == valueString)
            //            selected = "checked";

            //        htmlString.Append("<div class='input-control radio' data-role='input-control' style='margin-right:10px;margin-top:-2px'>");
            //        htmlString.Append("<label><input type='radio' value='" + item.ID + "' name='radio01' " + selected + " /><span class='check'></span>" + item.Name + "</label></div>");
            //    }
            //    htmlString.Append("<div id='errorMeessage_" + name + "'><small class='fg-red'>" + errMessage + "</small></div>");
            //    //htmlString.Append("<div class='input-control checkbox margin10' data-role='input-control'><label>Check me out<input type='checkbox' disabled checked /><span class='check'></span></label></div>");
            //    //htmlString.Append("<div class='input-control radio margin10' data-role='input-control'><label><input type='radio' name='r1' checked /><span class='check'></span>Check me out</label></div>");
            //    //htmlString.Append("<div class='input-control text' data-role='datepicker' data-date='2013-11-13' data-effect='slide' data-other-days='1'><input type='text'><button class='btn-date'></button></div>");
            //    return htmlString.ToString();
            //}


            public static string  A08_Hidden(string name,string valueString)
            {
                var htmlString = new StringBuilder();
                htmlString.Append("<input type='hidden' value='" + valueString + "' name='" + name + "' id='" + name + "' />");
                return htmlString.ToString();
            }
        }
    }
}
