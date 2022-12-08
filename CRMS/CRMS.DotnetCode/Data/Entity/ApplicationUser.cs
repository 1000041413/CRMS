using Data.Enum;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class User : IdentityUser
    {
        public long EmpID { get; set; }

        public string EmpAddress { get; set; }

        public UserType UserType { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("role", Convert.ToString((int)this.UserType)));
            userIdentity.AddClaim(new Claim("userid", this.Id));
            userIdentity.AddClaim(new Claim("username", string.Concat(this.Firstname, ' ', this.Lastname)));

            return userIdentity;
        }
    }
}
