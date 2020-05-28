using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.Repository;
using AuthServer.Services;
using AuthServer.Stores;
using IdentityServer4;
using IdentityServer4.Quickstart.UI;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace AuthServer
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

            IdentityModelEventSource.ShowPII = true;

            var builder = services.AddIdentityServer(action =>
            {
                action.UserInteraction.LoginUrl = "/account/login"; // 修改默认得登录地址
            });
            builder.Services.AddTransient<UserClientRepository>();
            builder.Services.AddTransient<ApiResourceRepository>();

            builder.AddInMemoryIdentityResources(Configs.Config.GetIdentityResources())
                //.AddInMemoryApiResources(Configs.Config.GetApis())
                .AddResourceStore<InDatabaseResourceStore>()
                .AddClientStore<InDatabaseClientStore>()
                .AddTestUsers(TestUsers.Users)
                .AddCustomTokenRequestValidator<BBSSTokenRequestValidator>()
                .AddProfileService<ProfileService>()
                ;
            builder.AddDeveloperSigningCredential();

            builder.Services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            builder.Services.AddTransient<IProfileService, ProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
