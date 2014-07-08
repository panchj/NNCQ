using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.ViewModelAttribute
{
    /// <summary>
    /// 用于定义
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ExtensionJavaScriptFile:Attribute
    {
        public string FileSource { get; set; }

        public ExtensionJavaScriptFile(string source) 
        {
            this.FileSource = source;
        }
    }
}
