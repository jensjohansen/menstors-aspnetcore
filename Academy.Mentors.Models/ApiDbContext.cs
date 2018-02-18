/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using Microsoft.EntityFrameworkCore;

namespace Academy.Mentors.Models
{
    /// <summary>
    /// Main Database Context
    /// </summary>
    public class ApiDataContext : DbContext
    {
        /// <summary>
        /// Main Database Context
        /// </summary>
        /// <param name="options"></param>
        public ApiDataContext(DbContextOptions<ApiDataContext> options) : base(options)
        {
        }

        /// <summary>
        /// Application Users DbSet
        /// </summary>
        public DbSet<JwtUser> JwtUsers { get; set; }

        /// <summary>
        /// Contributors DbSet
        /// </summary>
        public DbSet<Contributor> Contributors { get; set; }

        /// <summary>
        /// Papers DbSet
        /// </summary>
        public DbSet<Paper> Papers { get; set; }

        /// <summary>
        /// Paper Versions DbSet
        /// </summary>
        public DbSet<PaperVersion> PaperVersions { get; set; }

        /// <summary>
        /// Reviews DbSet
        /// </summary>
        public DbSet<Review> Reviews { get; set; }

        /// <summary>
        /// Test Fields DbSet
        /// </summary>
        public DbSet<TestField> TestFields { get; set; }

    }
}
