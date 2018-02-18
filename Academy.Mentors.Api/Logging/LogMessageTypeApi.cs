/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Academy.Mentors.Api.Logging
{
    /// <summary>
    /// LogMessageTypes RESTful API Controller (ASP.NET Core) 
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    [DataContract]
    public class LogMessageTypeApiController : Controller
    {
        private LogDataContext _dbContext;

        /// <summary>
        /// Constructor for Controller
        /// </summary>
        /// <param name="logDataContext"></param>
        public LogMessageTypeApiController(LogDataContext logDataContext)
        {
            _dbContext = logDataContext;
            _dbContext.Database.EnsureCreated();
        }

        /// <summary>
        /// Add new LogMessageType to the datastore
        /// </summary>
        /// <param name="loginToken">The token for the user requesting this data. 
        ///	 If provided, the system verifies the user rights to access the data</param>
        /// <param name="body">LogMessageType object that needs to be added to the datastore</param>
        /// <response code="200">successful operation</response>
        /// <response code="405">Invalid input</response>
        [HttpPost]
        [Route("/logMessageType")]
        [ProducesResponseType(typeof(LogMessageType), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        public virtual IActionResult AddLogMessageType(
            [FromQuery]string loginToken,
            [FromBody]LogMessageType body)
        {
            if (body != null)
            {
                _dbContext.LogMessageTypes.Add(body);
                _dbContext.SaveChanges();
            }
            if (body != null && body.Id != null) return new OkObjectResult(body);
            else return NotFound();
        }

        /// <summary>
        /// Delete LogMessageType to the datastore
        /// </summary>
        /// <param name="loginToken">The token for the user requesting this data. 
        ///	 If provided, the system verifies the user rights to access the data</param>
        /// <param name="id">Id of the object that needs to be deleted from the datastore</param>
        /// <response code="200">successful operation</response>
        /// <response code="405">Invalid input</response>
        [HttpDelete]
        [Route("/logMessageType")]
        [ProducesResponseType(typeof(LogMessageType), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        public virtual IActionResult DeleteLogMessageType(
            [FromQuery]string loginToken,
            [FromQuery]long? id)
        {
            if (id == null || id < 1) return new NotFoundResult();
            var body = _dbContext.LogMessageTypes.SingleOrDefault(m => m.Id == id);
            _dbContext.LogMessageTypes.Remove(body);
            _dbContext.SaveChanges();
            body = _dbContext.LogMessageTypes.SingleOrDefault(m => m.Id == id);
            if (body == null)
            {
                return Ok("Deleted successfully");
            }
            else return BadRequest("Unable to delete object");
        }

        /// <summary>
        /// Retrieve LogMessageType from the datastore by Id
        /// </summary>
        /// <param name="loginToken">The token for the user requesting this data. 
        ///    If provided, the system verifies the user rights to access the data</param>
        /// <param name="id">Id of the object to retrieve from the datastore</param>
        /// <response code="200">successful operation</response>
        /// <response code="405">Invalid input</response>
        [HttpGet]
        [Route("/logMessageType")]
        [ProducesResponseType(typeof(LogMessageType), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        public virtual IActionResult GetLogMessageType(
            [FromQuery]string loginToken,
            [FromQuery]long? id)
        {
            if (id == null || id < 1) return new OkObjectResult(default(LogMessageType));
            var body = _dbContext.LogMessageTypes
                // Include Parents
                // Include Children - comment out any that return large data sets or circular references
                //.Include("LogMessages")
                .SingleOrDefault(m => m.Id == id);
            if (body == null) return NotFound();
            return new OkObjectResult(body);
        }

        /// <summary>
        /// List LogMessageTypes from the datastore with a startsWith filter
        /// </summary>
        /// <param name="loginToken">The token for the user requesting this data.
        ///	 If provided, the system verifies the user rights to access the data</param>
        /// <param name="startsWith">the beginning of the text to find</param>
        /// <param name="pageSize">the page size of the result</param>
        /// <param name="pageNumber">the number of the page to retrieve, starting at 0</param>
        /// <response code="200">successful operation</response>
        /// <response code="405">Invalid input</response>
        [HttpGet]
        [Route("/logMessageType/list")]
        [ProducesResponseType(typeof(LogMessageType), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        public virtual IActionResult ListLogMessageTypes(
            [FromQuery]string loginToken,
            [FromQuery]string startsWith = "",
            [FromQuery]int pageSize = 100,
            [FromQuery]int pageNumber = 0)
        {
            int skip = pageNumber;
            int take = pageSize;
            skip = skip * take;
            var results = _dbContext.LogMessageTypes.Where(b => b.Id > 0
                && (
                    b.Name.StartsWith(startsWith) ||
                    b.Description.StartsWith(startsWith)
                    )
                ).Skip(skip).Take(take)
                // Include Parents
                .ToList();
            if (results == null || results.Count < 1) return NotFound();
            return new OkObjectResult(results);
        }

        /// <summary>
        /// Count List LogMessageTypes results from the datastore with a startsWith filter
        /// </summary>
        /// <param name="loginToken">The token for the user requesting this data.
        ///	 If provided, the system verifies the user rights to access the data</param>
        /// <param name="startsWith">the beginning of the text to find</param>
        /// <response code="200">successful operation</response>
        /// <response code="405">Invalid input</response>
        [HttpGet]
        [Route("/logMessageType/list/count")]
        [ProducesResponseType(typeof(LogMessageType), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        public virtual IActionResult ListLogMessageTypesCount(
            [FromQuery]string loginToken,
            [FromQuery]string startsWith = "")
        {
            var results = _dbContext.LogMessageTypes.Where(b => b.Id > 0
                && (
                    b.Name.StartsWith(startsWith) ||
                    b.Description.StartsWith(startsWith)
                    )
                ).Take(10000).Count();
            if (results < 1) return NotFound();
            return new OkObjectResult(results);
        }

        /// <summary>
        /// Search LogMessageTypes from the datastore with a Contains filter
        /// </summary>
        /// <param name="loginToken">The token for the user requesting this data. 
        ///	 If provided, the system verifies the user rights to access the data</param>
        /// <param name="q">the text to search for</param>
        /// <param name="pageSize">the page size of the result</param>
        /// <param name="pageNumber">the number of the page to retrieve, starting at 0</param>
        /// <response code="200">successful operation</response>
        /// <response code="405">Invalid input</response>
        [HttpGet]
        [Route("/logMessageType/search")]
        [ProducesResponseType(typeof(LogMessageType), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        public virtual IActionResult SearchLogMessageTypes(
            [FromQuery]string loginToken,
            [FromQuery]string q = "",
            [FromQuery]int pageSize = 100,
            [FromQuery]int pageNumber = 0)
        {
            int skip = pageNumber;
            int take = pageSize;
            skip = skip * take;
            var results = _dbContext.LogMessageTypes.Where(b => b.Id > 0
                && (
                    b.Name.Contains(q) ||
                    b.Description.Contains(q)
                    )
                ).Skip(skip).Take(take)
                // Include Parents
                .ToList();
            if (results == null || results.Count < 1) return NotFound();
            return new OkObjectResult(results);
        }

        /// <summary>
        /// Count Search LogMessageTypes results from the datastore with a Contains filter
        /// </summary>
        /// <param name="loginToken">The token for the user requesting this data. 
        ///	 If provided, the system verifies the user rights to access the data</param>
        /// <param name="q">the text to search for</param>
        /// <response code="200">successful operation</response>
        /// <response code="405">Invalid input</response>
        [HttpGet]
        [Route("/logMessageType/search/count")]
        [ProducesResponseType(typeof(LogMessageType), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        public virtual IActionResult SearchLogMessageTypesCount(
            [FromQuery]string loginToken,
            [FromQuery]string q = "")
        {
            var results = _dbContext.LogMessageTypes.Where(b => b.Id > 0
                && (
                    b.Name.Contains(q) ||
                    b.Description.Contains(q)
                    )
                ).Take(10000).Count();
            if (results < 1) return NotFound();
            return new OkObjectResult(results);
        }

        /// <summary>
        /// Select LogMessageTypes from the datastore with a list of ids
        /// </summary>
        /// <param name="loginToken">The token for the user requesting this data. 
        ///	 If provided, the system verifies the user rights to access the data</param>
        /// <param name="ids">A string list of LogMessageType Ids, separated by commas</param> 
        /// <response code="200">successful operation</response> 
        /// <response code="404">Category object not found</response> 
        [HttpGet]
        [Route("/logMessageType/select")]
        [ProducesResponseType(typeof(LogMessageType), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        public virtual IActionResult SelectLogMessageTypes(
            [FromQuery]string loginToken,
            [FromQuery]string ids)
        {
            // prevent SQL injection 
            Regex digitsAndCommaOnly = new Regex(@"[^\d,]");
            var idList = digitsAndCommaOnly.Replace(ids, "");
            var results = _dbContext.LogMessageTypes.FromSql("Select * from LogMessageTypes where LogMessageTypes.Id in (" + idList + "); ").ToList();
            if (results == null || results.Count < 1) return NotFound();
            return new OkObjectResult(results);
        }

        /// <summary>
        /// Update LogMessageType in the datastore
        /// </summary>
        /// <param name="loginToken">The token for the user requesting this data. 
        ///	 If provided, the system verifies the user rights to access the data</param>
        /// <param name="body">LogMessageType object that needs to be updated in the datastore</param>
        /// <response code="200">successful operation</response>
        /// <response code="405">Invalid input</response>
        [HttpPut]
        [Route("/logMessageType")]
        [ProducesResponseType(typeof(LogMessageType), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        public virtual IActionResult UpdateLogMessageType(
            [FromQuery]string loginToken,
            [FromBody]LogMessageType body)
        {
            var itemToUpdate = _dbContext.LogMessageTypes.Single(b => b.Id == body.Id);
            if (itemToUpdate != null)
            {
                if (body.Name != null && !body.Name.Equals(itemToUpdate.Name))
                    itemToUpdate.Name = body.Name;
                if (body.Description != null && !body.Description.Equals(itemToUpdate.Description))
                    itemToUpdate.Description = body.Description;
                if (body.RetentionDays != null && !body.RetentionDays.Equals(itemToUpdate.RetentionDays))
                    itemToUpdate.RetentionDays = body.RetentionDays;
                _dbContext.SaveChanges();
                return Ok("Successful operation, no data returned");
            }
            else return NotFound("LogMessageType not found");
        }

    } // End of Class LogMessageTypeApiController
} // End of Namespace Academy.Mentors.Api.Logging
