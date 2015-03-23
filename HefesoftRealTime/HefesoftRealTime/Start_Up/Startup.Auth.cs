using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using AccidentalFish.AspNet.Identity.Azure;
using HefesoftRealTime.Providers;

namespace HefesoftRealTime
{
    public partial class Startup
    {
        static Startup()
        {
           
            PublicClientId = "self";
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=hefesoft;AccountKey=dodn17DT7hBi3lXrWlvXihLS9J7xuItHLIpWLBZn2QEMdBHm02Lqxr055rNCpP5z3FhfcjjX3MhPy1Npk3VF3Q==";
            UserManagerFactory = () => new UserManager<TableUser>(new TableUserStore<TableUser>(connectionString));

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, UserManagerFactory),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static Func<UserManager<TableUser>> UserManagerFactory { get; set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            //app.UseCookieAuthentication(new CookieAuthenticationOptions());
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enable the application to use bearer tokens to authenticate users
            // app.UseOAuthBearerTokens(OAuthOptions);

            app.UseOAuthAuthorizationServer(OAuthOptions);
            
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                Provider = new ApplicationOAuthBearerAuthenticationProvider(),
            });
        }
    }
}
