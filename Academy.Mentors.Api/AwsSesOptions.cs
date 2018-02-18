/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace Academy.Mentors.Api
{
    /// <summary>
    /// Aws Ses Options for Sending Email/SMS Messages
    /// </summary>
    public class AwsSesOptions : IOptions<AwsSesOptions>
    {
        /// <summary>
        /// The Username used to authenticate to SMTP Server
        /// </summary>
        public string smtpUser { get; set; }

        /// <summary>
        /// The Passwrd used to authenticate to the SMTP server
        /// </summary>
        public string smtpPass { get; set; }

        /// <summary>
        /// The Port used to send email through the server
        /// </summary>
        public string smtpPort { get; set; }

        /// <summary>
        /// The Server DNS host name or IP address
        /// </summary>
        public string smtpHost { get; set; }

        /// <summary>
        /// The Reply To or From email address
        /// </summary>
        public string smtpFrom { get; set; }

        /// <summary>
        /// Return the properties as Value
        /// </summary>
        /// <returns></returns>
        public AwsSesOptions Value
        {
            get { return this; }
            set { }
        }

    }
}
