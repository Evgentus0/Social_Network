using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Owin;
using SocialNetwork_BLL.Infrastructure;
using SocialNetwork_BLL.Interfaces;
using SocialNetwork_BLL.Services;
using SocialNetwork_PL.Providers;
using System;

namespace SocialNetwork_PL
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }
        private IUserService _userService;
        public Startup()
        {
            IKernel kernel = new StandardKernel(new ServiceModule("name=SocialNetwork_FinalProject"));
            kernel.Bind<IUserService>().To<UserService>();
            _userService = kernel.Get<IUserService>();
            
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);


            // Configure the application for OAuth based flow
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new ApplicationOAuthProvider(_userService),
                AuthorizeEndpointPath = new PathString("/api/users/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);


            //var facebookOption = new FacebookAuthenticationOptions()
            //{
            //    AppId = "1173744182813035",
            //    AppSecret = "60b73ea6d8a6156fa75c39e8b866fc91",
            //    BackchannelHttpHandler=new FacebookBackChannelHandler(),
            //    UserInformationEndpoint= "https://graph.facebook.com/v2.4/me?fields=id,email"
            //};
            //facebookOption.Scope.Add("email");
            //app.UseFacebookAuthentication(facebookOption);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");


            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "200918633820-ubb670l3h5r0ho874jrfvrmjpm2lrsrn.apps.googleusercontent.com",
            //    ClientSecret = "qYM2l7836_BcSdKUH9rBOpAF"
            //});
        }
    }
}
