/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/

/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/


/* Powered by Solution Zone (http://www.solution.zone)   2/14/2018 1:37:43 PM */




using Academy.Mentors.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Academy.Mentors.UnitTests.Models
{
	/// <summary>
	/// Contributor Model Unit Test
	/// </summary>
	[TestClass]
	public class ContributorUnitTest
	{
		public Contributor testItem;
		public Contributor testItem2;
		public DateTime? testDate = DateTime.Now;

		/// <summary>
		/// Full Unit Tests for CI/CD
		/// </summary>
		[TestMethod]
		public void ContributorFullTest()
		{
			TestDefaultConstructor();
			TestFullConstructor();
			TestSetters();
			TestMethods();
		}

		/// <summary>
		/// Limited Smoke Tests for Configuration Validation
		/// </summary>
		[TestMethod]
		public void ContributorSmokeTest()
		{
			TestFullConstructor();
		}

		/// <summary>
		/// Test Default Constructor for Contributor Object
		/// </summary>
		public void TestDefaultConstructor()
		{
			testItem = new Contributor();
			Assert.AreEqual(default(long?), testItem.Id);
			Assert.AreEqual(default(String), testItem.Name);
			Assert.AreEqual(default(String), testItem.Description);
			Assert.AreEqual(default(String), testItem.Degree);
			Assert.AreEqual(default(String), testItem.AlmaMater);
			Assert.AreEqual(default(String), testItem.Email);
			Assert.AreEqual(default(int?), testItem.Evaluations);
			Assert.AreEqual(default(String), testItem.Password);
			Assert.AreEqual(default(String), testItem.Comments);
			Assert.AreEqual(default(DateTime?), testItem.AuditEntered);
			Assert.AreEqual(default(long?), testItem.AuditEnteredBy);
			Assert.AreEqual(default(DateTime?), testItem.AuditUpdated);
			Assert.AreEqual(default(long?), testItem.AuditUpdatedBy);

		}

		/// <summary>
		/// Test Full Constructor for Contributor Object
		/// </summary>
		public void TestFullConstructor()
		{
			testItem = new Contributor(
				0L  // Id
				, "Name"  // Name
				, "Description"  // Description
				, "Degree"  // Degree
				, "AlmaMater"  // AlmaMater
				, "Email"  // Email
				, 1  // Evaluations
				, "Password"  // Password
				, "Comments"  // Comments
				, testDate  // AuditEntered
				, 15L  // AuditEnteredBy
				, testDate  // AuditUpdated
				, 15L  // AuditUpdatedBy
				);

			Assert.AreEqual("Name", testItem.Name);
			Assert.AreEqual("Description", testItem.Description);
			Assert.AreEqual("Degree", testItem.Degree);
			Assert.AreEqual("AlmaMater", testItem.AlmaMater);
			Assert.AreEqual("Email", testItem.Email);
			Assert.AreEqual(1, testItem.Evaluations);
			Assert.AreEqual("Password", testItem.Password);
			Assert.AreEqual("Comments", testItem.Comments);
			Assert.AreEqual(testDate, testItem.AuditEntered);
			Assert.AreEqual(15L, testItem.AuditEnteredBy);
			Assert.AreEqual(testDate, testItem.AuditUpdated);
			Assert.AreEqual(15L, testItem.AuditUpdatedBy);

		}

		/// <summary>
		/// Test Setters for Contributor Object
		/// </summary>
		public void TestSetters()
		{
			testItem = new Contributor()
			{
				Id = 15L,  // Id
				Name = "Name",  // Name
				Description = "Description",  // Description
				Degree = "Degree",  // Degree
				AlmaMater = "AlmaMater",  // AlmaMater
				Email = "Email",  // Email
				Evaluations = 1,  // Evaluations
				Password = "Password",  // Password
				Comments = "Comments",  // Comments
				AuditEntered = testDate,  // AuditEntered
				AuditEnteredBy = 15L,  // AuditEnteredBy
				AuditUpdated = testDate,  // AuditUpdated
				AuditUpdatedBy = 15L,  // AuditUpdatedB
			};

			Assert.AreEqual(15L, testItem.Id);
			Assert.AreEqual("Name", testItem.Name);
			Assert.AreEqual("Description", testItem.Description);
			Assert.AreEqual("Degree", testItem.Degree);
			Assert.AreEqual("AlmaMater", testItem.AlmaMater);
			Assert.AreEqual("Email", testItem.Email);
			Assert.AreEqual(1, testItem.Evaluations);
			Assert.AreEqual("Password", testItem.Password);
			Assert.AreEqual("Comments", testItem.Comments);
			Assert.AreEqual(testDate, testItem.AuditEntered);
			Assert.AreEqual(15L, testItem.AuditEnteredBy);
			Assert.AreEqual(testDate, testItem.AuditUpdated);
			Assert.AreEqual(15L, testItem.AuditUpdatedBy);

		}

		/// <summary>
		/// Test Other Methods for Contributor Object
		/// </summary>
		public void TestMethods()
		{
			testItem = new Contributor()
			{
				Id = 15L,
				Name = "Name",
				Description = "Description",
				Degree = "Degree",
				AlmaMater = "AlmaMater",
				Email = "Email",
				Evaluations = 1,
				Password = "Password",
				Comments = "Comments",
				AuditEntered = testDate,
				AuditEnteredBy = 15L,
				AuditUpdated = testDate,
				AuditUpdatedBy = 15L
			};

			testItem2 = new Contributor()
			{
				Id = 15L,
				Name = "Name",
				Description = "Description",
				Degree = "Degree",
				AlmaMater = "AlmaMater",
				Email = "Email",
				Evaluations = 1,
				Password = "Password",
				Comments = "Comments",
				AuditEntered = testDate,
				AuditEnteredBy = 15L,
				AuditUpdated = testDate,
				AuditUpdatedBy = 15L
			};

			Assert.AreEqual(testItem.GetHashCode(), testItem2.GetHashCode()); 
			Assert.AreEqual(testItem.GetType(), testItem2.GetType()); 
			Assert.AreEqual(testItem.ToJson(), testItem2.ToJson()); 
			Assert.AreEqual(testItem.ToString(), testItem2.ToString()); 
			Assert.IsTrue(testItem.Equals(testItem2)); 
			Assert.IsTrue(testItem2.Equals(testItem)); 
			Assert.IsTrue(testItem == testItem2); 
			Assert.IsFalse(testItem2 != testItem); 
		}

	} // End of Test Class ContributorUnitTest
} // End of Namespace Academy.MentorsUnitTests.Models
