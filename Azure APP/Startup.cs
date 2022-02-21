using Azure_APP.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using ITfoxtec.Identity.Saml2;
using ITfoxtec.Identity.Saml2.Schemas.Metadata;
using ITfoxtec.Identity.Saml2.MvcCore.Configuration;

namespace Azure_APP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddControllers();
            var ssoSettings = Configuration.GetSection("SSOSettings").Get<SSO>();

            //// Saml2 Authentication
            //services.AddAuthentication(sharedOptions =>
            //{
            //    sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    sharedOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    sharedOptions.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    sharedOptions.DefaultChallengeScheme = "Saml2";
            //})
            //  .AddSaml2(options =>
            //  {
            //      options.SPOptions.EntityId = new EntityId(ssoSettings.EntityID);
            //      options.IdentityProviders.Add(
            //      new IdentityProvider(
            //        new EntityId(ssoSettings.IdentityProvider), options.SPOptions)
            //      {
            //          MetadataLocation = ssoSettings.MetadataLocation
                      
            //      });
            //  })
            // .AddCookie(options =>
            // {
            //     options.Cookie.Name = "DSS_Backoffice";
            //     options.LoginPath = new PathString("/Login");
            //     options.LogoutPath = new PathString("/logout");
            //     options.ExpireTimeSpan = TimeSpan.FromDays(1);
            //     options.SlidingExpiration = true;
            //     // options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseResponseCaching();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
