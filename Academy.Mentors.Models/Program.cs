/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using System; 
using System.Collections.Generic; 
using System.IO; 
using System.Linq; 
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting; 

namespace Academy.Mentors.Models 
{ 
    /// <summary> 
    /// Program 
    /// </summary> 
    public class Program 
    { 
        /// <summary> 
        /// Main 
        /// </summary> 
        /// <param name="args"></param> 
        public static void Main(string[] args) 
        { 
            var host = new WebHostBuilder() 
                .UseKestrel() 
                .UseContentRoot(Directory.GetCurrentDirectory()) 
                .UseIISIntegration() 
                .UseStartup<Startup>() 
               // .UseApplicationInsights() 
                .Build(); 
            host.Run(); 
        } 
    } 
} 
