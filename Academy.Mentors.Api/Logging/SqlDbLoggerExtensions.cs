/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using Microsoft.Extensions.Logging; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
 
namespace Academy.Mentors.Api.Logging 
{ 
    /// <summary>
    /// SqlDb Logger Extensions
    /// </summary>
    public static class SqlDbLoggerExtensions 
    { 
        /// <summary>
        /// Add Context with defaults
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="filter"></param>
        /// <param name="connectionStr"></param>
        /// <returns></returns>
        public static ILoggerFactory AddContext(this ILoggerFactory factory, 
        Func<string, LogLevel, bool> filter = null, string connectionStr = null) 
        { 
            factory.AddProvider(new SqlDbLoggerProvider(filter, connectionStr)); 
            return factory; 
        } 
 
        /// <summary>
        /// Add Context 
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="minLevel"></param>
        /// <param name="connectionStr"></param>
        /// <returns></returns>
        public static ILoggerFactory AddContext(this ILoggerFactory factory, LogLevel minLevel, string connectionStr) 
        { 
            return AddContext( 
                factory, 
                (_, logLevel) => logLevel >= minLevel, connectionStr); 
        } 
    } 
} 
