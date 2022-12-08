using Data.Entity;
using Data.Enum;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;
using CRMS.Providers;
using CRMS.Models;
using Microsoft.AspNet.Identity;


namespace CRMS.Controllers
{
    public class BaseApiController : ApiController
    {
        protected void PopulateMetaData(BaseEntity entity)
        {
            if (entity.Id == 0)
            {
                //New Entity
                entity.CreationTs = DateTime.Now;
                entity.CreatedBy = LoggedInUserName;
            }
            else
            {
                entity.ModifiedTs = DateTime.Now;
                entity.ModifiedBy = LoggedInUserName;
            }

            entity.StatusType = StatusType.Enabled;
        }



        protected string LoggedInUserName
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }


        private ModelFactory _modelFactory;
        private ApplicationUserManager _AppUserManager = null;
        private ApplicationRoleManager _AppRoleManager = null;

        protected ApplicationUserManager AppUserManager
        {
            get
            {
                return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        protected ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _AppRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        public BaseApiController()
        {
        }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request, this.AppUserManager);
                }
                return _modelFactory;
            }
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
