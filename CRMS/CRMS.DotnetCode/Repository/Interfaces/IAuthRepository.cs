using Data.Entity;
using Data.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task<IdentityResult> Register(RegisterViewModel model);
        Task<User> FindUser(string userName, string password);
    }
}
