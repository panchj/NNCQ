using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.ViewModelAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EditorItemSpecification:Attribute
    {
        public EditorItemType ItemType { get; set; }
        public int Width { get; set; }
        public int HorizontalZone = 1;

        public EditorItemSpecification(EditorItemType itemType) 
        {
            this.ItemType = itemType;
        }
    }
}
