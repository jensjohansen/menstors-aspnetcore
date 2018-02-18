/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



/* Powered by Solution Zone (http://www.solution.zone)  */
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academy.Mentors.Models;

namespace Academy.Mentors.Api.Controllers
{
    /// <summary>
    /// Contributors RESTful API Controller (ASP.NET Core)
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ContributorApiController : ContributorBaseApiController
    {
        /// <summary>
        /// Contributor API Controller
        /// </summary>
        /// <param name="apiDataContext">DbContext for EntityFramework</param>
        /// <param name="logger">Injected ILoggerFactory</param>
        public ContributorApiController(ApiDataContext apiDataContext, ILoggerFactory logger) : base(apiDataContext, logger)
        { 
        }

		/// <summary>
		/// Logs user into the system, providing a backend-side loginToken. All update API methods require the login token.
		/// </summary>
		/// <remarks></remarks>
		/// <param name="Email">The user name for login</param>
		/// <param name="Password">The password for login in clear text</param>
		/// <response code="200">successful operation</response>
		/// <response code="400">Invalid username/password supplied</response>
		/// <response code="403">Account blocked or unsuccessful login threshold exceeded</response>
		/// <response code="404">User not registered or registration pending</response>
		[HttpGet]
		[Route("/contributor/login")]
		[ProducesResponseType(typeof(string), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		[ApiExplorerSettings(IgnoreApi = false)]
		public virtual IActionResult LoginUser([FromQuery]string Email, [FromQuery]string Password)
		{
			var hashedPassword = _tokenizer.GetSHA1HashData(Password);
			var user = _dbContext.Contributors.First(u => u.Email == Email);
			if (user == null) return NotFound("User not found");
			if (user.Password == hashedPassword)
			{
				long Id = user.Id ?? default(long);
				return new OkObjectResult(_tokenizer.GetToken(Id, "login", 24));
			}
			else return NotFound("User not registered or registration pending");
		}

    }
}
