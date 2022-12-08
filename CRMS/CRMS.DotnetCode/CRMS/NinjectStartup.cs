using Ninject.Modules;
using Repository;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CRMS
{
    public class NinjectStartup : NinjectModule
    {
        public override void Load()
        {
            this.Bind<HttpConfiguration>().ToMethod(ctx => GlobalConfiguration.Configuration);

            this.Kernel.Bind<IUow>().To<Uow>();
        }
    }
}