/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using Academy.Mentors.Administration.Models;
using Academy.Mentors.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Academy.Mentors.Administration
{
    /// <summary>
    /// Main Database Context
    /// </summary>
    public class AdministrationContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Main Database Context
        /// </summary>
        /// <param name="options"></param>
        public AdministrationContext(DbContextOptions<AdministrationContext> options) : base(options)
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



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
