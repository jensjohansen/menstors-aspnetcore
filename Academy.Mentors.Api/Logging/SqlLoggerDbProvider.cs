/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using Microsoft.Extensions.Logging;
using System;

namespace Academy.Mentors.Api.Logging
{
    /// <summary>
    /// SqlDb Logger Provider 
    /// </summary>
    public class SqlDbLoggerProvider: ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filter;
        private string _connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="connectionStr"></param>
        public SqlDbLoggerProvider(Func<string, LogLevel, bool> filter, string connectionStr)
        {
            _filter = filter;
            _connectionString = connectionStr;
        }

        /// <summary>
        /// Create Logger from Factory
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            return new SqlDbLogger(categoryName, _filter, _connectionString);
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
        }
    }
}
