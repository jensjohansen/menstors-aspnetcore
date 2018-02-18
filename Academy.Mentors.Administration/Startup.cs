/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using Academy.Mentors.Administration.Models;
using Academy.Mentors.Administration.Services;
using Academy.Mentors.Api;
using Academy.Mentors.Api.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Mentors.Administration
{
    public class Startup
    {
        private static string SecretKey = "Dummy Secret Key To replace";
        // private static string SecretKey = System.Environment.GetEnvironmentVariable("Api_Signer_Key");  
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup Application Context
            services.AddDbContext<AdministrationContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MentorsAcademyDatabase")));

            var logConnectionString = ConfigurationExtensions.GetConnectionString(this.Configuration, "MentorsAcademyLogDatabase"); 
            services.AddDbContext<LogDataContext>(options => 
            { 
                options.UseSqlServer(logConnectionString); 
            }); 

            // Setup ASP.NET Identity for logins
            services.AddScoped<IPasswordHasher<ApplicationUser>, CrossPlatformPasswordHasher<ApplicationUser>>();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AdministrationContext>()
                .AddDefaultTokenProviders();

            // Get options from app settings 
            var jwtAppSettingOptions = Configuration.GetSection("JwtIssuerOptions");
            var sesAppSettingOptions = Configuration.GetSection("AwsSesOptions");
            var sendGridAppSettingOptions = Configuration.GetSection("SendGridOptions");
            var loginProviders = Configuration.GetSection("LoginProviders");

            // Configure JwtIssuerOptions    
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.Api_Signer_Key = jwtAppSettingOptions[nameof(JwtIssuerOptions.Api_Signer_Key)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            // If you want to tweak Identity cookies, they're no longer part of IdentityOptions.
            services.ConfigureApplicationCookie(options => options.LoginPath = "/Access/LogIn");
            services.AddAuthentication()
                /*
                    .AddFacebook(options =>
                    {
                        options.AppId = loginProviders["FacebookApiKey"];
                        options.AppSecret = loginProviders["FacebookSecret"];
                    })
                    .AddGoogle(options =>
                    {
                        options.ClientId = loginProviders["GoogleApiKey"];
                        options.ClientSecret = loginProviders["GoogleSecret"];
                    })
                    .AddMicrosoftAccount(options =>
                    {
                        options.ClientId = loginProviders["MicrosoftApiKey"];
                        options.ClientSecret = loginProviders["MicrosoftSecret"];
                    })
                    .AddTwitter(options =>
                    {
                        options.ConsumerKey = loginProviders["TwitterApiKey"];
                        options.ConsumerSecret = loginProviders["TwitterSecret"];
                    })
                */
                ;

            // Add Framework Services
            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.Configure<AuthMessageSenderOptions>(options => {
                options.SendGridKey = sendGridAppSettingOptions["ApiKey"];
                options.SendGridUser = sendGridAppSettingOptions["User"];
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddContext(LogLevel.Information, Configuration.GetConnectionString("MentorsAcademyLogDatabase")); 

            CreateRoles(serviceProvider).Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            UserManager.PasswordHasher = new CrossPlatformPasswordHasher<ApplicationUser>();
            string[] roleNames = { "Administrator", "Manager", "Member" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Here you could create a super user who will maintain the web app
            var poweruser = new ApplicationUser
            {

                UserName = "defaultAdministrator@Academy.Mentors",
                Email = "defaultAdministrator@Academy.Mentors",
            };

            //Ensure you have these values in your appsettings.json file
            string userPWD = "Default#Admin#Password777";
            var _user = await UserManager.FindByEmailAsync(poweruser.UserName);

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(poweruser, "Administrator");
                    await UserManager.AddToRoleAsync(poweruser, "Manager");
                    await UserManager.AddToRoleAsync(poweruser, "Member");
                }
            }
        }

    }
}
