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
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Mentors.Administration.ViewComponents.Collections
{
	/// <summary>
	/// TestFields Collection View Component (ASP.NET Core 1.1)
	/// </summary>
	public class TestFieldsViewComponent : ViewComponent
	{
		private AdministrationContext _context;


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="context">MVC Version of DataContext</param>
		public TestFieldsViewComponent(AdministrationContext context)
		{
			_context = context;
		}

		/// <summary>
		/// TestFields View Component (lists)
		/// </summary>
		/// <param name="viewType">Name of the list view to render</param>
		/// <param name="auditEnteredBy">Filter on auditEnteredBy (null to not filter)</param>
		/// <param name="auditUpdatedBy">Filter on auditUpdatedBy (null to not filter)</param>
		/// <param name="sortOrder">Page Sort Order</param>
		/// <param name="searchString">Search String to limit results</param>
		/// <param name="testFieldsPage">Page number of results, starting at 0</param>
		/// <param name="pageSize">Page size to display</param>
		/// <param name="currentFilter">Last search string while paging</param>
		/// <returns>Task Action Result</returns>
		public async Task<IViewComponentResult> InvokeAsync(
			string viewType,
			long? auditEnteredBy,
			long? auditUpdatedBy,
			string sortOrder,
			string testFieldsPage,
			int? pageSize,
			string searchString,
			string currentSort = "",
			string currentFilter = "")
		{
			if (String.IsNullOrEmpty(viewType)) viewType = "Default";
			// pass page configuration to Views
			ViewData["viewType"] = viewType;
			ViewData["CurrentSort"] = sortOrder;

			if (string.IsNullOrEmpty(testFieldsPage)) testFieldsPage = "1";
			int page = 1;
			int.TryParse(testFieldsPage, out page);
			ViewData["auditEnteredBy"] = auditEnteredBy;
			ViewData["auditUpdatedBy"] = auditUpdatedBy;
			// Set up column sorting
			ViewData["IdSortParm"] = sortOrder == "Id" ? "Id_desc" : "Id";
			ViewData["NameSortParm"] = sortOrder == "Name" ? "Name_desc" : "Name";
			ViewData["DescriptionSortParm"] = sortOrder == "Description" ? "Description_desc" : "Description";
			ViewData["MyBooleanSortParm"] = sortOrder == "MyBoolean" ? "MyBoolean_desc" : "MyBoolean";
			ViewData["MyCreditCardSortParm"] = sortOrder == "MyCreditCard" ? "MyCreditCard_desc" : "MyCreditCard";
			ViewData["MyCurrencySortParm"] = sortOrder == "MyCurrency" ? "MyCurrency_desc" : "MyCurrency";
			ViewData["MyDateTimeSortParm"] = sortOrder == "MyDateTime" ? "MyDateTime_desc" : "MyDateTime";
			ViewData["MyDoubleSortParm"] = sortOrder == "MyDouble" ? "MyDouble_desc" : "MyDouble";
			ViewData["MyEmailSortParm"] = sortOrder == "MyEmail" ? "MyEmail_desc" : "MyEmail";
			ViewData["MyFloatSortParm"] = sortOrder == "MyFloat" ? "MyFloat_desc" : "MyFloat";
			ViewData["MyImageUrlSortParm"] = sortOrder == "MyImageUrl" ? "MyImageUrl_desc" : "MyImageUrl";
			ViewData["MyIntegerSortParm"] = sortOrder == "MyInteger" ? "MyInteger_desc" : "MyInteger";
			ViewData["MyLongSortParm"] = sortOrder == "MyLong" ? "MyLong_desc" : "MyLong";
			ViewData["MyPhoneSortParm"] = sortOrder == "MyPhone" ? "MyPhone_desc" : "MyPhone";
			ViewData["MyPostalCodeSortParm"] = sortOrder == "MyPostalCode" ? "MyPostalCode_desc" : "MyPostalCode";
			ViewData["MyStringSortParm"] = sortOrder == "MyString" ? "MyString_desc" : "MyString";
			ViewData["MyTextAreaSortParm"] = sortOrder == "MyTextArea" ? "MyTextArea_desc" : "MyTextArea";
			ViewData["MyTicksSortParm"] = sortOrder == "MyTicks" ? "MyTicks_desc" : "MyTicks";
			ViewData["MyUrlSortParm"] = sortOrder == "MyUrl" ? "MyUrl_desc" : "MyUrl";
			ViewData["CommentsSortParm"] = sortOrder == "Comments" ? "Comments_desc" : "Comments";
			ViewData["AuditEnteredSortParm"] = sortOrder == "AuditEntered" ? "AuditEntered_desc" : "AuditEntered";
			ViewData["AuditEnteredBySortParm"] = sortOrder == "AuditEnteredBy" ? "AuditEnteredBy_desc" : "AuditEnteredBy";
			ViewData["AuditUpdatedSortParm"] = sortOrder == "AuditUpdated" ? "AuditUpdated_desc" : "AuditUpdated";
			ViewData["AuditUpdatedBySortParm"] = sortOrder == "AuditUpdatedBy" ? "AuditUpdatedBy_desc" : "AuditUpdatedBy";
			// Reset page if there is a new search order
			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}
			if (searchString == null) searchString = "";

			if (page < 1) page = 1;
			// Pass paging settings
			ViewData["CurrentFilter"] = searchString;
			ViewData["testFieldsPage"] = page.ToString();
			ViewData["pageSize"] = pageSize;
			int testId = -1;
			int.TryParse(searchString, out testId);
			// Get data from context
				var results = _context.TestFields.Where(b => b.Id > -1 
					&& (auditEnteredBy == null || b.AuditEnteredBy.Equals(auditEnteredBy))
					&& (auditUpdatedBy == null || b.AuditUpdatedBy.Equals(auditUpdatedBy))
					&& (b.Id == testId ||
						b.Id.ToString().Contains(searchString) ||
						b.Name.Contains(searchString) ||
						b.Description.Contains(searchString) ||
						b.MyCreditCard.Contains(searchString) ||
						b.MyEmail.Contains(searchString) ||
						b.MyImageUrl.Contains(searchString) ||
						b.MyInteger.ToString().Contains(searchString) ||
						b.MyLong.ToString().Contains(searchString) ||
						b.MyPhone.Contains(searchString) ||
						b.MyPostalCode.Contains(searchString) ||
						b.MyString.Contains(searchString) ||
						b.MyTextArea.Contains(searchString) ||
						b.MyTicks.ToString().Contains(searchString) ||
						b.MyUrl.Contains(searchString) ||
						b.Comments.Contains(searchString) ||
						b.AuditEnteredBy.ToString().Contains(searchString) ||
						b.AuditUpdatedBy.ToString().Contains(searchString) 
						)).Take(1000);
			//Set up column sorting
			switch (sortOrder)
			{
				case "Id": results = results.OrderBy(a => a.Id); break;
				case "Id_desc": results = results.OrderByDescending(a => a.Id); break;
				case "Name": results = results.OrderBy(a => a.Name); break;
				case "Name_desc": results = results.OrderByDescending(a => a.Name); break;
				case "Description": results = results.OrderBy(a => a.Description); break;
				case "Description_desc": results = results.OrderByDescending(a => a.Description); break;
				case "MyBoolean": results = results.OrderBy(a => a.MyBoolean); break;
				case "MyBoolean_desc": results = results.OrderByDescending(a => a.MyBoolean); break;
				case "MyCreditCard": results = results.OrderBy(a => a.MyCreditCard); break;
				case "MyCreditCard_desc": results = results.OrderByDescending(a => a.MyCreditCard); break;
				case "MyCurrency": results = results.OrderBy(a => a.MyCurrency); break;
				case "MyCurrency_desc": results = results.OrderByDescending(a => a.MyCurrency); break;
				case "MyDateTime": results = results.OrderBy(a => a.MyDateTime); break;
				case "MyDateTime_desc": results = results.OrderByDescending(a => a.MyDateTime); break;
				case "MyDouble": results = results.OrderBy(a => a.MyDouble); break;
				case "MyDouble_desc": results = results.OrderByDescending(a => a.MyDouble); break;
				case "MyEmail": results = results.OrderBy(a => a.MyEmail); break;
				case "MyEmail_desc": results = results.OrderByDescending(a => a.MyEmail); break;
				case "MyFloat": results = results.OrderBy(a => a.MyFloat); break;
				case "MyFloat_desc": results = results.OrderByDescending(a => a.MyFloat); break;
				case "MyImageUrl": results = results.OrderBy(a => a.MyImageUrl); break;
				case "MyImageUrl_desc": results = results.OrderByDescending(a => a.MyImageUrl); break;
				case "MyInteger": results = results.OrderBy(a => a.MyInteger); break;
				case "MyInteger_desc": results = results.OrderByDescending(a => a.MyInteger); break;
				case "MyLong": results = results.OrderBy(a => a.MyLong); break;
				case "MyLong_desc": results = results.OrderByDescending(a => a.MyLong); break;
				case "MyPhone": results = results.OrderBy(a => a.MyPhone); break;
				case "MyPhone_desc": results = results.OrderByDescending(a => a.MyPhone); break;
				case "MyPostalCode": results = results.OrderBy(a => a.MyPostalCode); break;
				case "MyPostalCode_desc": results = results.OrderByDescending(a => a.MyPostalCode); break;
				case "MyString": results = results.OrderBy(a => a.MyString); break;
				case "MyString_desc": results = results.OrderByDescending(a => a.MyString); break;
				case "MyTextArea": results = results.OrderBy(a => a.MyTextArea); break;
				case "MyTextArea_desc": results = results.OrderByDescending(a => a.MyTextArea); break;
				case "MyTicks": results = results.OrderBy(a => a.MyTicks); break;
				case "MyTicks_desc": results = results.OrderByDescending(a => a.MyTicks); break;
				case "MyUrl": results = results.OrderBy(a => a.MyUrl); break;
				case "MyUrl_desc": results = results.OrderByDescending(a => a.MyUrl); break;
				case "Comments": results = results.OrderBy(a => a.Comments); break;
				case "Comments_desc": results = results.OrderByDescending(a => a.Comments); break;
				case "AuditEntered": results = results.OrderBy(a => a.AuditEntered); break;
				case "AuditEntered_desc": results = results.OrderByDescending(a => a.AuditEntered); break;
				case "AuditEnteredBy": results = results.OrderBy(a => a.AuditEnteredBy); break;
				case "AuditEnteredBy_desc": results = results.OrderByDescending(a => a.AuditEnteredBy); break;
				case "AuditUpdated": results = results.OrderBy(a => a.AuditUpdated); break;
				case "AuditUpdated_desc": results = results.OrderByDescending(a => a.AuditUpdated); break;
				case "AuditUpdatedBy": results = results.OrderBy(a => a.AuditUpdatedBy); break;
				case "AuditUpdatedBy_desc": results = results.OrderByDescending(a => a.AuditUpdatedBy); break;
				default: results = results.OrderBy(s => s.Id); break;
			}
			return View(await PaginatedList<TestField>.CreateAsync(results.AsNoTracking(), page, pageSize ?? 4));
		}

	} // End of Class TestFieldsViewComponent
} // End of Namespace Academy.Mentors.Administration.ViewComponents.Collections
