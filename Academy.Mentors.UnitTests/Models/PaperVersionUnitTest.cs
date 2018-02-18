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
	/// PaperVersion Model Unit Test
	/// </summary>
	[TestClass]
	public class PaperVersionUnitTest
	{
		public PaperVersion testItem;
		public PaperVersion testItem2;
		public DateTime? testDate = DateTime.Now;

		/// <summary>
		/// Full Unit Tests for CI/CD
		/// </summary>
		[TestMethod]
		public void PaperVersionFullTest()
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
		public void PaperVersionSmokeTest()
		{
			TestFullConstructor();
		}

		/// <summary>
		/// Test Default Constructor for PaperVersion Object
		/// </summary>
		public void TestDefaultConstructor()
		{
			testItem = new PaperVersion();
			Assert.AreEqual(default(long?), testItem.Id);
			Assert.AreEqual(default(String), testItem.Name);
			Assert.AreEqual(default(String), testItem.Description);
			Assert.AreEqual(default(String), testItem.Content);
			Assert.AreEqual(default(long?), testItem.ContributorId);
			Assert.AreEqual(default(long?), testItem.PaperId);
			Assert.AreEqual(default(String), testItem.Comments);
			Assert.AreEqual(default(DateTime?), testItem.AuditEntered);
			Assert.AreEqual(default(long?), testItem.AuditEnteredBy);
			Assert.AreEqual(default(DateTime?), testItem.AuditUpdated);
			Assert.AreEqual(default(long?), testItem.AuditUpdatedBy);

			Assert.AreEqual(default(Contributor), testItem.Contributor);
			Assert.AreEqual(default(Paper), testItem.Paper);
		}

		/// <summary>
		/// Test Full Constructor for PaperVersion Object
		/// </summary>
		public void TestFullConstructor()
		{
			testItem = new PaperVersion(
				0L  // Id
				, "Name"  // Name
				, "Description"  // Description
				, "Content"  // Content
				, 15L  // ContributorId
				, 15L  // PaperId
				, "Comments"  // Comments
				, testDate  // AuditEntered
				, 15L  // AuditEnteredBy
				, testDate  // AuditUpdated
				, 15L  // AuditUpdatedBy
				);

			Assert.AreEqual("Name", testItem.Name);
			Assert.AreEqual("Description", testItem.Description);
			Assert.AreEqual("Content", testItem.Content);
			Assert.AreEqual(15L, testItem.ContributorId);
			Assert.AreEqual(15L, testItem.PaperId);
			Assert.AreEqual("Comments", testItem.Comments);
			Assert.AreEqual(testDate, testItem.AuditEntered);
			Assert.AreEqual(15L, testItem.AuditEnteredBy);
			Assert.AreEqual(testDate, testItem.AuditUpdated);
			Assert.AreEqual(15L, testItem.AuditUpdatedBy);

			Assert.AreEqual(default(Contributor), testItem.Contributor);
			Assert.AreEqual(default(Paper), testItem.Paper);
		}

		/// <summary>
		/// Test Setters for PaperVersion Object
		/// </summary>
		public void TestSetters()
		{
			testItem = new PaperVersion()
			{
				Id = 15L,  // Id
				Name = "Name",  // Name
				Description = "Description",  // Description
				Content = "Content",  // Content
				ContributorId = 15L,  // ContributorId
				PaperId = 15L,  // PaperId
				Comments = "Comments",  // Comments
				AuditEntered = testDate,  // AuditEntered
				AuditEnteredBy = 15L,  // AuditEnteredBy
				AuditUpdated = testDate,  // AuditUpdated
				AuditUpdatedBy = 15L,  // AuditUpdatedB
			};

			Assert.AreEqual(15L, testItem.Id);
			Assert.AreEqual("Name", testItem.Name);
			Assert.AreEqual("Description", testItem.Description);
			Assert.AreEqual("Content", testItem.Content);
			Assert.AreEqual(15L, testItem.ContributorId);
			Assert.AreEqual(15L, testItem.PaperId);
			Assert.AreEqual("Comments", testItem.Comments);
			Assert.AreEqual(testDate, testItem.AuditEntered);
			Assert.AreEqual(15L, testItem.AuditEnteredBy);
			Assert.AreEqual(testDate, testItem.AuditUpdated);
			Assert.AreEqual(15L, testItem.AuditUpdatedBy);

			Assert.AreEqual(default(Contributor), testItem.Contributor);
			Assert.AreEqual(default(Paper), testItem.Paper);
		}

		/// <summary>
		/// Test Other Methods for PaperVersion Object
		/// </summary>
		public void TestMethods()
		{
			testItem = new PaperVersion()
			{
				Id = 15L,
				Name = "Name",
				Description = "Description",
				Content = "Content",
				ContributorId = 15L,
				PaperId = 15L,
				Comments = "Comments",
				AuditEntered = testDate,
				AuditEnteredBy = 15L,
				AuditUpdated = testDate,
				AuditUpdatedBy = 15L
			};

			testItem2 = new PaperVersion()
			{
				Id = 15L,
				Name = "Name",
				Description = "Description",
				Content = "Content",
				ContributorId = 15L,
				PaperId = 15L,
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

	} // End of Test Class PaperVersionUnitTest
} // End of Namespace Academy.MentorsUnitTests.Models
