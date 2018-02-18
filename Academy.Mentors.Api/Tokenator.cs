/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using System; 
using System.IO; 
using System.IO.Compression; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 

namespace Academy.Mentors.Api 
{ 
    /// <summary> 
    /// Tokenator - Generates and validates Object Tokens 
    /// </summary> 
    public class Tokenator 
    { 
        private byte[] service_key = Encoding.ASCII.GetBytes("Dummy Signing Key"); 

        /// <summary> 
        /// Default Constructor 
        /// </summary> 
        public Tokenator() { } 
        /// <summary> 
        /// Override default constructor to pull signer key from JWT if Get Environment Variable fails (as it does on AWS) 
        /// </summary> 
        public Tokenator(string signature) 
        { 
            var signer = Environment.GetEnvironmentVariable("Api_Signer_Key"); 
            if (String.IsNullOrEmpty(signer)) 
            { 
                signer = signature; 
            } 
            service_key = Encoding.ASCII.GetBytes(signer); 
        } 

        /// <summary> 
        /// Creates a simple token with time stamp 
        /// </summary> 
        /// <param name="Id">The PK ID</param> 
        /// <param name="objectName">The Object Name this tokenizes</param> 
        /// <param name="hours">The number of hours this token should live.</param> 
        /// <returns>Id Token</returns> 
        public string GetToken(long Id, string objectName, int hours) 
        { 
            var result = objectName + "|" + Id + "|" + DateTime.UtcNow.AddHours(hours).Ticks.ToString() + "|"; 
            result += Encode(result); 
            return result; 
        } 

        /// <summary> 
        /// Validates a Tokenator Token, returning null if failed or expired, and a valid ID if valid and unexpired 
        /// </summary> 
        /// <param name="token"></param> 
        /// <returns></returns> 
        public long? ValidateToken(string token) 
        { 
            long? result = null; 
            if (token == null) 
            { 
                return result; 
            } 
            else 
            { 
                var original = token.Split('|'); 
                if (original.Length == 4 && Encode(original[0] + "|" + original[1] + "|" + original[2] + "|").Equals(original[3]) && 
                    Int64.TryParse(original[2], out long ticks) && ticks > DateTime.UtcNow.Ticks) 
                { 
                    if (Int64.TryParse(original[1], out long id)) result = id; 
                } 
                return result; 
            } 
        } 

        /// <summary> 
        /// Hashes the main token 
        /// </summary> 
        /// <param name="input"></param> 
        /// <returns></returns> 
        private string Encode(string input) 
        { 
            HMACSHA1 myhmacsha1 = new HMACSHA1(service_key); 
            byte[] byteArray = Encoding.ASCII.GetBytes(input); 
            MemoryStream stream = new MemoryStream(byteArray); 
            return myhmacsha1.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s); 
        } 

		/// <summary>
		/// take any string and encrypt it using SHA1 then
		/// return the encrypted data. This is the password Encryption algorithm used
		/// in the CodeIgniter version of the MySolution Stack
		/// </summary>
		/// <param name="data">input text you will entered to encrypt it</param>
		/// <returns>return the encrypted text as hexadecimal string</returns>
		public string GetSHA1HashData(string data)
		{
			StringBuilder returnValue = new StringBuilder();
			//create new instance of md5
			using (var sha1 = SHA1.Create())
			{
				//convert the input text to array of bytes
				// PHP uses ASCII encoding
				var hashData = sha1.ComputeHash(Encoding.ASCII.GetBytes(data));
				//loop for each byte and add it to StringBuilder
				foreach (var b in hashData)
				{
					returnValue.Append(b.ToString("x2"));
				}
			}
			return returnValue.ToString();
		}


        /// <summary>
        /// Hash a password using PHP, Java and .NET compatible hashing algorithm
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(string password)
        {
            return GetSHA1HashData(password);
        }

        /// <summary>
        /// Coy data between streams
        /// </summary>
        /// <param name="inStream"></param>
        /// <param name="outStream"></param>
        public static void CopyStream(Stream inStream, Stream outStream)
        {
            byte[] bytes = new byte[4096];
            int cnt;

            while ((cnt = inStream.Read(bytes, 0, bytes.Length)) != 0)
            {
                outStream.Write(bytes, 0, cnt);
            }
        }

        /// <summary>
        /// Compress a string into a byte array
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static byte[] Compress(string original)
        {
            var bytes = Encoding.UTF8.GetBytes(original);

            using (var inStream = new MemoryStream(bytes))
            using (var outStream = new MemoryStream())
            {
                using (var gs = new GZipStream(outStream, CompressionMode.Compress))
                {
                    CopyStream(inStream, gs);
                }
                return outStream.ToArray();
            }
        }

        /// <summary>
        /// Decompress a byte array into the original string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string Decompress(byte[] bytes)
        {
            using (var inStream = new MemoryStream(bytes))
            using (var outStream = new MemoryStream())
            {
                using (var gs = new GZipStream(inStream, CompressionMode.Decompress))
                {
                    CopyStream(gs, outStream);
                }
                return Encoding.UTF8.GetString(outStream.ToArray());
            }
        }

        /// <summary>
        /// Compress a string into a Base64 String
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public string Compress64(string original)
        {
            return Convert.ToBase64String(Compress(original));
        }

        /// <summary>
        /// Decompress a string from its compressed Base64 String
        /// </summary>
        /// <param name="compressed"></param>
        /// <returns></returns>
        public string Decompress64(string compressed)
        {
            return Decompress(Convert.FromBase64String(compressed));
        }
    } 
} 
