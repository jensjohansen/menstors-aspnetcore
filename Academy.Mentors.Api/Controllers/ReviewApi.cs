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
    /// Reviews RESTful API Controller (ASP.NET Core)
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ReviewApiController : ReviewBaseApiController
    {
        /// <summary>
        /// Review API Controller
        /// </summary>
        /// <param name="apiDataContext">DbContext for EntityFramework</param>
        /// <param name="logger">Injected ILoggerFactory</param>
        public ReviewApiController(ApiDataContext apiDataContext, ILoggerFactory logger) : base(apiDataContext, logger)
        { 
        }

    }
}
