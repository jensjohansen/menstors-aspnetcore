/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



namespace Academy.Mentors.Administration.Services
{
    /// <summary>
    /// Options for Email Service
    /// </summary>
    public class AuthMessageSenderOptions
    {
        /// <summary>
        /// SendGrid User
        /// </summary>
        public string SendGridUser { get; set; }

        /// <summary>
        /// Send Grid Key
        /// </summary>
        public string SendGridKey { get; set; }
    }
}
