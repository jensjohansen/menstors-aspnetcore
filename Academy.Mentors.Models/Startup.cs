/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Configuration; 
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Logging; 
namespace Academy.Mentors.Models 
{ 
    /// <summary> 
    /// Startup 
    /// </summary> 
    public class Startup 
    { 
        /// <summary> 
        /// Startup 
        /// </summary> 
        /// <param name="env"></param> 
        public Startup(IHostingEnvironment env) 
        { 
            var builder = new ConfigurationBuilder() 
                .SetBasePath(env.ContentRootPath) 
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true) 
                .AddEnvironmentVariables(); 
            Configuration = builder.Build(); 
        } 

        /// <summary> 
        /// Configuration 
        /// </summary> 
        public IConfigurationRoot Configuration { get; } 

        // This method gets called by the runtime. Use this method to add services to the container. 
        /// <summary> 
        /// ConfigureServices 
        /// </summary> 
        /// <param name="services"></param> 
        public void ConfigureServices(IServiceCollection services) 
        { 
            // Add framework services. 
            services.AddMvc(); 
        } 

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
        /// <summary> 
        /// Configure 
        /// </summary> 
        /// <param name="app"></param> 
        /// <param name="env"></param> 
        /// <param name="loggerFactory"></param> 
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) 
        { 
            loggerFactory.AddConsole(Configuration.GetSection("Logging")); 
            loggerFactory.AddDebug(); 

            app.UseMvc(); 
        } 
    } 
} 
