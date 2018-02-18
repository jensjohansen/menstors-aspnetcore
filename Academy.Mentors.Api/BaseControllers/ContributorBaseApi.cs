/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/


/* Powered by Solution Zone (http://www.solution.zone)  2/14/2018 1:37:43 PM */


using Academy.Mentors.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; 
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace Academy.Mentors.Api.Controllers
{
	/// <summary>
	/// Contributors RESTful API Controller (ASP.NET Core 2.0)
	/// </summary>
	[DataContract]
	public abstract class ContributorBaseApiController : Controller
	{
		/// <summary>
		/// Entity Framework DbContext for this controller
		/// </summary>
		protected ApiDataContext _dbContext;

		/// <summary>
		/// ILoggerFactory injected into this controller for logging
		/// </summary>
		protected readonly ILogger _logger; 

		/// <summary>
		/// Tokenizer injected into this controller for encryption
		/// </summary>
		protected Tokenator _tokenizer = new Tokenator();

		/// <summary>
		/// Constructor for Controller
		/// </summary>
		/// <param name="apiDataContext">API Data Context</param>
		/// <param name="logger">ILoggerFactory</param>
		public ContributorBaseApiController(ApiDataContext apiDataContext, ILoggerFactory logger)
		{
			_logger = logger.CreateLogger<ContributorBaseApiController>(); 
			_dbContext = apiDataContext;
			_dbContext.Database.EnsureCreated();

            var count = _dbContext.Contributors.Count();
            if (count == 0)
            {
                // Add default administrator
                _dbContext.Contributors.Add(
                        new Contributor
                        {
                            Name = "Default Administrator",
                            Description = "Default system administrator",
                            Email = "defaultAdministrator@Academy.Mentors",
                            Password = _tokenizer.GetSHA1HashData("Default#Admin#Password777"),
                            Comments = "This account should be disabled or deleted (in favor of a better admin account) in production"
                        }
                    );
                // Add default unit tester
                _dbContext.Contributors.Add(
                        new Contributor
                        {
                            Name = "Default Unit Tester",
                            Description = "Default unit test run account",
                            Email = "defaultUnitTestRunner@Academy.Mentors",
                            Password = _tokenizer.GetSHA1HashData("Unit#Test#Password2018"),
                            Comments = "This account should be disabled or deleted in production"
                        }
                    );
                _dbContext.SaveChanges();
			}

		}

        /// <summary>
        /// Ping to verify Contributors Api Health
        /// </summary>
        /// <param name="pingText">Arbitrary text passed into the servie</param>
        /// <response code="200">Ping Text Reversed</response>
        [HttpGet]
        [Route("/contributor/ping")]
        [ProducesResponseType(typeof(string), 200)]
        public virtual IActionResult PingContributors([FromQuery]string pingText)
        {
            var charArray = pingText.ToCharArray();
            Array.Reverse(charArray);
            return new OkObjectResult(new string(charArray));
        }

		/// <summary>
		/// Add new Contributor to the datastore
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="body">Contributor object that needs to be added to the datastore</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpPost]
		[Route("/contributor")]
		[ProducesResponseType(typeof(Contributor), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult AddContributor(
			[FromQuery]string loginToken,
			[FromBody]Contributor body)
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken);
			if (loggedInUser != null)
			{
				if (body != null)
				{
					body.AuditEnteredBy = loggedInUser; 
					body.AuditEntered = DateTime.Now; 
					body.AuditUpdatedBy = loggedInUser; 
					body.AuditUpdated = DateTime.Now; 
					body.Password = _tokenizer.GetSHA1HashData(body.Password); 
					_dbContext.Contributors.Add(body);
					_dbContext.SaveChanges();
				}
				if (body != null && body.Id != null) return new OkObjectResult(body);
				else return NotFound("Record was not added");
			}
			else return BadRequest("Invalid or expired login token");
		}

		/// <summary>
		/// Delete Contributor to the datastore
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="id">Id of the object that needs to be deleted from the datastore</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpDelete]
		[Route("/contributor")]
		[ProducesResponseType(typeof(Contributor), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult DeleteContributor(
			[FromQuery]string loginToken,
			[FromQuery]long? id)
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				if (id == null || id < 1) return new NotFoundResult(); 
				var body = _dbContext.Contributors.SingleOrDefault(m => m.Id == id); 
				_dbContext.Contributors.Remove(body); 
				_dbContext.SaveChanges(); 
				body = _dbContext.Contributors.SingleOrDefault(m => m.Id == id); 
				if (body == null)
				{ 
					return Ok("Record deleted"); 
				}
				else return BadRequest("Unable to delete object"); 
			} 
			else return BadRequest("Invalid or expired login token");
		}

		/// <summary>
		/// Retrieve Contributor from the datastore by Id
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///    If provided, the system verifies the user rights to access the data</param>
		/// <param name="id">Id of the object to retrieve from the datastore</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/contributor")]
		[ProducesResponseType(typeof(Contributor), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult GetContributor(
			[FromQuery]string loginToken,
			[FromQuery]long? id)
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				if (id == null || id < 1) return new OkObjectResult(default(Contributor)); 
				var body = _dbContext.Contributors
					// Include Parents
					// Include Children - comment out any that return large data sets or circular references
					//.Include("Papers")
					//.Include("PaperVersions")
					//.Include("Reviews")
					.SingleOrDefault(m => m.Id == id); 
				if (body == null) return NotFound(); 
				return new OkObjectResult(body); 
			} 
			else return BadRequest("Invalid or expired login token");
		}

		/// <summary>
		/// List Contributors from the data store with a startsWith filter
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data.
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="startsWith">the beginning of the text to find</param>
		/// <param name="pageSize">the page size of the result</param>
		/// <param name="pageNumber">the number of the page to retrieve, starting at 0</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/contributor/list")]
		[ProducesResponseType(typeof(Contributor), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult ListContributors(
			[FromQuery]string loginToken,
			[FromQuery]string startsWith,
			[FromQuery]int? pageSize,
			[FromQuery]int? pageNumber)
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				if (startsWith == null) startsWith = "";
				int skip = pageNumber??0; 
				int take = pageSize??100; 
				skip = skip * take; 
				var results = _dbContext.Contributors.Where(b => b.Id > 0 
					&& (startsWith.Length == 0 ||
					 (b.Id.ToString().Equals(startsWith) ||
						b.Name.StartsWith(startsWith) ||
						b.Description.StartsWith(startsWith) ||
						b.Degree.StartsWith(startsWith) ||
						b.AlmaMater.StartsWith(startsWith) ||
						b.Email.StartsWith(startsWith) ||
						b.Password.StartsWith(startsWith) ||
						b.Comments.StartsWith(startsWith) 
						))
					).Skip(skip).Take(take)
					// Include Parents
					.ToList(); 
				if (results == null || results.Count < 1) return NotFound(); 
				return new OkObjectResult(results); 
			} 
			else return BadRequest("Invalid or expired login token"); 
		}

		/// <summary>
		/// Count List Contributors results from the data store with a startsWith filter
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data.
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="startsWith">the beginning of the text to find</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/contributor/list/count")]
		[ProducesResponseType(typeof(Contributor), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult ListContributorsCount(
			[FromQuery]string loginToken,
			[FromQuery]string startsWith = "")
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				if (startsWith == null) startsWith = "";
				var results = _dbContext.Contributors.Where(b => b.Id > 0 
					&& (startsWith.Length == 0 ||
					   (b.Id.ToString().Equals(startsWith) ||
						b.Name.StartsWith(startsWith) ||
						b.Description.StartsWith(startsWith) ||
						b.Degree.StartsWith(startsWith) ||
						b.AlmaMater.StartsWith(startsWith) ||
						b.Email.StartsWith(startsWith) ||
						b.Password.StartsWith(startsWith) ||
						b.Comments.StartsWith(startsWith) 
						))
					).Take(10000).Count(); 
				if (results < 1) return NotFound(); 
				return new OkObjectResult(results); 
			} 
			else return BadRequest("Invalid or expired login token"); 
		}

		/// <summary>
		/// Search Contributors from the datastore with a Contains filter
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="q">the text to search for</param>
		/// <param name="pageSize">the page size of the result</param>
		/// <param name="pageNumber">the number of the page to retrieve, starting at 0</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/contributor/search")]
		[ProducesResponseType(typeof(Contributor), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult SearchContributors(
			[FromQuery]string loginToken,
			[FromQuery]string q,
			[FromQuery]int? pageSize,
			[FromQuery]int? pageNumber)
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				if (q == null) q = "";
				int skip = pageNumber??0; 
				int take = pageSize??100; 
				skip = skip * take; 
				var results = _dbContext.Contributors.Where(b => b.Id > 0 
					&& (q.Length == 0 ||
					   (b.Id.ToString().Equals(q) ||
						b.Name.Contains(q) ||
						b.Description.Contains(q) ||
						b.Degree.Contains(q) ||
						b.AlmaMater.Contains(q) ||
						b.Email.Contains(q) ||
						b.Password.Contains(q) ||
						b.Comments.Contains(q) 
						))
					).Skip(skip).Take(take)
					// Include Parents
					.ToList(); 
				if (results == null || results.Count < 1) return NotFound(); 
				return new OkObjectResult(results); 
			} 
			else return BadRequest("Invalid or expired login token"); 
		}

		/// <summary>
		/// Count Search Contributors results from the datastore with a Contains filter
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="q">the text to search for</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/contributor/search/count")]
		[ProducesResponseType(typeof(Contributor), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult SearchContributorsCount(
			[FromQuery]string loginToken,
			[FromQuery]string q = "")
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				if (q == null) q = "";
				var results = _dbContext.Contributors.Where(b => b.Id > 0 
					&& (q.Length == 0 ||
					   (b.Id.ToString().Equals(q) ||
						b.Name.Contains(q) ||
						b.Description.Contains(q) ||
						b.Degree.Contains(q) ||
						b.AlmaMater.Contains(q) ||
						b.Email.Contains(q) ||
						b.Password.Contains(q) ||
						b.Comments.Contains(q) 
						))
					).Take(10000).Count(); 
				if (results < 1) return NotFound(); 
				return new OkObjectResult(results); 
			} 
			else return BadRequest("Invalid or expired login token"); 
		}

		/// <summary>
		/// Select Contributors from the datastore with a list of ids
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="ids">A string list of Contributor Ids, separated by commas</param> 
		/// <response code="200">successful operation</response> 
		/// <response code="404">Category object not found</response> 
		[HttpGet]
		[Route("/contributor/select")]
		[ProducesResponseType(typeof(Contributor), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult SelectContributors(
			[FromQuery]string loginToken,
			[FromQuery]string ids) 
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser == null) return BadRequest("Invalid or expired Login Token"); 
			// prevent SQL injection 
			Regex digitsAndCommaOnly = new Regex(@"[^\d,]"); 
			var idList = digitsAndCommaOnly.Replace(ids, ""); 
			var results = _dbContext.Contributors.FromSql("Select * from [Contributors] where [Id] in (" + idList + "); ").ToList(); 
			if (results == null || results.Count < 1) return NotFound("No matching records found"); 
			return new OkObjectResult(results); 
		} 

		/// <summary>
		/// Update Contributor in the datastore
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="body">Contributor object that needs to be updated in the datastore</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpPut]
		[Route("/contributor")]
		[ProducesResponseType(typeof(Contributor), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult UpdateContributor(
			[FromQuery]string loginToken,
			[FromBody]Contributor body)
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken);
			if (loggedInUser != null)
			{
				var itemToUpdate = _dbContext.Contributors.AsNoTracking().SingleOrDefault(b => b.Id == body.Id);
				if (itemToUpdate != null) 
				{ 
					body.AuditEnteredBy = itemToUpdate.AuditEnteredBy; 
					body.AuditEntered = itemToUpdate.AuditEntered; 
					body.AuditUpdatedBy = loggedInUser; 
					body.AuditUpdated = DateTime.Now; 
					if (body.Password != null && !body.Password.Equals(itemToUpdate.Password)) 
						body.Password = _tokenizer.GetSHA1HashData(body.Password); 
					if (body.Name != null && !body.Name.Equals(itemToUpdate.Name)) 
						itemToUpdate.Name = body.Name; 
					if (body.Description != null && !body.Description.Equals(itemToUpdate.Description)) 
						itemToUpdate.Description = body.Description; 
					if (body.Degree != null && !body.Degree.Equals(itemToUpdate.Degree)) 
						itemToUpdate.Degree = body.Degree; 
					if (body.AlmaMater != null && !body.AlmaMater.Equals(itemToUpdate.AlmaMater)) 
						itemToUpdate.AlmaMater = body.AlmaMater; 
					if (body.Email != null && !body.Email.Equals(itemToUpdate.Email)) 
						itemToUpdate.Email = body.Email; 
					if (body.Evaluations != null && !body.Evaluations.Equals(itemToUpdate.Evaluations)) 
						itemToUpdate.Evaluations = body.Evaluations; 
					if (body.Password != null && !body.Password.Equals(itemToUpdate.Password)) 
						itemToUpdate.Password = body.Password; 
					if (body.Comments != null && !body.Comments.Equals(itemToUpdate.Comments)) 
						itemToUpdate.Comments = body.Comments; 
					if (body.AuditEntered != null && !body.AuditEntered.Equals(itemToUpdate.AuditEntered)) 
						itemToUpdate.AuditEntered = body.AuditEntered; 
					if (body.AuditEnteredBy != null && !body.AuditEnteredBy.Equals(itemToUpdate.AuditEnteredBy)) 
						itemToUpdate.AuditEnteredBy = body.AuditEnteredBy; 
					if (body.AuditUpdated != null && !body.AuditUpdated.Equals(itemToUpdate.AuditUpdated)) 
						itemToUpdate.AuditUpdated = body.AuditUpdated; 
					if (body.AuditUpdatedBy != null && !body.AuditUpdatedBy.Equals(itemToUpdate.AuditUpdatedBy)) 
						itemToUpdate.AuditUpdatedBy = body.AuditUpdatedBy; 
					body.AuditUpdatedBy = loggedInUser; 
					body.AuditUpdated = DateTime.Now; 
					_dbContext.SaveChanges(); 
					return Ok("Successful operation, no data returned"); 
				} 
				else return NotFound("Contributor not found"); 
			} 
			else return BadRequest("Invalid or expired login token"); 
		}

	} // End of Class ContributorApiController
} // End of Namespace Academy.Mentors.Controllers
