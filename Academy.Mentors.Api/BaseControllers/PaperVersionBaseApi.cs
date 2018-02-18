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
	/// PaperVersions RESTful API Controller (ASP.NET Core 2.0)
	/// </summary>
	[DataContract]
	public abstract class PaperVersionBaseApiController : Controller
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
		public PaperVersionBaseApiController(ApiDataContext apiDataContext, ILoggerFactory logger)
		{
			_logger = logger.CreateLogger<PaperVersionBaseApiController>(); 
			_dbContext = apiDataContext;
			_dbContext.Database.EnsureCreated();
		}

        /// <summary>
        /// Ping to verify PaperVersions Api Health
        /// </summary>
        /// <param name="pingText">Arbitrary text passed into the servie</param>
        /// <response code="200">Ping Text Reversed</response>
        [HttpGet]
        [Route("/paperVersion/ping")]
        [ProducesResponseType(typeof(string), 200)]
        public virtual IActionResult PingPaperVersions([FromQuery]string pingText)
        {
            var charArray = pingText.ToCharArray();
            Array.Reverse(charArray);
            return new OkObjectResult(new string(charArray));
        }

		/// <summary>
		/// Add new PaperVersion to the datastore
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="body">PaperVersion object that needs to be added to the datastore</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpPost]
		[Route("/paperVersion")]
		[ProducesResponseType(typeof(PaperVersion), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult AddPaperVersion(
			[FromQuery]string loginToken,
			[FromBody]PaperVersion body)
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
					_dbContext.PaperVersions.Add(body);
					_dbContext.SaveChanges();
				}
				if (body != null && body.Id != null) return new OkObjectResult(body);
				else return NotFound("Record was not added");
			}
			else return BadRequest("Invalid or expired login token");
		}

		/// <summary>
		/// Delete PaperVersion to the datastore
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="id">Id of the object that needs to be deleted from the datastore</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpDelete]
		[Route("/paperVersion")]
		[ProducesResponseType(typeof(PaperVersion), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult DeletePaperVersion(
			[FromQuery]string loginToken,
			[FromQuery]long? id)
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				if (id == null || id < 1) return new NotFoundResult(); 
				var body = _dbContext.PaperVersions.SingleOrDefault(m => m.Id == id); 
				_dbContext.PaperVersions.Remove(body); 
				_dbContext.SaveChanges(); 
				body = _dbContext.PaperVersions.SingleOrDefault(m => m.Id == id); 
				if (body == null)
				{ 
					return Ok("Record deleted"); 
				}
				else return BadRequest("Unable to delete object"); 
			} 
			else return BadRequest("Invalid or expired login token");
		}

		/// <summary>
		/// Retrieve PaperVersion from the datastore by Id
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///    If provided, the system verifies the user rights to access the data</param>
		/// <param name="id">Id of the object to retrieve from the datastore</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/paperVersion")]
		[ProducesResponseType(typeof(PaperVersion), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult GetPaperVersion(
			[FromQuery]string loginToken,
			[FromQuery]long? id)
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				if (id == null || id < 1) return new OkObjectResult(default(PaperVersion)); 
				var body = _dbContext.PaperVersions
					// Include Parents
					//.Include("Contributor")
					//.Include("Paper")
					// Include Children - comment out any that return large data sets or circular references
					//.Include("Reviews")
					.SingleOrDefault(m => m.Id == id); 
				if (body == null) return NotFound(); 
				return new OkObjectResult(body); 
			} 
			else return BadRequest("Invalid or expired login token");
		}

		/// <summary>
		/// List PaperVersions from the data store with a startsWith filter
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data.
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="startsWith">the beginning of the text to find</param>
		/// <param name="contributorId">Filter on contributorId (null or 0 to not filter)</param>
		/// <param name="paperId">Filter on paperId (null or 0 to not filter)</param>
		/// <param name="pageSize">the page size of the result</param>
		/// <param name="pageNumber">the number of the page to retrieve, starting at 0</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/paperVersion/list")]
		[ProducesResponseType(typeof(PaperVersion), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult ListPaperVersions(
			[FromQuery]string loginToken,
			[FromQuery]long? contributorId,
			[FromQuery]long? paperId,
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
				var results = _dbContext.PaperVersions.Where(b => b.Id > 0 
					&& (contributorId == null || b.ContributorId.Equals(contributorId))
					&& (paperId == null || b.PaperId.Equals(paperId))
					&& (startsWith.Length == 0 ||
					 (b.Id.ToString().Equals(startsWith) ||
						b.Name.StartsWith(startsWith) ||
						b.Description.StartsWith(startsWith) ||
						b.Content.StartsWith(startsWith) ||
						b.Comments.StartsWith(startsWith) 
						))
					).Skip(skip).Take(take)
					// Include Parents
					//.Include("Contributor")
					//.Include("Paper")
					.ToList(); 
				if (results == null || results.Count < 1) return NotFound(); 
				return new OkObjectResult(results); 
			} 
			else return BadRequest("Invalid or expired login token"); 
		}

		/// <summary>
		/// Count List PaperVersions results from the data store with a startsWith filter
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data.
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="startsWith">the beginning of the text to find</param>
		/// <param name="contributorId">Filter on contributorId (null or 0 to not filter)</param>
		/// <param name="paperId">Filter on paperId (null or 0 to not filter)</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/paperVersion/list/count")]
		[ProducesResponseType(typeof(PaperVersion), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult ListPaperVersionsCount(
			[FromQuery]string loginToken,
			[FromQuery]long? contributorId,
			[FromQuery]long? paperId,
			[FromQuery]string startsWith = "")
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				if (startsWith == null) startsWith = "";
				var results = _dbContext.PaperVersions.Where(b => b.Id > 0 
					&& (contributorId == null || b.ContributorId.Equals(contributorId))
					&& (paperId == null || b.PaperId.Equals(paperId))
					&& (startsWith.Length == 0 ||
					   (b.Id.ToString().Equals(startsWith) ||
						b.Name.StartsWith(startsWith) ||
						b.Description.StartsWith(startsWith) ||
						b.Content.StartsWith(startsWith) ||
						b.Comments.StartsWith(startsWith) 
						))
					).Take(10000).Count(); 
				if (results < 1) return NotFound(); 
				return new OkObjectResult(results); 
			} 
			else return BadRequest("Invalid or expired login token"); 
		}

		/// <summary>
		/// Search PaperVersions from the datastore with a Contains filter
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="contributorId">Filter on contributorId (null or 0 to not filter)</param>
		/// <param name="paperId">Filter on paperId (null or 0 to not filter)</param>
		/// <param name="q">the text to search for</param>
		/// <param name="pageSize">the page size of the result</param>
		/// <param name="pageNumber">the number of the page to retrieve, starting at 0</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/paperVersion/search")]
		[ProducesResponseType(typeof(PaperVersion), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult SearchPaperVersions(
			[FromQuery]string loginToken,
			[FromQuery]long? contributorId,
			[FromQuery]long? paperId,
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
				var results = _dbContext.PaperVersions.Where(b => b.Id > 0 
					&& (contributorId == null || b.ContributorId.Equals(contributorId))
					&& (paperId == null || b.PaperId.Equals(paperId))
					&& (q.Length == 0 ||
					   (b.Id.ToString().Equals(q) ||
						b.Name.Contains(q) ||
						b.Description.Contains(q) ||
						b.Content.Contains(q) ||
						b.Comments.Contains(q) 
						))
					).Skip(skip).Take(take)
					// Include Parents
					//.Include("Contributor")
					//.Include("Paper")
					.ToList(); 
				if (results == null || results.Count < 1) return NotFound(); 
				return new OkObjectResult(results); 
			} 
			else return BadRequest("Invalid or expired login token"); 
		}

		/// <summary>
		/// Count Search PaperVersions results from the datastore with a Contains filter
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="contributorId">Filter on contributorId (null or 0 to not filter)</param>
		/// <param name="paperId">Filter on paperId (null or 0 to not filter)</param>
		/// <param name="q">the text to search for</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/paperVersion/search/count")]
		[ProducesResponseType(typeof(PaperVersion), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult SearchPaperVersionsCount(
			[FromQuery]string loginToken,
			[FromQuery]long? contributorId,
			[FromQuery]long? paperId,
			[FromQuery]string q = "")
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				if (q == null) q = "";
				var results = _dbContext.PaperVersions.Where(b => b.Id > 0 
					&& (contributorId == null || b.ContributorId.Equals(contributorId))
					&& (paperId == null || b.PaperId.Equals(paperId))
					&& (q.Length == 0 ||
					   (b.Id.ToString().Equals(q) ||
						b.Name.Contains(q) ||
						b.Description.Contains(q) ||
						b.Content.Contains(q) ||
						b.Comments.Contains(q) 
						))
					).Take(10000).Count(); 
				if (results < 1) return NotFound(); 
				return new OkObjectResult(results); 
			} 
			else return BadRequest("Invalid or expired login token"); 
		}

		/// <summary>
		/// Select PaperVersions from the datastore with a list of ids
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="ids">A string list of PaperVersion Ids, separated by commas</param> 
		/// <response code="200">successful operation</response> 
		/// <response code="404">Category object not found</response> 
		[HttpGet]
		[Route("/paperVersion/select")]
		[ProducesResponseType(typeof(PaperVersion), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult SelectPaperVersions(
			[FromQuery]string loginToken,
			[FromQuery]string ids) 
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser == null) return BadRequest("Invalid or expired Login Token"); 
			// prevent SQL injection 
			Regex digitsAndCommaOnly = new Regex(@"[^\d,]"); 
			var idList = digitsAndCommaOnly.Replace(ids, ""); 
			var results = _dbContext.PaperVersions.FromSql("Select * from [PaperVersions] where [Id] in (" + idList + "); ").ToList(); 
			if (results == null || results.Count < 1) return NotFound("No matching records found"); 
			return new OkObjectResult(results); 
		} 

		/// <summary>
		/// Update PaperVersion in the datastore
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="body">PaperVersion object that needs to be updated in the datastore</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpPut]
		[Route("/paperVersion")]
		[ProducesResponseType(typeof(PaperVersion), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult UpdatePaperVersion(
			[FromQuery]string loginToken,
			[FromBody]PaperVersion body)
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken);
			if (loggedInUser != null)
			{
				var itemToUpdate = _dbContext.PaperVersions.AsNoTracking().SingleOrDefault(b => b.Id == body.Id);
				if (itemToUpdate != null) 
				{ 
					body.AuditEnteredBy = itemToUpdate.AuditEnteredBy; 
					body.AuditEntered = itemToUpdate.AuditEntered; 
					body.AuditUpdatedBy = loggedInUser; 
					body.AuditUpdated = DateTime.Now; 
					if (body.Name != null && !body.Name.Equals(itemToUpdate.Name)) 
						itemToUpdate.Name = body.Name; 
					if (body.Description != null && !body.Description.Equals(itemToUpdate.Description)) 
						itemToUpdate.Description = body.Description; 
					if (body.Content != null && !body.Content.Equals(itemToUpdate.Content)) 
						itemToUpdate.Content = body.Content; 
					if (body.ContributorId != null && !body.ContributorId.Equals(itemToUpdate.ContributorId)) 
						itemToUpdate.ContributorId = body.ContributorId; 
					if (body.PaperId != null && !body.PaperId.Equals(itemToUpdate.PaperId)) 
						itemToUpdate.PaperId = body.PaperId; 
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
				else return NotFound("PaperVersion not found"); 
			} 
			else return BadRequest("Invalid or expired login token"); 
		}

	} // End of Class PaperVersionApiController
} // End of Namespace Academy.Mentors.Controllers
