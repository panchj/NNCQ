using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.ViewModelAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class PlainFacadeItemSpecification : Attribute
    {
        public string RelevanceID { get; set; }
        public PlainFacadeItemSpecification(string id) 
        {
            RelevanceID = id;
        }

    }
}
