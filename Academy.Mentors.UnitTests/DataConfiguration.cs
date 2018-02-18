/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



namespace Academy.Mentors.UnitTests
{
    /// <summary>
    /// Unit Test Data Configuration
    /// </summary>
    public class DataConfiguration
    {
        /// <summary>
        /// Connection String
        /// </summary>
        protected readonly string connectionString = "server=localhost\\SQLEXPRESS;user id=dbuser;password=dbpassword;Integrated Security=True;MultipleActiveResultSets=True;persistsecurityinfo=True;database=MentorsAcademy";

        /// <summary>
        /// Unit Test Username
        /// </summary>
        protected readonly string loginEmail = "defaultUnitTestRunner@Academy.Mentors";

        /// <summary>
        /// Unit test Password
        /// </summary>
        protected readonly string loginPassword = "Unit#Test#Password2018";

        /// <summary>
        /// Constructor
        /// </summary>
        public DataConfiguration() { }

        /// <summary>
        /// Password property
        /// </summary>
        public string LoginPassword => loginPassword;

        /// <summary>
        /// Login Username Property
        /// </summary>
        public string LoginEmail => loginEmail;

        /// <summary>
        /// Connection String Property
        /// </summary>
        public string ConnectionString => connectionString;
    }
}
