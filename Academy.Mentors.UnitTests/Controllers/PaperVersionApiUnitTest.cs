/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/


/* Powered by Solution Zone (http://www.solution.zone)   2/14/2018 1:37:43 PM */


using Academy.Mentors.Api.Controllers;
using Academy.Mentors.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Microsoft.EntityFrameworkCore.SqlServer; 
using Moq;
using System;

namespace Academy.Mentors.UnitTests.Controllers
{
	/// <summary>
	/// PaperVersion API Controller Unit Test
	/// </summary>
	[TestClass]
	public class PaperVersionApiUnitTest
	{
		public static ApiDataContext _dbContext;
		public static ILoggerFactory _logger;
		public static String loginToken;
		public static DbContextOptions<ApiDataContext> _contextOptions;

		public PaperVersion testItem;
		public PaperVersion saveItem;
		public DateTime? testDate = DateTime.Now;

		[ClassInitialize]
		public static void SetupUnitTest(TestContext myContext) 
		{ 
			var dataConfiguration = new DataConfiguration();
			_contextOptions = new DbContextOptionsBuilder<ApiDataContext>() 
				.UseSqlServer(@dataConfiguration.ConnectionString).Options; 
			_dbContext = new ApiDataContext(_contextOptions); 
            _logger = new Mock<ILoggerFactory>().Object;
			using (var userApi = new ContributorApiController(_dbContext, _logger)) 
			{ 
				var userResponse = userApi.LoginUser(dataConfiguration.LoginEmail, dataConfiguration.LoginPassword) as OkObjectResult; 
				loginToken = userResponse.Value.ToString(); 
			} 
		} 

		[ClassCleanup]
		public static void TeardownUnitTest() 
		{ 
			_dbContext.Dispose(); 
		} 

		/// <summary> 
		/// Run full set of tests 
		/// </summary> 
		[TestMethod] 
		public void PaperVersionApiFullTest() 
		{ 
			testItem = null; 
			saveItem = null; 
			TestAdd(); 
			TestGet(); 
			TestUpdate(); 
			TestRevert(); 
			TestList(); 
			TestListCount(); 
			TestSelect(); 
			TestSearch(); 
			TestSearchCount(); 
			TestDelete(); 
		} 

		/// <summary> 
		/// Run a short smoke test validation set of tests 
		/// </summary> 
		[TestMethod] 
		public void PaperVersionApiSmokeTest() 
		{ 
			TestPing(); 
		} 

        /// <summary>
        /// Test Service Ping
        /// </summary>
        public void TestPing()
        {
            using (var controller = new PaperVersionApiController(_dbContext, _logger))
            {
                var response = controller.PingPaperVersions("Hello!") as OkObjectResult;
                Assert.IsNotNull(response);
                Assert.AreEqual(200, response.StatusCode);
                Assert.AreEqual("!olleH", response.Value);
            }
        }

		/// <summary>
		/// Test Adding a Record to the Data Base
		/// </summary>
		public void TestAdd()
		{
			using (var controller = new PaperVersionApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) SetupTestItem(); 
				Assert.IsNull(testItem.Id); 
				var response = controller.AddPaperVersion(loginToken, testItem) as OkObjectResult; 
				Assert.IsNotNull(response); 
				Assert.AreEqual(200, response.StatusCode); 
				saveItem = response.Value as PaperVersion; 
				Assert.IsNotNull(saveItem); 
				Assert.IsNotNull(saveItem.Id); 
			} 
		}

