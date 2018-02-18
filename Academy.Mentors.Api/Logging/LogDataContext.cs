/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using Microsoft.EntityFrameworkCore;

namespace Academy.Mentors.Api.Logging
{
    /// <summary>
    /// Logging Database Context
    /// </summary>
    public class LogDataContext : DbContext
    {
        /// <summary>
        /// Log Database Context
        /// </summary>
        /// <param name="options"></param>
        public LogDataContext(DbContextOptions<LogDataContext> options) : base(options)
        {
        }

        /// <summary>
        /// The Message Types DbSet
        /// </summary>
        public DbSet<LogMessageType> LogMessageTypes { get; set; }

        /// <summary>
        /// The Log Messages DbSet
        /// </summary>
        public DbSet<LogMessage> LogMessages { get; set; }

    }
}
