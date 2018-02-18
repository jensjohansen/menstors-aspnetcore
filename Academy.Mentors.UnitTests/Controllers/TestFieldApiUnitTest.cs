/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/


/* Powered by Solution Zone (http://www.solution.zone)   2/14/2018 1:37:44 PM */


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
	/// TestField API Controller Unit Test
	/// </summary>
	[TestClass]
	public class TestFieldApiUnitTest
	{
		public static ApiDataContext _dbContext;
		public static ILoggerFactory _logger;
		public static String loginToken;
		public static DbContextOptions<ApiDataContext> _contextOptions;

		public TestField testItem;
		public TestField saveItem;
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
		public void TestFieldApiFullTest() 
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
		public void TestFieldApiSmokeTest() 
		{ 
			TestPing(); 
		} 

        /// <summary>
        /// Test Service Ping
        /// </summary>
        public void TestPing()
        {
            using (var controller = new TestFieldApiController(_dbContext, _logger))
            {
                var response = controller.PingTestFields("Hello!") as OkObjectResult;
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
			using (var controller = new TestFieldApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) SetupTestItem(); 
				Assert.IsNull(testItem.Id); 
				var response = controller.AddTestField(loginToken, testItem) as OkObjectResult; 
				Assert.IsNotNull(response); 
				Assert.AreEqual(200, response.StatusCode); 
				saveItem = response.Value as TestField; 
				Assert.IsNotNull(saveItem); 
				Assert.IsNotNull(saveItem.Id); 
			} 
		}

		/// <summary>
		/// Test Deleting a Record from the Data Base
		/// </summary>
		public void TestDelete()
		{
			using (var controller = new TestFieldApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) TestAdd(); 
				var response = controller.DeleteTestField(loginToken, saveItem.Id) as OkResult; 
				// Test to make sure the item is not found 
				var notFound = controller.GetTestField(loginToken, saveItem.Id) as NotFoundResult; 
				Assert.IsNotNull(notFound); 
				Assert.AreEqual(404, notFound.StatusCode); 
			} 
		}

		/// <summary>
		/// Test Retrieving a Record from the Data Base
		/// </summary>
		public void TestGet()
		{
			using (var controller = new TestFieldApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) TestAdd(); 
				var response = controller.GetTestField(loginToken, saveItem.Id) as OkObjectResult; 
				Assert.IsNotNull(response); 
				Assert.AreEqual(200, response.StatusCode); 
				testItem = response.Value as TestField; 
				Assert.AreEqual(testItem.Id, saveItem.Id); 
				Assert.AreEqual(testItem.Name, saveItem.Name); 
				Assert.AreEqual(testItem.Description, saveItem.Description); 
				Assert.AreEqual(testItem.MyBoolean, saveItem.MyBoolean); 
				Assert.AreEqual(testItem.MyCreditCard, saveItem.MyCreditCard); 
				Assert.AreEqual(testItem.MyCurrency, saveItem.MyCurrency); 
				Assert.AreEqual(testItem.MyDateTime, saveItem.MyDateTime); 
				Assert.AreEqual(testItem.MyDouble, saveItem.MyDouble); 
				Assert.AreEqual(testItem.MyEmail, saveItem.MyEmail); 
				Assert.AreEqual(testItem.MyFloat, saveItem.MyFloat); 
				Assert.AreEqual(testItem.MyImageUrl, saveItem.MyImageUrl); 
				Assert.AreEqual(testItem.MyInteger, saveItem.MyInteger); 
				Assert.AreEqual(testItem.MyLong, saveItem.MyLong); 
				Assert.AreEqual(testItem.MyPhone, saveItem.MyPhone); 
				Assert.AreEqual(testItem.MyPostalCode, saveItem.MyPostalCode); 
				Assert.AreEqual(testItem.MyString, saveItem.MyString); 
				Assert.AreEqual(testItem.MyTextArea, saveItem.MyTextArea); 
				Assert.AreEqual(testItem.MyTicks, saveItem.MyTicks); 
				Assert.AreEqual(testItem.MyUrl, saveItem.MyUrl); 
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
			using (var controller = new TestFieldApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) SetupTestItem(); 
				Assert.IsNotNull(testItem.Id); 
				var response = controller.ListTestFields(loginToken, 
						null,   // myBoolean,
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
			using (var controller = new TestFieldApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) SetupTestItem(); 
				Assert.IsNotNull(testItem.Id); 
				var response = controller.ListTestFieldsCount(loginToken, 
						null,   // myBoolean,
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
			using (var controller = new TestFieldApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) SetupTestItem(); 
				Assert.IsNotNull(testItem.Id); 
				var response = controller.UpdateTestField(loginToken, testItem) as OkObjectResult; 
				Assert.IsNotNull(response); 
				Assert.AreEqual(200, response.StatusCode); 
			}
		}

		/// <summary>
		/// Test Serching Records from the Data Base
		/// </summary>
		public void TestSearch()
		{
			using (var controller = new TestFieldApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) SetupTestItem(); 
				Assert.IsNotNull(testItem.Id); 
				var response = controller.SearchTestFields(loginToken, 
						null,   // myBoolean,
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
			using (var controller = new TestFieldApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) SetupTestItem(); 
				Assert.IsNotNull(testItem.Id); 
				var response = controller.SearchTestFieldsCount(loginToken, 
						null,   // myBoolean,
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
			using (var controller = new TestFieldApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) SetupTestItem(); 
				Assert.IsNotNull(testItem.Id); 
				var response = controller.SelectTestFields(loginToken, testItem.Id.ToString()) as OkObjectResult; 
				Assert.IsNotNull(response); 
				Assert.AreEqual(200, response.StatusCode); 
			}
		}

		/// <summary>
		/// Test Updating a Record in the Data Base
		/// </summary>
		public void TestUpdate()
		{
			using (var controller = new TestFieldApiController(_dbContext, _logger)) 
			{ 
				if (saveItem == null || saveItem.Id == null) SetupTestItem(); 
				UpdateTestItem(); 
				Assert.IsNotNull(testItem.Id); 
				var response = controller.UpdateTestField(loginToken, testItem) as OkObjectResult; 
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
			testItem = new TestField()
			{
				Name = "Name", 
				Description = "Description", 
				MyBoolean = true, 
				MyCreditCard = "MyCreditCard", 
				MyCurrency = 25, 
				MyDateTime = testDate, 
				MyDouble = 10, 
				MyEmail = "MyEmail", 
				MyFloat = 5, 
				MyImageUrl = "MyImageUrl", 
				MyInteger = 1, 
				MyLong = null, 
				MyPhone = "MyPhone", 
				MyPostalCode = "MyPostalCode", 
				MyString = "MyString", 
				MyTextArea = "MyTextArea", 
				MyTicks = null, 
				MyUrl = "MyUrl", 
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
			testItem.MyBoolean = false;  // MyBoolean
			testItem.MyCreditCard = "draCtiderCyM";  // MyCreditCard
			testItem.MyCurrency = 2500;  // MyCurrency
			testItem.MyDateTime = DateTime.Now;  // MyDateTime
			testItem.MyDouble = 1000;  // MyDouble
			testItem.MyEmail = "liamEyM";  // MyEmail
			testItem.MyFloat = 500;  // MyFloat
			testItem.MyImageUrl = "lrUegamIyM";  // MyImageUrl
			testItem.MyInteger = 100;  // MyInteger
			testItem.MyLong = null;  // MyLong
			testItem.MyPhone = "enohPyM";  // MyPhone
			testItem.MyPostalCode = "edoClatsoPyM";  // MyPostalCode
			testItem.MyString = "gnirtSyM";  // MyString
			testItem.MyTextArea = "aerAtxeTyM";  // MyTextArea
			testItem.MyTicks = null;  // MyTicks
			testItem.MyUrl = "lrUyM";  // MyUrl
			testItem.Comments = "Comments";  // Comments
			testItem.AuditEntered = DateTime.Now;  // AuditEntered
			testItem.AuditEnteredBy = null;  // AuditEnteredBy
			testItem.AuditUpdated = DateTime.Now;  // AuditUpdated
			testItem.AuditUpdatedBy = null;  // AuditUpdatedBy
		}

	} // End of Test Class TestFieldUnitTest
} // End of Namespace Academy.Mentors.UnitTests.Controllers
