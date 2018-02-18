/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System;

namespace Academy.Mentors.Api.Logging
{
    /// <summary>
    /// SqlDb Logger Class
    /// </summary>
    public class SqlDbLogger: ILogger
    {
        private string _categoryName;
        private Func<string, LogLevel, bool> _filter;
        private string _connectionString;
        private int MessageMaxLength = 4000;

        /// <summary>
        /// SqlDb Logger Constructor
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="filter"></param>
        /// <param name="connectionString"></param>
        public SqlDbLogger(string categoryName, Func<string, LogLevel, bool> filter, string connectionString)
        {
            _categoryName = categoryName;
            _filter = filter;
            _connectionString = connectionString;
        }

        /// <summary>
        /// Log a Message to the SqlDb Logging Database
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public void Log<TState>(
            LogLevel logLevel, 
            EventId eventId, 
            TState state, 
            Exception exception, 
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));
            
            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message)) return; 

            if (exception != null)
            {
                message += "\n" + exception.ToString();
            }

            long LogLevelKey = 6; 

            switch(logLevel)
            {
                case LogLevel.Critical: { LogLevelKey = 1; break; }
                case LogLevel.Debug: { LogLevelKey = 7; break; }
                case LogLevel.Error: { LogLevelKey = 4; break; }
                case LogLevel.Information: { LogLevelKey = 6; break; }
                case LogLevel.Trace: { LogLevelKey = 3; break; }
                case LogLevel.Warning: { LogLevelKey = 5; break; }
            }

            message = message.Length > MessageMaxLength ? message.Substring(0, MessageMaxLength) : message;

            string query = "INSERT INTO [LogMessages]([ApplicationName],[FullMessage], [ShortMessage], [LogMessageTypeId]) VALUES ("
                + "'Academy.Mentors','" 
                + message.Replace("'", "''") + "','"
                + eventId.Name + "',"
                + LogLevelKey + ");";
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                using(var insertCommand = dbConnection.CreateCommand())
                {
                    insertCommand.CommandText = query;
                    insertCommand.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Check if LogLevel is enabled
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return (_filter == null || _filter(_categoryName, logLevel));
        }

        /// <summary>
        /// Begin Scope for Logging
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}

