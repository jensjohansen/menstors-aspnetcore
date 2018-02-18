/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using Academy.Mentors.Api;
using Microsoft.AspNetCore.Identity;

namespace Academy.Mentors.Administration
{
    /// <summary>
    /// A Password Hasher compatible with PHP and Java Hashing as well
    /// </summary>
    public class CrossPlatformPasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
    {
        private Tokenator _tokenizer = new Tokenator();

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CrossPlatformPasswordHasher()
        {
        }

        /// <summary>
        /// Returns the Hashed Password of a String
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(string password)
        {
            return _tokenizer.GetSHA1HashData(password);
        }

        /// <summary>
        /// Identity 3.0 HashPassword
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(TUser user, string password)
        {
            return _tokenizer.GetSHA1HashData(password);
        }

        /// <summary>
        /// Validates a provided password in clear text matches the hashed password
        /// </summary>
        /// <param name="hashedPassword"></param>
        /// <param name="providedPassword"></param>
        /// <returns></returns>
        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword == _tokenizer.GetSHA1HashData(providedPassword))
            {
                return PasswordVerificationResult.Success;
            }
            return PasswordVerificationResult.Failed;
        }

        /// <summary>
        /// Identity 3 Verify Hashed Password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="hashedPassword"></param>
        /// <param name="providedPassword"></param>
        /// <returns></returns>
        public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            if (hashedPassword == _tokenizer.GetSHA1HashData(providedPassword))
            {
                return PasswordVerificationResult.Success;
            }
            return PasswordVerificationResult.Failed;
        }
    }
}
