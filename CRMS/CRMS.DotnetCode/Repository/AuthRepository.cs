using Data;
using Data.Entity;
using Data.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Repository
{
    public class AuthRepository : IAuthRepository
    {
        private CRMSDbContext dbContext;
        private UserManager<User> _userManager;

        public AuthRepository(CRMSDbContext context, UserManager<User> userManager)
        {
            dbContext = context;
            _userManager = userManager;
            //   _mapper = mapper;
        }

        public async Task<User> FindUser(string userName, string password) //async
        {
            User user = await  _userManager.FindAsync(userName, password);

            return user;
        }




        public async Task<IdentityResult> Register(RegisterViewModel model) //async
        {

            var user = new User
            { UserName = model.UserName,
                                  Firstname= model.Firstname ,
                                  Lastname = model.Lastname,
                                  UserType = model.UserType,
                                  EmpID = model.EmpID,
                                  Email=model.Email

            };
          //  var role = model.UserType.ToString();

           /* if (await _userManager.FindByNameAsync(role) == null)
            {
                var roleResult = await _userManager.CreateAsync(role);

                if (roleResult != IdentityResult.Success)
                {
                    return roleResult;
                }
            }*/

            var result = await _userManager.CreateAsync(user, model.Password);
 

         //   var addToRoleResult = await _userManager.AddToRoleAsync(user.Id, role);

            return !result.Succeeded ? result : IdentityResult.Success;
        }
    }
}
