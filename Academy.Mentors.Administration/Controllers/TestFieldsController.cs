/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/


/* Powered by Solution Zone (http://www.solution.zone)  2/14/2018 1:37:44 PM */


using Academy.Mentors.Api;
using Academy.Mentors.Models;
using Academy.Mentors.Administration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Mentors.Administration.Controllers
{
	/// <summary>
	/// TestFields Administration MVC Controller (ASP.NET Core 1.1)
	/// </summary>
	[Authorize]
	public class TestFieldsController : Controller
	{
		private AdministrationContext _context;

		/// <summary>
		/// Tokenizer injected into this controller for encryption
		/// </summary>
		protected Tokenator _tokenizer = new Tokenator();


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="context">MVC Version of DataContext</param>
		public TestFieldsController(AdministrationContext context)
		{
			_context = context;
		}

		/// <summary>
		/// GET /TestFields/Index
		/// </summary>
		/// <param name="sortOrder">Page Sort Order</param>
		/// <param name="searchString">Search String to limit results</param>
		/// <param name="page">Page number of results, starting at 0</param>
		/// <param name="pageSize">Page size to display</param>
		/// <param name="currentFilter">Last search string while paging</param>
		/// <returns>Task Action Result</returns>
		public async Task<IActionResult> Index(
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			string currentFilter = "")
		{
			// pass page configuration to Views
			ViewData["CurrentSort"] = sortOrder;
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
			// Pass paging settings
			ViewData["CurrentFilter"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			int testId = -1;
			int.TryParse(searchString,out testId);
			// Get data from context
				var results = _context.TestFields.Where(b => b.Id > -1 
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
			return View(await PaginatedList<TestField>.CreateAsync(results.AsNoTracking(), page ?? 1, pageSize ?? 10));
		}


		/// <summary>
		/// GET /TestFields/Select
		/// </summary>
		/// <param name="selectedId">Already selected ID</param>
		/// <param name="referred">URI of caller</param>
		/// <param name="sortOrder">Page Sort Order</param>
		/// <param name="searchString">Search String to limit results</param>
		/// <param name="page">Page number of results, starting at 0</param>
		/// <param name="pageSize">Page size to display</param>
		/// <param name="currentFilter">Last search string while paging</param>
		/// <returns>Task Action Result</returns>
		public async Task<IActionResult> Select(
			long? selectedId,
			string referred,
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			string currentFilter = "")
		{
			if (searchString == null) searchString = ""; 
			// Pass paging settings
			ViewData["CurrentFilter"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			ViewData["selectedId"] = selectedId;
			ViewData["referred"] = referred;
			int testId = -1;
			int.TryParse(searchString,out testId);
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
			// Pass paging settings
			ViewData["CurrentFilter"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			// Get data from context
				var results = _context.TestFields.Where(b => b.Id > -1 
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
			return View(await PaginatedList<TestField>.CreateAsync(results.AsNoTracking(), page ?? 1, pageSize ?? 10));
		}


		/// <summary>
		/// GET /TestFields/Details/Id
		/// </summary>
		/// <param name="id">Id of record to display</param>
		/// <param name="referer">The go-back url</param>
		/// <param name="sortOrder">Page Sort Order</param>
		/// <param name="searchString">Search String to limit results</param>
		/// <param name="page">Page number of results, starting at 0</param>
		/// <param name="pageSize">Page size to display</param>
		/// <param name="currentFilter">Last search string while paging</param>
		/// <returns>Task Action Result</returns>
		public async Task<IActionResult> Details(
			long? id,
			string referer,
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			string currentFilter = "")
		{
			// pass page configuration to Views
			ViewData["CurrentSort"] = sortOrder;
			if (referer == null) referer = Request.Headers["Referer"];
			ViewData["referer"] = referer;
			ViewData["sortOrder"] = sortOrder;
			ViewData["searchString"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			ViewData["currentFilter"] = currentFilter;
			if (id == null) return NotFound();
			var result = await _context.TestFields.SingleOrDefaultAsync(m => m.Id == id);
			if (result == null) return NotFound();
			return View(result);
		}


		/// <summary>
		/// GET /TestFields/Edit/Id
		/// </summary>
		/// <param name="id">Id of record to edit</param>
		/// <param name="referer">The go-back url</param>
		/// <param name="sortOrder">Page Sort Order</param>
		/// <param name="searchString">Search String to limit results</param>
		/// <param name="page">Page number of results, starting at 0</param>
		/// <param name="pageSize">Page size to display</param>
		/// <param name="currentFilter">Last search string while paging</param>
		/// <returns>Task Action Result</returns>
		public async Task<IActionResult> Edit(
			long? id,
			string referer,
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			string currentFilter = "")
		{
			// pass page configuration to Views
			if (referer == null) referer = Request.Headers["Referer"];
			ViewData["referer"] = referer;
			ViewData["CurrentSort"] = sortOrder;
			ViewData["sortOrder"] = sortOrder;
			ViewData["searchString"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			ViewData["currentFilter"] = currentFilter;
			if (id == null) return NotFound();
			var result = await _context.TestFields.SingleOrDefaultAsync(m => m.Id == id);
			if (result == null) return NotFound();
			return View(result);
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.

		/// <summary>
		/// POST /TestFields/Edit/Id
		/// </summary>
		/// <param name="id">Id of record to edit</param>
		/// <param name="sortOrder">Page Sort Order</param>
		/// <param name="searchString">Search String to limit results</param>
		/// <param name="page">Page number of results, starting at 0</param>
		/// <param name="pageSize">Page size to display</param>
		/// <param name="currentFilter">Last search string while paging</param>
		/// <param name="item">The edited Item</param>
		/// <returns>Task Action Result</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(
			long? id,
			string referer,
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			[Bind("Id,Name,Description,MyBoolean,MyCreditCard,MyCurrency,MyDateTime,MyDouble,MyEmail,MyFloat,MyImageUrl,MyInteger,MyLong,MyPhone,MyPostalCode,MyString,MyTextArea,MyTicks,MyUrl,Comments,AuditEntered,AuditEnteredBy,AuditUpdated,AuditUpdatedBy")] TestField item,
			string currentFilter = "")
		{
			// pass page configuration to Views
			ViewData["CurrentSort"] = sortOrder;
			ViewData["sortOrder"] = sortOrder;
			ViewData["searchString"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			ViewData["currentFilter"] = currentFilter;
			if (id != item.Id) return NotFound();

			var oldItem = _context.TestFields.AsNoTracking().SingleOrDefault(m => m.Id == id);
			if (oldItem == null) return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(item);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!TestFieldExists(item.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				if (referer == null) return RedirectToAction("Index");
				return new RedirectResult(referer);
			}
			return View(item);
		}

		private bool TestFieldExists(long? id)
		{
		    return _context.TestFields.Any(e => e.Id == id);
		}


		/// <summary>
		/// GET /TestFields/Create
		/// </summary>
		/// <param name="referer">The go-back url</param>
		/// <param name="sortOrder">Page Sort Order</param>
		/// <param name="searchString">Search String to limit results</param>
		/// <param name="page">Page number of results, starting at 0</param>
		/// <param name="pageSize">Page size to display</param>
		/// <param name="currentFilter">Last search string while paging</param>
		/// <returns>Task Action Result</returns>
		public IActionResult Create(
			string referer,
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			string currentFilter = "")
		{
			// pass page configuration to Views
			if (referer == null) referer = Request.Headers["Referer"];
			ViewData["referer"] = referer;
			ViewData["CurrentSort"] = sortOrder;
			ViewData["sortOrder"] = sortOrder;
			ViewData["searchString"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			ViewData["currentFilter"] = currentFilter;
			var newItem = new TestField();


			return View(newItem);
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.

		/// <summary>
		/// POST /TestFields/Create/Id
		/// </summary>
		/// <param name="referer">The go-back url</param>
		/// <param name="sortOrder">Page Sort Order</param>
		/// <param name="searchString">Search String to limit results</param>
		/// <param name="page">Page number of results, starting at 0</param>
		/// <param name="pageSize">Page size to display</param>
		/// <param name="currentFilter">Last search string while paging</param>
		/// <param name="item">The edited Item</param>
		/// <returns>Task Action Result</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(
			long? id,
			string referer,
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			[Bind("Id,Name,Description,MyBoolean,MyCreditCard,MyCurrency,MyDateTime,MyDouble,MyEmail,MyFloat,MyImageUrl,MyInteger,MyLong,MyPhone,MyPostalCode,MyString,MyTextArea,MyTicks,MyUrl,Comments,AuditEntered,AuditEnteredBy,AuditUpdated,AuditUpdatedBy")] TestField item,
			string currentFilter = "")
		{
			// pass page configuration to Views
			ViewData["CurrentSort"] = sortOrder;
			ViewData["sortOrder"] = sortOrder;
			ViewData["searchString"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			ViewData["currentFilter"] = currentFilter;
			if (ModelState.IsValid)
			{
				_context.Add(item);
				await _context.SaveChangesAsync();
				if (referer == null) return RedirectToAction("Index");
				return new RedirectResult(referer);
			}
			return View(item);
		}


		/// <summary>
		/// GET /TestFields/Delete/Id
		/// </summary>
		/// <param name="id">Id of record to delete</param>
		/// <param name="referer">The go-back url</param>
		/// <param name="sortOrder">Page Sort Order</param>
		/// <param name="searchString">Search String to limit results</param>
		/// <param name="page">Page number of results, starting at 0</param>
		/// <param name="pageSize">Page size to display</param>
		/// <param name="currentFilter">Last search string while paging</param>
		/// <returns>Task Action Result</returns>
		public async Task<IActionResult> Delete(
			long? id,
			string referer,
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			string currentFilter = "")
		{
			// pass page configuration to Views
			if (referer == null) referer = Request.Headers["Referer"];
			ViewData["referer"] = referer;
			ViewData["CurrentSort"] = sortOrder;
			ViewData["sortOrder"] = sortOrder;
			ViewData["searchString"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			ViewData["currentFilter"] = currentFilter;
			if (id == null) return NotFound();
			var result = await _context.TestFields.SingleOrDefaultAsync(m => m.Id == id);
			if (result == null) return NotFound();
			return View(result);
		}

		/// <summary>
		/// POST /TestFields/Delete
		/// </summary>
		/// <param name="id"></param>
		/// <param name="referer">The go-back url</param>
		/// <returns></returns>
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(long? id, string referer)
		{
			var item = await _context.TestFields.SingleOrDefaultAsync(m => m.Id == id);
			_context.TestFields.Remove(item);
			await _context.SaveChangesAsync();
			if (referer == null) return RedirectToAction("Index");
			return new RedirectResult(referer);
		}

	} // End of Class TestFieldsController
} // End of Namespace Academy.Mentors.Administration.Controllers
