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


/* Powered by Solution Zone (http://www.solution.zone)   2/14/2018 1:37:44 PM */




using Academy.Mentors.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Academy.Mentors.UnitTests.Models
{
	/// <summary>
	/// TestField Model Unit Test
	/// </summary>
	[TestClass]
	public class TestFieldUnitTest
	{
		public TestField testItem;
		public TestField testItem2;
		public DateTime? testDate = DateTime.Now;

		/// <summary>
		/// Full Unit Tests for CI/CD
		/// </summary>
		[TestMethod]
		public void TestFieldFullTest()
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
		public void TestFieldSmokeTest()
		{
			TestFullConstructor();
		}

		/// <summary>
		/// Test Default Constructor for TestField Object
		/// </summary>
		public void TestDefaultConstructor()
		{
			testItem = new TestField();
			Assert.AreEqual(default(long?), testItem.Id);
			Assert.AreEqual(default(String), testItem.Name);
			Assert.AreEqual(default(String), testItem.Description);
			Assert.AreEqual(default(bool?), testItem.MyBoolean);
			Assert.AreEqual(default(String), testItem.MyCreditCard);
			Assert.AreEqual(default(Decimal?), testItem.MyCurrency);
			Assert.AreEqual(default(DateTime?), testItem.MyDateTime);
			Assert.AreEqual(default(double?), testItem.MyDouble);
			Assert.AreEqual(default(String), testItem.MyEmail);
			Assert.AreEqual(default(float?), testItem.MyFloat);
			Assert.AreEqual(default(String), testItem.MyImageUrl);
			Assert.AreEqual(default(int?), testItem.MyInteger);
			Assert.AreEqual(default(long?), testItem.MyLong);
			Assert.AreEqual(default(String), testItem.MyPhone);
			Assert.AreEqual(default(String), testItem.MyPostalCode);
			Assert.AreEqual(default(String), testItem.MyString);
			Assert.AreEqual(default(String), testItem.MyTextArea);
			Assert.AreEqual(default(long?), testItem.MyTicks);
			Assert.AreEqual(default(String), testItem.MyUrl);
			Assert.AreEqual(default(String), testItem.Comments);
			Assert.AreEqual(default(DateTime?), testItem.AuditEntered);
			Assert.AreEqual(default(long?), testItem.AuditEnteredBy);
			Assert.AreEqual(default(DateTime?), testItem.AuditUpdated);
			Assert.AreEqual(default(long?), testItem.AuditUpdatedBy);

		}

		/// <summary>
		/// Test Full Constructor for TestField Object
		/// </summary>
		public void TestFullConstructor()
		{
			testItem = new TestField(
				0L  // Id
				, "Name"  // Name
				, "Description"  // Description
				, true  // MyBoolean
				, "MyCreditCard"  // MyCreditCard
				, 25  // MyCurrency
				, testDate  // MyDateTime
				, 10  // MyDouble
				, "MyEmail"  // MyEmail
				, 5  // MyFloat
				, "MyImageUrl"  // MyImageUrl
				, 1  // MyInteger
				, 15L  // MyLong
				, "MyPhone"  // MyPhone
				, "MyPostalCode"  // MyPostalCode
				, "MyString"  // MyString
				, "MyTextArea"  // MyTextArea
				, 15L  // MyTicks
				, "MyUrl"  // MyUrl
				, "Comments"  // Comments
				, testDate  // AuditEntered
				, 15L  // AuditEnteredBy
				, testDate  // AuditUpdated
				, 15L  // AuditUpdatedBy
				);

			Assert.AreEqual("Name", testItem.Name);
			Assert.AreEqual("Description", testItem.Description);
			Assert.AreEqual(true, testItem.MyBoolean);
			Assert.AreEqual("MyCreditCard", testItem.MyCreditCard);
			Assert.AreEqual(25, testItem.MyCurrency);
			Assert.AreEqual(testDate, testItem.MyDateTime);
			Assert.AreEqual(10, testItem.MyDouble);
			Assert.AreEqual("MyEmail", testItem.MyEmail);
			Assert.AreEqual(5, testItem.MyFloat);
			Assert.AreEqual("MyImageUrl", testItem.MyImageUrl);
			Assert.AreEqual(1, testItem.MyInteger);
			Assert.AreEqual(15L, testItem.MyLong);
			Assert.AreEqual("MyPhone", testItem.MyPhone);
			Assert.AreEqual("MyPostalCode", testItem.MyPostalCode);
			Assert.AreEqual("MyString", testItem.MyString);
			Assert.AreEqual("MyTextArea", testItem.MyTextArea);
			Assert.AreEqual(15L, testItem.MyTicks);
			Assert.AreEqual("MyUrl", testItem.MyUrl);
			Assert.AreEqual("Comments", testItem.Comments);
			Assert.AreEqual(testDate, testItem.AuditEntered);
			Assert.AreEqual(15L, testItem.AuditEnteredBy);
			Assert.AreEqual(testDate, testItem.AuditUpdated);
			Assert.AreEqual(15L, testItem.AuditUpdatedBy);

		}

		/// <summary>
		/// Test Setters for TestField Object
		/// </summary>
		public void TestSetters()
		{
			testItem = new TestField()
			{
				Id = 15L,  // Id
				Name = "Name",  // Name
				Description = "Description",  // Description
				MyBoolean = true,  // MyBoolean
				MyCreditCard = "MyCreditCard",  // MyCreditCard
				MyCurrency = 25,  // MyCurrency
				MyDateTime = testDate,  // MyDateTime
				MyDouble = 10,  // MyDouble
				MyEmail = "MyEmail",  // MyEmail
				MyFloat = 5,  // MyFloat
				MyImageUrl = "MyImageUrl",  // MyImageUrl
				MyInteger = 1,  // MyInteger
				MyLong = 15L,  // MyLong
				MyPhone = "MyPhone",  // MyPhone
				MyPostalCode = "MyPostalCode",  // MyPostalCode
				MyString = "MyString",  // MyString
				MyTextArea = "MyTextArea",  // MyTextArea
				MyTicks = 15L,  // MyTicks
				MyUrl = "MyUrl",  // MyUrl
				Comments = "Comments",  // Comments
				AuditEntered = testDate,  // AuditEntered
				AuditEnteredBy = 15L,  // AuditEnteredBy
				AuditUpdated = testDate,  // AuditUpdated
				AuditUpdatedBy = 15L,  // AuditUpdatedB
			};

			Assert.AreEqual(15L, testItem.Id);
			Assert.AreEqual("Name", testItem.Name);
			Assert.AreEqual("Description", testItem.Description);
			Assert.AreEqual(true, testItem.MyBoolean);
			Assert.AreEqual("MyCreditCard", testItem.MyCreditCard);
			Assert.AreEqual(25, testItem.MyCurrency);
			Assert.AreEqual(testDate, testItem.MyDateTime);
			Assert.AreEqual(10, testItem.MyDouble);
			Assert.AreEqual("MyEmail", testItem.MyEmail);
			Assert.AreEqual(5, testItem.MyFloat);
			Assert.AreEqual("MyImageUrl", testItem.MyImageUrl);
			Assert.AreEqual(1, testItem.MyInteger);
			Assert.AreEqual(15L, testItem.MyLong);
			Assert.AreEqual("MyPhone", testItem.MyPhone);
			Assert.AreEqual("MyPostalCode", testItem.MyPostalCode);
			Assert.AreEqual("MyString", testItem.MyString);
			Assert.AreEqual("MyTextArea", testItem.MyTextArea);
			Assert.AreEqual(15L, testItem.MyTicks);
			Assert.AreEqual("MyUrl", testItem.MyUrl);
			Assert.AreEqual("Comments", testItem.Comments);
			Assert.AreEqual(testDate, testItem.AuditEntered);
			Assert.AreEqual(15L, testItem.AuditEnteredBy);
			Assert.AreEqual(testDate, testItem.AuditUpdated);
			Assert.AreEqual(15L, testItem.AuditUpdatedBy);

		}

		/// <summary>
		/// Test Other Methods for TestField Object
		/// </summary>
		public void TestMethods()
		{
			testItem = new TestField()
			{
				Id = 15L,
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
				MyLong = 15L,
				MyPhone = "MyPhone",
				MyPostalCode = "MyPostalCode",
				MyString = "MyString",
				MyTextArea = "MyTextArea",
				MyTicks = 15L,
				MyUrl = "MyUrl",
				Comments = "Comments",
				AuditEntered = testDate,
				AuditEnteredBy = 15L,
				AuditUpdated = testDate,
				AuditUpdatedBy = 15L
			};

			testItem2 = new TestField()
			{
				Id = 15L,
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
				MyLong = 15L,
				MyPhone = "MyPhone",
				MyPostalCode = "MyPostalCode",
				MyString = "MyString",
				MyTextArea = "MyTextArea",
				MyTicks = 15L,
				MyUrl = "MyUrl",
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

	} // End of Test Class TestFieldUnitTest
} // End of Namespace Academy.MentorsUnitTests.Models
