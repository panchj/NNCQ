using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Core
{
    public interface IEntity
    {
        Guid ID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string SortCode { get; set; }
    }
}
