/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/




using Academy.Mentors.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens; 
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Academy.Mentors.Api.Controllers
{
    /// <summary>
    /// JWT Controller
    /// </summary>
    [Route("oauth/[controller]")]
    [DataContract]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class JwtController : Controller
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ILogger _logger;
        private readonly JsonSerializerSettings _serializerSettings;
        private ApiDataContext _dbContext;
        private Tokenator _tokenizer = new Tokenator();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jwtOptions"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="cloutApiDataContext"></param>
        public JwtController(IOptions<JwtIssuerOptions> jwtOptions, ILoggerFactory loggerFactory, ApiDataContext cloutApiDataContext)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);

            _logger = loggerFactory.CreateLogger<JwtController>();

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            _dbContext = cloutApiDataContext;
            _dbContext.Database.EnsureCreated();
            var jwtCount = _dbContext.JwtUsers.Count();
            if (jwtCount == 0)
            {
                _dbContext.JwtUsers.Add(new JwtUser {
                    UserName = "DefaultApplication",
                    Password = _tokenizer.GetSHA1HashData("DefaultApplicationPassword")
                });
                _dbContext.SaveChanges();
            }

        }

        /// <summary>
        /// Get a token with username and password
        /// </summary>
        /// <param name="jwtUser"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/oauth/token")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        public async Task<IActionResult> Get([FromQuery] string username, [FromQuery] string password)
        {
            var hashedPassword = _tokenizer.GetSHA1HashData(password);
            var jwtUser = new JwtUser {
                UserName = username,
                Password = hashedPassword
            };
            var foundUser = _dbContext.JwtUsers.Where(u => u.UserName == username && u.Password == hashedPassword).FirstOrDefault();
            if (foundUser == null)
            {
                _logger.LogInformation($"Invalid username ({username}) or password ({password})");
                return BadRequest("Invalid credentials");
            }

            var identity = await GetClaimsIdentity(jwtUser, foundUser);
            if (identity == null)
            {
                _logger.LogInformation($"Invalid username ({username}) or password ({password})");
                return BadRequest("Missing credentials");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwtUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat,
                  ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                  ClaimValueTypes.Integer64),
                identity.FindFirst("Academy.MentorsCharacter")
            };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Serialize and return the response
            var response = new
            {
                api_key = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            };

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        /// <summary>
        /// IMAGINE BIG RED WARNING SIGNS HERE!
        /// You'd want to retrieve claims through your claims provider
        /// in whatever way suits you, the below is purely for demo purposes!
        /// </summary>
        private static Task<ClaimsIdentity> GetClaimsIdentity(JwtUser user, JwtUser foundUser)
        {
            if (user.UserName == foundUser.UserName &&
                user.Password == foundUser.Password)
            {
                return Task.FromResult(new ClaimsIdentity(
                  new GenericIdentity(user.UserName, "Token"),
                  new[]
                  {
                    new Claim("Academy.MentorsCharacter", "IAmAIAmAAcademy.MentorsUser")
                  }));
            }

            if (user.UserName == "NotMickeyMouse" &&
                user.Password == "MickeyMouseIsBoss123")
            {
                return Task.FromResult(new ClaimsIdentity(
                  new GenericIdentity(user.UserName, "Token"),
                  new Claim[] { }));
            }

            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
