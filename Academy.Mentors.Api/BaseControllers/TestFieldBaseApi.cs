/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/


/* Powered by Solution Zone (http://www.solution.zone)  2/14/2018 1:37:44 PM */


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
	/// TestFields RESTful API Controller (ASP.NET Core 2.0)
	/// </summary>
	[DataContract]
	public abstract class TestFieldBaseApiController : Controller
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
		public TestFieldBaseApiController(ApiDataContext apiDataContext, ILoggerFactory logger)
		{
			_logger = logger.CreateLogger<TestFieldBaseApiController>(); 
			_dbContext = apiDataContext;
			_dbContext.Database.EnsureCreated();
		}

        /// <summary>
        /// Ping to verify TestFields Api Health
        /// </summary>
        /// <param name="pingText">Arbitrary text passed into the servie</param>
        /// <response code="200">Ping Text Reversed</response>
        [HttpGet]
        [Route("/testField/ping")]
        [ProducesResponseType(typeof(string), 200)]
        public virtual IActionResult PingTestFields([FromQuery]string pingText)
        {
            var charArray = pingText.ToCharArray();
            Array.Reverse(charArray);
            return new OkObjectResult(new string(charArray));
        }

		/// <summary>
		/// Add new TestField to the datastore
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="body">TestField object that needs to be added to the datastore</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpPost]
		[Route("/testField")]
		[ProducesResponseType(typeof(TestField), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult AddTestField(
			[FromQuery]string loginToken,
			[FromBody]TestField body)
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
					_dbContext.TestFields.Add(body);
					_dbContext.SaveChanges();
				}
				if (body != null && body.Id != null) return new OkObjectResult(body);
				else return NotFound("Record was not added");
			}
			else return BadRequest("Invalid or expired login token");
		}

		/// <summary>
		/// Delete TestField to the datastore
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="id">Id of the object that needs to be deleted from the datastore</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpDelete]
		[Route("/testField")]
		[ProducesResponseType(typeof(TestField), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult DeleteTestField(
			[FromQuery]string loginToken,
			[FromQuery]long? id)
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				if (id == null || id < 1) return new NotFoundResult(); 
				var body = _dbContext.TestFields.SingleOrDefault(m => m.Id == id); 
				_dbContext.TestFields.Remove(body); 
				_dbContext.SaveChanges(); 
				body = _dbContext.TestFields.SingleOrDefault(m => m.Id == id); 
				if (body == null)
				{ 
					return Ok("Record deleted"); 
				}
				else return BadRequest("Unable to delete object"); 
			} 
			else return BadRequest("Invalid or expired login token");
		}

		/// <summary>
		/// Retrieve TestField from the datastore by Id
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///    If provided, the system verifies the user rights to access the data</param>
		/// <param name="id">Id of the object to retrieve from the datastore</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/testField")]
		[ProducesResponseType(typeof(TestField), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult GetTestField(
			[FromQuery]string loginToken,
			[FromQuery]long? id)
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				if (id == null || id < 1) return new OkObjectResult(default(TestField)); 
				var body = _dbContext.TestFields
					// Include Parents
					// Include Children - comment out any that return large data sets or circular references
					.SingleOrDefault(m => m.Id == id); 
				if (body == null) return NotFound(); 
				return new OkObjectResult(body); 
			} 
			else return BadRequest("Invalid or expired login token");
		}

		/// <summary>
		/// List TestFields from the data store with a startsWith filter
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data.
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="startsWith">the beginning of the text to find</param>
		/// <param name="myBoolean">Filter on myBoolean (null to not filter)</param>
		/// <param name="pageSize">the page size of the result</param>
		/// <param name="pageNumber">the number of the page to retrieve, starting at 0</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/testField/list")]
		[ProducesResponseType(typeof(TestField), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult ListTestFields(
			[FromQuery]string loginToken,
			[FromQuery]bool? myBoolean,
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
				var results = _dbContext.TestFields.Where(b => b.Id > 0 
					&& (myBoolean == null || b.MyBoolean.Equals(myBoolean))
					&& (startsWith.Length == 0 ||
					 (b.Id.ToString().Equals(startsWith) ||
						b.Name.StartsWith(startsWith) ||
						b.Description.StartsWith(startsWith) ||
						b.MyCreditCard.StartsWith(startsWith) ||
						b.MyEmail.StartsWith(startsWith) ||
						b.MyImageUrl.StartsWith(startsWith) ||
						b.MyPhone.StartsWith(startsWith) ||
						b.MyPostalCode.StartsWith(startsWith) ||
						b.MyString.StartsWith(startsWith) ||
						b.MyTextArea.StartsWith(startsWith) ||
						b.MyUrl.StartsWith(startsWith) ||
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
		/// Count List TestFields results from the data store with a startsWith filter
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data.
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="startsWith">the beginning of the text to find</param>
		/// <param name="myBoolean">Filter on myBoolean (null to not filter)</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/testField/list/count")]
		[ProducesResponseType(typeof(TestField), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult ListTestFieldsCount(
			[FromQuery]string loginToken,
			[FromQuery]bool? myBoolean,
			[FromQuery]string startsWith = "")
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				if (startsWith == null) startsWith = "";
				var results = _dbContext.TestFields.Where(b => b.Id > 0 
					&& (myBoolean == null || b.MyBoolean.Equals(myBoolean))
					&& (startsWith.Length == 0 ||
					   (b.Id.ToString().Equals(startsWith) ||
						b.Name.StartsWith(startsWith) ||
						b.Description.StartsWith(startsWith) ||
						b.MyCreditCard.StartsWith(startsWith) ||
						b.MyEmail.StartsWith(startsWith) ||
						b.MyImageUrl.StartsWith(startsWith) ||
						b.MyPhone.StartsWith(startsWith) ||
						b.MyPostalCode.StartsWith(startsWith) ||
						b.MyString.StartsWith(startsWith) ||
						b.MyTextArea.StartsWith(startsWith) ||
						b.MyUrl.StartsWith(startsWith) ||
						b.Comments.StartsWith(startsWith) 
						))
					).Take(10000).Count(); 
				if (results < 1) return NotFound(); 
				return new OkObjectResult(results); 
			} 
			else return BadRequest("Invalid or expired login token"); 
		}

		/// <summary>
		/// Search TestFields from the datastore with a Contains filter
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="myBoolean">Filter on myBoolean (null to not filter)</param>
		/// <param name="q">the text to search for</param>
		/// <param name="pageSize">the page size of the result</param>
		/// <param name="pageNumber">the number of the page to retrieve, starting at 0</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/testField/search")]
		[ProducesResponseType(typeof(TestField), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult SearchTestFields(
			[FromQuery]string loginToken,
			[FromQuery]bool? myBoolean,
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
				var results = _dbContext.TestFields.Where(b => b.Id > 0 
					&& (myBoolean == null || b.MyBoolean.Equals(myBoolean))
					&& (q.Length == 0 ||
					   (b.Id.ToString().Equals(q) ||
						b.Name.Contains(q) ||
						b.Description.Contains(q) ||
						b.MyCreditCard.Contains(q) ||
						b.MyEmail.Contains(q) ||
						b.MyImageUrl.Contains(q) ||
						b.MyPhone.Contains(q) ||
						b.MyPostalCode.Contains(q) ||
						b.MyString.Contains(q) ||
						b.MyTextArea.Contains(q) ||
						b.MyUrl.Contains(q) ||
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
		/// Count Search TestFields results from the datastore with a Contains filter
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="myBoolean">Filter on myBoolean (null to not filter)</param>
		/// <param name="q">the text to search for</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpGet]
		[Route("/testField/search/count")]
		[ProducesResponseType(typeof(TestField), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult SearchTestFieldsCount(
			[FromQuery]string loginToken,
			[FromQuery]bool? myBoolean,
			[FromQuery]string q = "")
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser != null) 
			{ 
				if (q == null) q = "";
				var results = _dbContext.TestFields.Where(b => b.Id > 0 
					&& (myBoolean == null || b.MyBoolean.Equals(myBoolean))
					&& (q.Length == 0 ||
					   (b.Id.ToString().Equals(q) ||
						b.Name.Contains(q) ||
						b.Description.Contains(q) ||
						b.MyCreditCard.Contains(q) ||
						b.MyEmail.Contains(q) ||
						b.MyImageUrl.Contains(q) ||
						b.MyPhone.Contains(q) ||
						b.MyPostalCode.Contains(q) ||
						b.MyString.Contains(q) ||
						b.MyTextArea.Contains(q) ||
						b.MyUrl.Contains(q) ||
						b.Comments.Contains(q) 
						))
					).Take(10000).Count(); 
				if (results < 1) return NotFound(); 
				return new OkObjectResult(results); 
			} 
			else return BadRequest("Invalid or expired login token"); 
		}

		/// <summary>
		/// Select TestFields from the datastore with a list of ids
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="ids">A string list of TestField Ids, separated by commas</param> 
		/// <response code="200">successful operation</response> 
		/// <response code="404">Category object not found</response> 
		[HttpGet]
		[Route("/testField/select")]
		[ProducesResponseType(typeof(TestField), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult SelectTestFields(
			[FromQuery]string loginToken,
			[FromQuery]string ids) 
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken); 
			if (loggedInUser == null) return BadRequest("Invalid or expired Login Token"); 
			// prevent SQL injection 
			Regex digitsAndCommaOnly = new Regex(@"[^\d,]"); 
			var idList = digitsAndCommaOnly.Replace(ids, ""); 
			var results = _dbContext.TestFields.FromSql("Select * from [TestFields] where [Id] in (" + idList + "); ").ToList(); 
			if (results == null || results.Count < 1) return NotFound("No matching records found"); 
			return new OkObjectResult(results); 
		} 

		/// <summary>
		/// Update TestField in the datastore
		/// </summary>
		/// <param name="loginToken">The token for the user requesting this data. 
		///	 If provided, the system verifies the user rights to access the data</param>
		/// <param name="body">TestField object that needs to be updated in the datastore</param>
		/// <response code="200">successful operation</response>
		/// <response code="405">Invalid input</response>
		[HttpPut]
		[Route("/testField")]
		[ProducesResponseType(typeof(TestField), 200)]
		[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
		public virtual IActionResult UpdateTestField(
			[FromQuery]string loginToken,
			[FromBody]TestField body)
		{
			if (string.IsNullOrEmpty(loginToken)) return BadRequest("Login token is required"); 
			var loggedInUser = _tokenizer.ValidateToken(loginToken);
			if (loggedInUser != null)
			{
				var itemToUpdate = _dbContext.TestFields.AsNoTracking().SingleOrDefault(b => b.Id == body.Id);
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
					if (body.MyBoolean != null && !body.MyBoolean.Equals(itemToUpdate.MyBoolean)) 
						itemToUpdate.MyBoolean = body.MyBoolean; 
					if (body.MyCreditCard != null && !body.MyCreditCard.Equals(itemToUpdate.MyCreditCard)) 
						itemToUpdate.MyCreditCard = body.MyCreditCard; 
					if (body.MyCurrency != null && !body.MyCurrency.Equals(itemToUpdate.MyCurrency)) 
						itemToUpdate.MyCurrency = body.MyCurrency; 
					if (body.MyDateTime != null && !body.MyDateTime.Equals(itemToUpdate.MyDateTime)) 
						itemToUpdate.MyDateTime = body.MyDateTime; 
					if (body.MyDouble != null && !body.MyDouble.Equals(itemToUpdate.MyDouble)) 
						itemToUpdate.MyDouble = body.MyDouble; 
					if (body.MyEmail != null && !body.MyEmail.Equals(itemToUpdate.MyEmail)) 
						itemToUpdate.MyEmail = body.MyEmail; 
					if (body.MyFloat != null && !body.MyFloat.Equals(itemToUpdate.MyFloat)) 
						itemToUpdate.MyFloat = body.MyFloat; 
					if (body.MyImageUrl != null && !body.MyImageUrl.Equals(itemToUpdate.MyImageUrl)) 
						itemToUpdate.MyImageUrl = body.MyImageUrl; 
					if (body.MyInteger != null && !body.MyInteger.Equals(itemToUpdate.MyInteger)) 
						itemToUpdate.MyInteger = body.MyInteger; 
					if (body.MyLong != null && !body.MyLong.Equals(itemToUpdate.MyLong)) 
						itemToUpdate.MyLong = body.MyLong; 
					if (body.MyPhone != null && !body.MyPhone.Equals(itemToUpdate.MyPhone)) 
						itemToUpdate.MyPhone = body.MyPhone; 
					if (body.MyPostalCode != null && !body.MyPostalCode.Equals(itemToUpdate.MyPostalCode)) 
						itemToUpdate.MyPostalCode = body.MyPostalCode; 
					if (body.MyString != null && !body.MyString.Equals(itemToUpdate.MyString)) 
						itemToUpdate.MyString = body.MyString; 
					if (body.MyTextArea != null && !body.MyTextArea.Equals(itemToUpdate.MyTextArea)) 
						itemToUpdate.MyTextArea = body.MyTextArea; 
					if (body.MyTicks != null && !body.MyTicks.Equals(itemToUpdate.MyTicks)) 
						itemToUpdate.MyTicks = body.MyTicks; 
					if (body.MyUrl != null && !body.MyUrl.Equals(itemToUpdate.MyUrl)) 
						itemToUpdate.MyUrl = body.MyUrl; 
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
				else return NotFound("TestField not found"); 
			} 
			else return BadRequest("Invalid or expired login token"); 
		}

	} // End of Class TestFieldApiController
} // End of Namespace Academy.Mentors.Controllers
