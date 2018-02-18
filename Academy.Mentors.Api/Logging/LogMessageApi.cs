/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using Academy.Mentors.Api.Logging ;
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
	/// LogMessages RESTful API Controller (ASP.NET Core 2.0)
	/// </summary>
       [ApiExplorerSettings(IgnoreApi = true)]
	[DataContract]
	public class LogMessageApiController : Controller
	{
		/// <summary>
		/// Entity Framework DbContext for this controller
		/// </summary>
		protected LogDataContext _dbContext;

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
		/// <param name="logDataContext">API Data Context</param>
		/// <param name="logger">ILoggerFactory</param>
		public LogMessageApiController(LogDataContext logDataContext, ILoggerFactory logger)
		{
			_logger = logger.CreateLogger<LogMessageApiController>(); 
			_dbContext = logDataContext;
			_dbContext.Database.EnsureCreated();
		}

        /// <summary>
        /// Ping to verify LogMessages Api Health
        /// </summary>
        /// <param name="pingText">Arbitrary text passed into the servie</param>
        /// <response code="200">Ping Text Reversed</response>
        [HttpGet]
        [Route("/logMessage/ping")]
        [ProducesResponseType(typeof(string), 200)]
        public virtual IActionResult PingLogMessages([FromQuery]string pingText)
        {
            var charArray = pingText.ToCharArray();
            Array.Reverse(charArray);
            return new OkObjectResult(new string(charArray));
        }

		/// <summary>
		/// Add new LogMessage to the datastore
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="body">LogMessage object that needs to be added to the datastore</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpPost]
		[Route("/logMessage")]
		[ProducesResponseType(typeof(LogMessage), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult AddLogMessage(
			[FromQuery]string loginToken,
			[FromBody]LogMessage body)
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken);
			if (loggedInUser != null)
			{
				if (body != null)
				{
					_dbContext.LogMessages.Add(body);
					_dbContext.SaveChanges();
				}
				if (body != null && body.Id != null) return new OkObjectResult(body);
				else return NotFound("Record was not added");
			}
			else return BadRequest("Invalid or expired login token");
		}

		/// <summary>
		/// Delete LogMessage to the datastore
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="id">Id of the object that needs to be deleted from the datastore</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpDelete]
		[Route("/logMessage")]
		[ProducesResponseType(typeof(LogMessage), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult DeleteLogMessage(
			[FromQuery]string loginToken,
			[FromQuery]long? id)
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				if (id == null || id < 1) return new NotFoundResult(); 
				var body = _dbContext.LogMessages.SingleOrDefault(m => m.Id == id); 
				_dbContext.LogMessages.Remove(body); 
				_dbContext.SaveChanges(); 
				body = _dbContext.LogMessages.SingleOrDefault(m => m.Id == id); 
				if (body == null)
				{ 
					return Ok("Record deleted"); 
				}
				else return BadRequest("Unable to delete object"); 
			} 
			else return BadRequest("Invalid or expired login token");
		}

		/// <summary>
		/// Retrieve LogMessage from the datastore by Id
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///    If provided, the system verifies the user rights to access the data</param>
		/// <param name="id">Id of the object to retrieve from the datastore</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/logMessage")]
		[ProducesResponseType(typeof(LogMessage), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult GetLogMessage(
			[FromQuery]string loginToken,
			[FromQuery]long? id)
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				if (id == null || id < 1) return new OkObjectResult(default(LogMessage)); 
				var body = _dbContext.LogMessages
					// Include Parents
					//.Include("LogMessageType")
					// Include Children - comment out any that return large data sets or circular references
					.SingleOrDefault(m => m.Id == id); 
				if (body == null) return NotFound(); 
				return new OkObjectResult(body); 
			} 
			else return BadRequest("Invalid or expired login token");
		}

		/// <summary>
		/// List LogMessages from the data store with a startsWith filter
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data.
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="startsWith">the beginning of the text to find</param>
		/// <param name="logMessageTypeId">Filter on logMessageTypeId (null or 0 to not filter)</param>
		/// <param name="pageSize">the page size of the result</param>
		/// <param name="pageNumber">the number of the page to retrieve, starting at 0</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/logMessage/list")]
		[ProducesResponseType(typeof(LogMessage), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult ListLogMessages(
			[FromQuery]string loginToken,
			[FromQuery]long? logMessageTypeId,
			[FromQuery]string startsWith = "",
			[FromQuery]int pageSize = 100,
			[FromQuery]int pageNumber = 0)
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				int skip = pageNumber; 
				int take = pageSize; 
				skip = skip * take; 
				var results = _dbContext.LogMessages.Where(b => b.Id > 0 
					&& (logMessageTypeId == null || b.LogMessageTypeId.Equals(logMessageTypeId))
					&& (b.Id.ToString().Equals(startsWith) ||
						b.ApplicationName.StartsWith(startsWith) ||
						b.ApplicationMethod.StartsWith(startsWith) ||
						b.IpAddress.StartsWith(startsWith) ||
						b.LoginToken.StartsWith(startsWith) ||
						b.ShortMessage.StartsWith(startsWith) ||
						b.RequestHttpMethod.StartsWith(startsWith) ||
						b.RequestUri.StartsWith(startsWith) ||
						b.RequestParams.StartsWith(startsWith) ||
						b.RequestBody.StartsWith(startsWith) ||
						b.ResponseContent.StartsWith(startsWith) ||
						b.FullMessage.StartsWith(startsWith) ||
						b.Exception.StartsWith(startsWith) ||
						b.Trace.StartsWith(startsWith) 
						)
					).Skip(skip).Take(take)
					// Include Parents
					//.Include("LogMessageType")
					.ToList(); 
				if (results == null || results.Count < 1) return NotFound(); 
				return new OkObjectResult(results); 
			} 
			else return BadRequest("Invalid or expired login token"); 
		}

		/// <summary>
		/// Count List LogMessages results from the data store with a startsWith filter
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data.
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="startsWith">the beginning of the text to find</param>
		/// <param name="logMessageTypeId">Filter on logMessageTypeId (null or 0 to not filter)</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/logMessage/list/count")]
		[ProducesResponseType(typeof(LogMessage), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult ListLogMessagesCount(
			[FromQuery]string loginToken,
			[FromQuery]long? logMessageTypeId,
			[FromQuery]string startsWith = "")
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				var results = _dbContext.LogMessages.Where(b => b.Id > 0 
					&& (logMessageTypeId == null || b.LogMessageTypeId.Equals(logMessageTypeId))
					&& (b.Id.ToString().Equals(startsWith) ||
						b.ApplicationName.StartsWith(startsWith) ||
						b.ApplicationMethod.StartsWith(startsWith) ||
						b.IpAddress.StartsWith(startsWith) ||
						b.LoginToken.StartsWith(startsWith) ||
						b.ShortMessage.StartsWith(startsWith) ||
						b.RequestHttpMethod.StartsWith(startsWith) ||
						b.RequestUri.StartsWith(startsWith) ||
						b.RequestParams.StartsWith(startsWith) ||
						b.RequestBody.StartsWith(startsWith) ||
						b.ResponseContent.StartsWith(startsWith) ||
						b.FullMessage.StartsWith(startsWith) ||
						b.Exception.StartsWith(startsWith) ||
						b.Trace.StartsWith(startsWith) 
						)
					).Take(10000).Count(); 
				if (results < 1) return NotFound(); 
				return new OkObjectResult(results); 
			} 
			else return BadRequest("Invalid or expired login token"); 
		}

		/// <summary>
		/// Search LogMessages from the datastore with a Contains filter
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="logMessageTypeId">Filter on logMessageTypeId (null or 0 to not filter)</param>
		/// <param name="q">the text to search for</param>
		/// <param name="pageSize">the page size of the result</param>
		/// <param name="pageNumber">the number of the page to retrieve, starting at 0</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/logMessage/search")]
		[ProducesResponseType(typeof(LogMessage), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult SearchLogMessages(
			[FromQuery]string loginToken,
			[FromQuery]long? logMessageTypeId,
			[FromQuery]string q = "",
			[FromQuery]int pageSize = 100,
			[FromQuery]int pageNumber = 0)
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				int skip = pageNumber; 
				int take = pageSize; 
				skip = skip * take; 
				var results = _dbContext.LogMessages.Where(b => b.Id > 0 
					&& (logMessageTypeId == null || b.LogMessageTypeId.Equals(logMessageTypeId))
					&& (b.Id.ToString().Equals(q) ||
						b.ApplicationName.Contains(q) ||
						b.ApplicationMethod.Contains(q) ||
						b.IpAddress.Contains(q) ||
						b.LoginToken.Contains(q) ||
						b.ShortMessage.Contains(q) ||
						b.RequestHttpMethod.Contains(q) ||
						b.RequestUri.Contains(q) ||
						b.RequestParams.Contains(q) ||
						b.RequestBody.Contains(q) ||
						b.ResponseContent.Contains(q) ||
						b.FullMessage.Contains(q) ||
						b.Exception.Contains(q) ||
						b.Trace.Contains(q) 
						)
					).Skip(skip).Take(take)
					// Include Parents
					//.Include("LogMessageType")
					.ToList(); 
				if (results == null || results.Count < 1) return NotFound(); 
				return new OkObjectResult(results); 
			} 
			else return BadRequest("Invalid or expired login token"); 
		}

		/// <summary>
		/// Count Search LogMessages results from the datastore with a Contains filter
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="logMessageTypeId">Filter on logMessageTypeId (null or 0 to not filter)</param>
		/// <param name="q">the text to search for</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/logMessage/search/count")]
		[ProducesResponseType(typeof(LogMessage), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult SearchLogMessagesCount(
			[FromQuery]string loginToken,
			[FromQuery]long? logMessageTypeId,
			[FromQuery]string q = "")
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				var results = _dbContext.LogMessages.Where(b => b.Id > 0 
					&& (logMessageTypeId == null || b.LogMessageTypeId.Equals(logMessageTypeId))
					&& (b.Id.ToString().Equals(q) ||
						b.ApplicationName.Contains(q) ||
						b.ApplicationMethod.Contains(q) ||
						b.IpAddress.Contains(q) ||
						b.LoginToken.Contains(q) ||
						b.ShortMessage.Contains(q) ||
						b.RequestHttpMethod.Contains(q) ||
						b.RequestUri.Contains(q) ||
						b.RequestParams.Contains(q) ||
						b.RequestBody.Contains(q) ||
						b.ResponseContent.Contains(q) ||
						b.FullMessage.Contains(q) ||
						b.Exception.Contains(q) ||
						b.Trace.Contains(q) 
						)
					).Take(10000).Count(); 
				if (results < 1) return NotFound(); 
				return new OkObjectResult(results); 
			} 
			else return BadRequest("Invalid or expired login token"); 
		}

		/// <summary>
		/// Select LogMessages from the datastore with a list of ids
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="ids">A string list of LogMessage Ids, separated by commas</param> 
		/// <response code="200">successful operation</response> 
		/// <response code="404">Category object not found</response> 
		[HttpGet]
		[Route("/logMessage/select")]
		[ProducesResponseType(typeof(LogMessage), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult SelectLogMessages(
			[FromQuery]string loginToken,
			[FromQuery]string ids) 
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser == null) return BadRequest("Invalid or expired Login Token"); 
			// prevent SQL injection 
			Regex digitsAndCommaOnly = new Regex(@"[^\d,]"); 
			var idList = digitsAndCommaOnly.Replace(ids, ""); 
			var results = _dbContext.LogMessages.FromSql("Select * from LogMessages where LogMessages.Id in (" + idList + "); ").ToList(); 
			if (results == null || results.Count < 1) return NotFound("No matching records found"); 
			return new OkObjectResult(results); 
		} 

		/// <summary>
		/// Update LogMessage in the datastore
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="body">LogMessage object that needs to be updated in the datastore</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpPut]
		[Route("/logMessage")]
		[ProducesResponseType(typeof(LogMessage), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult UpdateLogMessage(
			[FromQuery]string loginToken,
			[FromBody]LogMessage body)
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken);
			if (loggedInUser != null)
			{
				var itemToUpdate = _dbContext.LogMessages.Single(b => b.Id == body.Id);
				if (itemToUpdate != null) 
				{ 
					if (body.LogMessageTypeId != null && !body.LogMessageTypeId.Equals(itemToUpdate.LogMessageTypeId)) 
						itemToUpdate.LogMessageTypeId = body.LogMessageTypeId; 
					if (body.ApplicationName != null && !body.ApplicationName.Equals(itemToUpdate.ApplicationName)) 
						itemToUpdate.ApplicationName = body.ApplicationName; 
					if (body.ApplicationMethod != null && !body.ApplicationMethod.Equals(itemToUpdate.ApplicationMethod)) 
						itemToUpdate.ApplicationMethod = body.ApplicationMethod; 
					if (body.IpAddress != null && !body.IpAddress.Equals(itemToUpdate.IpAddress)) 
						itemToUpdate.IpAddress = body.IpAddress; 
					if (body.LoginToken != null && !body.LoginToken.Equals(itemToUpdate.LoginToken)) 
						itemToUpdate.LoginToken = body.LoginToken; 
					if (body.ShortMessage != null && !body.ShortMessage.Equals(itemToUpdate.ShortMessage)) 
						itemToUpdate.ShortMessage = body.ShortMessage; 
					if (body.RequestHttpMethod != null && !body.RequestHttpMethod.Equals(itemToUpdate.RequestHttpMethod)) 
						itemToUpdate.RequestHttpMethod = body.RequestHttpMethod; 
					if (body.RequestUri != null && !body.RequestUri.Equals(itemToUpdate.RequestUri)) 
						itemToUpdate.RequestUri = body.RequestUri; 
					if (body.RequestParams != null && !body.RequestParams.Equals(itemToUpdate.RequestParams)) 
						itemToUpdate.RequestParams = body.RequestParams; 
					if (body.RequestBody != null && !body.RequestBody.Equals(itemToUpdate.RequestBody)) 
						itemToUpdate.RequestBody = body.RequestBody; 
					if (body.StatusCode != null && !body.StatusCode.Equals(itemToUpdate.StatusCode)) 
						itemToUpdate.StatusCode = body.StatusCode; 
					if (body.ResponseContent != null && !body.ResponseContent.Equals(itemToUpdate.ResponseContent)) 
						itemToUpdate.ResponseContent = body.ResponseContent; 
					if (body.FullMessage != null && !body.FullMessage.Equals(itemToUpdate.FullMessage)) 
						itemToUpdate.FullMessage = body.FullMessage; 
					if (body.Exception != null && !body.Exception.Equals(itemToUpdate.Exception)) 
						itemToUpdate.Exception = body.Exception; 
					if (body.Trace != null && !body.Trace.Equals(itemToUpdate.Trace)) 
						itemToUpdate.Trace = body.Trace; 
					if (body.Logged != null && !body.Logged.Equals(itemToUpdate.Logged)) 
						itemToUpdate.Logged = body.Logged; 
					_dbContext.SaveChanges(); 
					return Ok("Successful operation, no data returned"); 
				} 
				else return NotFound("LogMessage not found"); 
			} 
			else return BadRequest("Invalid or expired login token"); 
		}

	} // End of Class LogMessageApiController
} // End of Namespace Academy.Mentors.Api.Logging
