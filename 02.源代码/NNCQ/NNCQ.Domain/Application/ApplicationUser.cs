using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NNCQ.Domain.Core;
using NNCQ.Domain.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Application
{
    public class ApplicationUser : IdentityUser 
    {
        public virtual Person Person { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

    }
}
