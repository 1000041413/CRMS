using Autofac;
using Autofac.Integration.WebApi;
using CRMS.Providers;
using log4net;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Repository;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;
using System.Web.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Http.Cors;
using Data;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(CRMS.Startup))]
namespace CRMS
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            ConfigureOAuthTokenGeneration(app);
            
            ConfigureOAuthTokenConsumption(app);

            app.UseCors(CorsOptions.AllowAll);



            var builder = new ContainerBuilder();


            builder.RegisterType<Uow>().As<IUow>();


            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.Register(c => LogManager.GetLogger("CRMSLogger")).As<ILog>(); // Log4Net Dependency Injection



            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);


            
            GlobalConfiguration.Configure(WebApiConfig.Register);



            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);


        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(CRMSDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
           // app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationProvider(),
                AccessTokenFormat = new CustomJwtFormat("http://localhost:80/crms_1000041413")
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
        }


        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {

            var issuer = "http://localhost:80/crms_1000041413";
            string audienceId = ConfigurationManager.AppSettings["AudienceId"];
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["AudienceSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                    }
                });
        }

    }
      
}