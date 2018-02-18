/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Academy.Mentors.Administration;

/// <summary>
/// Design Time DbContext Factory for Migrations
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AdministrationContext>
{
    /// <summary>
    /// Create DB Context
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public AdministrationContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var builder = new DbContextOptionsBuilder<AdministrationContext>();
        var connectionString = configuration.GetConnectionString("Academy.MentorsDatabase");
        builder.UseSqlServer(connectionString);
        return new AdministrationContext(builder.Options);
    }
}
