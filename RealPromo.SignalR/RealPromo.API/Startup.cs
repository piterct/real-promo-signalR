using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using RealPromo.API.Hubs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace RealPromo.API
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

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.Authority = "https://localhost:5001/";
             options.Audience = "dataEventRecordsApi";
             options.IncludeErrorDetails = true;
             options.SaveToken = true;
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateIssuerSigningKey = true,
                 NameClaimType = "email",
                 RoleClaimType = "role",
                 ValidAudiences = new List<string> { "dataEventRecordsApi" },
                 ValidIssuers = new List<string> { "https://localhost:5001/" }
             };
             options.Events = new JwtBearerEvents
             {
                 OnMessageReceived = context =>
                 {
                     if ((context.Request.Path.Value!.StartsWith("/signalrhome")
                         || context.Request.Path.Value!.StartsWith("/looney")
                         || context.Request.Path.Value!.StartsWith("/usersdm")
                        )
                         && context.Request.Query.TryGetValue("token", out StringValues token)
                     )
                     {
                         context.Token = token;
                     }

                     return Task.CompletedTask;
                 },
                 OnAuthenticationFailed = context =>
                 {
                     var te = context.Exception;
                     return Task.CompletedTask;
                 }
             };
         });

            services.AddControllersWithViews();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //RPC -> Endpoint
                endpoints.MapHub<PromoHub>("/PromoHub");
            });

            
         
        }
    }
}
