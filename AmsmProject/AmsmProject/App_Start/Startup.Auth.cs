using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace AmsmProject
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            //OWIN cookie authentication 
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,

                //redirect unauthorized requests to the /Error/AccessDenied"
                LoginPath = new PathString("/Error/AccessDenied"),
                // the authentication cookie created by ASP.NET Identity to expire after 2 hours.
                //The expiration allows the application to indicate how long the cookie is valid
                ExpireTimeSpan = new System.TimeSpan(2, 0, 0),
              //the sliding flag allows the expiration to be renewed as the user remains active within the application
                SlidingExpiration = true,
                //Defines if the cookie will only be sent back to HTTPS URL
                 CookieSecure = CookieSecureOption.Always

   
   
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
          //  app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            
           
        }
    }
}