		/// <summary>
		/// Test Deleting a Record from the Data Base
		/// </summary>
		public void TestDelete()
		{
			using (var controller = new PaperVersionApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) TestAdd(); 
				var response = controller.DeletePaperVersion(loginToken, saveItem.Id) as OkResult; 
				// Test to make sure the item is not found 
				var notFound = controller.GetPaperVersion(loginToken, saveItem.Id) as NotFoundResult; 
				Assert.IsNotNull(notFound); 
				Assert.AreEqual(404, notFound.StatusCode); 
			} 
		}

		/// <summary>
		/// Test Retrieving a Record from the Data Base
		/// </summary>
		public void TestGet()
		{
			using (var controller = new PaperVersionApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) TestAdd(); 
				var response = controller.GetPaperVersion(loginToken, saveItem.Id) as OkObjectResult; 
				Assert.IsNotNull(response); 
				Assert.AreEqual(200, response.StatusCode); 
				testItem = response.Value as PaperVersion; 
				Assert.AreEqual(testItem.Id, saveItem.Id); 
				Assert.AreEqual(testItem.Name, saveItem.Name); 
				Assert.AreEqual(testItem.Description, saveItem.Description); 
				Assert.AreEqual(testItem.Content, saveItem.Content); 
				Assert.AreEqual(testItem.ContributorId, saveItem.ContributorId); 
				Assert.AreEqual(testItem.PaperId, saveItem.PaperId); 
				Assert.AreEqual(testItem.Comments, saveItem.Comments); 
				Assert.AreEqual(testItem.AuditEntered, saveItem.AuditEntered); 
				Assert.AreEqual(testItem.AuditEnteredBy, saveItem.AuditEnteredBy); 
				Assert.AreEqual(testItem.AuditUpdated, saveItem.AuditUpdated); 
				Assert.AreEqual(testItem.AuditUpdatedBy, saveItem.AuditUpdatedBy); 
			} 
		}

		/// <summary>
		/// Test Listing Records from the Data Base
		/// </summary>
		public void TestList()
		{
			using (var controller = new PaperVersionApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) SetupTestItem(); 
				Assert.IsNotNull(testItem.Id); 
				var response = controller.ListPaperVersions(loginToken, 
						null,   // contributorId,
						null,   // paperId,
						null,  // Search string
						null,  // page number
						null   // page size
					) as OkObjectResult; 
				Assert.IsNotNull(response); 
				Assert.AreEqual(200, response.StatusCode); 
			}
		}

		/// <summary>
		/// Test counting the Listing of Records from the Data Base
		/// </summary>
		public void TestListCount()
		{
			using (var controller = new PaperVersionApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) SetupTestItem(); 
				Assert.IsNotNull(testItem.Id); 
				var response = controller.ListPaperVersionsCount(loginToken, 
						null,   // contributorId,
						null,   // paperId,
						null  // Search string
					) as OkObjectResult; 
				Assert.IsNotNull(response); 
				Assert.AreEqual(200, response.StatusCode); 
			}
		}

		/// <summary>
		/// Test Reverting a Record in the Data Base
		/// </summary>
		public void TestRevert()
		{
			using (var controller = new PaperVersionApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) SetupTestItem(); 
				Assert.IsNotNull(testItem.Id); 
				var response = controller.UpdatePaperVersion(loginToken, testItem) as OkObjectResult; 
				Assert.IsNotNull(response); 
				Assert.AreEqual(200, response.StatusCode); 
			}
		}

		/// <summary>
		/// Test Serching Records from the Data Base
		/// </summary>
		public void TestSearch()
		{
			using (var controller = new PaperVersionApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) SetupTestItem(); 
				Assert.IsNotNull(testItem.Id); 
				var response = controller.SearchPaperVersions(loginToken, 
						null,   // contributorId,
						null,   // paperId,
						null,  // Search string
						null,  // page number
						null   // page size
					) as OkObjectResult; 
				Assert.IsNotNull(response); 
				Assert.AreEqual(200, response.StatusCode); 
			}
		}

		/// <summary>
		/// Test Counting Searching Records from the Data Base
		/// </summary>
		public void TestSearchCount()
		{
			using (var controller = new PaperVersionApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) SetupTestItem(); 
				Assert.IsNotNull(testItem.Id); 
				var response = controller.SearchPaperVersionsCount(loginToken, 
						null,   // contributorId,
						null,   // paperId,
						null  // Search string
					) as OkObjectResult; 
				Assert.IsNotNull(response); 
				Assert.AreEqual(200, response.StatusCode); 
			}
		}

		/// <summary>
		/// Test Selecting a Record in the Data Base
		/// </summary>
		public void TestSelect()
		{
			using (var controller = new PaperVersionApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) SetupTestItem(); 
				Assert.IsNotNull(testItem.Id); 
				var response = controller.SelectPaperVersions(loginToken, testItem.Id.ToString()) as OkObjectResult; 
				Assert.IsNotNull(response); 
				Assert.AreEqual(200, response.StatusCode); 
			}
		}

		/// <summary>
		/// Test Updating a Record in the Data Base
		/// </summary>
		public void TestUpdate()
		{
			using (var controller = new PaperVersionApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) SetupTestItem(); 
				UpdateTestItem(); 
				Assert.IsNotNull(testItem.Id); 
				var response = controller.UpdatePaperVersion(loginToken, testItem) as OkObjectResult; 
				Assert.IsNotNull(response); 
				Assert.AreEqual(200, response.StatusCode); 
				TestGet(); 
			} 
		}

		/// <summary>
		/// Setup a test item
		/// </summary>
		public void SetupTestItem()
		{
			testItem = new PaperVersion()
			{
				Name = "Name", 
				Description = "Description", 
				Content = "Content", 
				ContributorId = null, 
				PaperId = null, 
				Comments = "Comments", 
				AuditEntered = testDate, 
				AuditEnteredBy = null, 
				AuditUpdated = testDate, 
				AuditUpdatedBy = null, 
			};
		}

		/// <summary>
		/// Update a test item
		/// </summary>
		public void UpdateTestItem()
		{
			testItem.Name = "emaN";  // Name
			testItem.Description = "noitpircseD";  // Description
			testItem.Content = "Content";  // Content
			testItem.ContributorId = null;  // ContributorId
			testItem.PaperId = null;  // PaperId
			testItem.Comments = "Comments";  // Comments
			testItem.AuditEntered = DateTime.Now;  // AuditEntered
			testItem.AuditEnteredBy = null;  // AuditEnteredBy
			testItem.AuditUpdated = DateTime.Now;  // AuditUpdated
			testItem.AuditUpdatedBy = null;  // AuditUpdatedBy
		}

	} // End of Test Class PaperVersionUnitTest
} // End of Namespace Academy.Mentors.UnitTests.Controllers
