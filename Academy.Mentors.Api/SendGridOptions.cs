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
    /// SendGrid Options for Sending Email/SMS Messages 
    /// </summary> 
    public class SendGridOptions : IOptions<SendGridOptions> 
    { 
        /// <summary> 
        /// The API key for SendGrid 
        /// </summary> 
        public string ApiKey { get; set; } 

        /// <summary> 
        /// The Email from which the messages are sent 
        /// </summary> 
        public string From { get; set; } 

        /// <summary> 
        /// Return the properties as Value 
        /// </summary> 
        /// <returns></returns> 
        public SendGridOptions Value 
        { 
            get { return this; } 
            set { } 
        } 
    } 
} 
