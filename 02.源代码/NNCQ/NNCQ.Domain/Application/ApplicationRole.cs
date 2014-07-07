using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Application
{
    public class ApplicationRole:IdentityRole
    {
        [StringLength(250)]
        public string DisplayName { get; set; }
        [StringLength(550)]
        public string Description { get; set; }
        [StringLength(50)]
        public string SortCode { get; set; }
    }
}
