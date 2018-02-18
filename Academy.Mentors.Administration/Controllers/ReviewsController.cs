/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/


/* Powered by Solution Zone (http://www.solution.zone)  2/14/2018 1:37:43 PM */


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
	/// Reviews Administration MVC Controller (ASP.NET Core 1.1)
	/// </summary>
	[Authorize]
	public class ReviewsController : Controller
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
		public ReviewsController(AdministrationContext context)
		{
			_context = context;
		}

		/// <summary>
		/// GET /Reviews/Index
		/// </summary>
		/// <param name="contributorId">Filter on contributorId (null to not filter)</param>
		/// <param name="paperId">Filter on paperId (null to not filter)</param>
		/// <param name="paperVersionId">Filter on paperVersionId (null to not filter)</param>
		/// <param name="sortOrder">Page Sort Order</param>
		/// <param name="searchString">Search String to limit results</param>
		/// <param name="page">Page number of results, starting at 0</param>
		/// <param name="pageSize">Page size to display</param>
		/// <param name="currentFilter">Last search string while paging</param>
		/// <returns>Task Action Result</returns>
		public async Task<IActionResult> Index(
			long? contributorId,
			long? paperId,
			long? paperVersionId,
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			string currentFilter = "")
		{
			// pass page configuration to Views
			ViewData["CurrentSort"] = sortOrder;
			ViewData["contributorId"] = contributorId;
			ViewData["paperId"] = paperId;
			ViewData["paperVersionId"] = paperVersionId;
			// Set up column sorting
			ViewData["IdSortParm"] = sortOrder == "Id" ? "Id_desc" : "Id";
			ViewData["NameSortParm"] = sortOrder == "Name" ? "Name_desc" : "Name";
			ViewData["DescriptionSortParm"] = sortOrder == "Description" ? "Description_desc" : "Description";
			ViewData["ContributorIdSortParm"] = sortOrder == "ContributorId" ? "ContributorId_desc" : "ContributorId";
			ViewData["PaperIdSortParm"] = sortOrder == "PaperId" ? "PaperId_desc" : "PaperId";
			ViewData["PaperVersionIdSortParm"] = sortOrder == "PaperVersionId" ? "PaperVersionId_desc" : "PaperVersionId";
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
				var results = _context.Reviews.Where(b => b.Id > -1 
					&& (contributorId == null || b.ContributorId == testId || b.ContributorId == contributorId)
					&& (paperId == null || b.PaperId == testId || b.PaperId == paperId)
					&& (paperVersionId == null || b.PaperVersionId == testId || b.PaperVersionId == paperVersionId)
					&& (b.Id == testId ||
						b.Id.ToString().Contains(searchString) ||
						b.Name.Contains(searchString) ||
						b.Description.Contains(searchString) ||
						b.ContributorId.ToString().Contains(searchString) ||
						b.PaperId.ToString().Contains(searchString) ||
						b.PaperVersionId.ToString().Contains(searchString) ||
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
				case "ContributorId": results = results.OrderBy(a => a.ContributorId); break;
				case "ContributorId_desc": results = results.OrderByDescending(a => a.ContributorId); break;
				case "PaperId": results = results.OrderBy(a => a.PaperId); break;
				case "PaperId_desc": results = results.OrderByDescending(a => a.PaperId); break;
				case "PaperVersionId": results = results.OrderBy(a => a.PaperVersionId); break;
				case "PaperVersionId_desc": results = results.OrderByDescending(a => a.PaperVersionId); break;
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
			return View(await PaginatedList<Review>.CreateAsync(results.AsNoTracking(), page ?? 1, pageSize ?? 10));
		}


		/// <summary>
		/// GET /Reviews/Select
		/// </summary>
		/// <param name="contributorId">Filter on contributorId (null to not filter)</param>
		/// <param name="paperId">Filter on paperId (null to not filter)</param>
		/// <param name="paperVersionId">Filter on paperVersionId (null to not filter)</param>
		/// <param name="selectedId">Already selected ID</param>
		/// <param name="referred">URI of caller</param>
		/// <param name="sortOrder">Page Sort Order</param>
		/// <param name="searchString">Search String to limit results</param>
		/// <param name="page">Page number of results, starting at 0</param>
		/// <param name="pageSize">Page size to display</param>
		/// <param name="currentFilter">Last search string while paging</param>
		/// <returns>Task Action Result</returns>
		public async Task<IActionResult> Select(
			long? contributorId,
			long? paperId,
			long? paperVersionId,
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
			ViewData["contributorId"] = contributorId;
			ViewData["paperId"] = paperId;
			ViewData["paperVersionId"] = paperVersionId;
			// Set up column sorting
			ViewData["IdSortParm"] = sortOrder == "Id" ? "Id_desc" : "Id";
			ViewData["NameSortParm"] = sortOrder == "Name" ? "Name_desc" : "Name";
			ViewData["DescriptionSortParm"] = sortOrder == "Description" ? "Description_desc" : "Description";
			ViewData["ContributorIdSortParm"] = sortOrder == "ContributorId" ? "ContributorId_desc" : "ContributorId";
			ViewData["PaperIdSortParm"] = sortOrder == "PaperId" ? "PaperId_desc" : "PaperId";
			ViewData["PaperVersionIdSortParm"] = sortOrder == "PaperVersionId" ? "PaperVersionId_desc" : "PaperVersionId";
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
				var results = _context.Reviews.Where(b => b.Id > -1 
					&& (contributorId == null || b.ContributorId == testId || b.ContributorId == contributorId)
					&& (paperId == null || b.PaperId == testId || b.PaperId == paperId)
					&& (paperVersionId == null || b.PaperVersionId == testId || b.PaperVersionId == paperVersionId)
					&& (b.Id == testId ||
						b.Id.ToString().Contains(searchString) ||
						b.Name.Contains(searchString) ||
						b.Description.Contains(searchString) ||
						b.ContributorId.ToString().Contains(searchString) ||
						b.PaperId.ToString().Contains(searchString) ||
						b.PaperVersionId.ToString().Contains(searchString) ||
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
				case "ContributorId": results = results.OrderBy(a => a.ContributorId); break;
				case "ContributorId_desc": results = results.OrderByDescending(a => a.ContributorId); break;
				case "PaperId": results = results.OrderBy(a => a.PaperId); break;
				case "PaperId_desc": results = results.OrderByDescending(a => a.PaperId); break;
				case "PaperVersionId": results = results.OrderBy(a => a.PaperVersionId); break;
				case "PaperVersionId_desc": results = results.OrderByDescending(a => a.PaperVersionId); break;
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
			return View(await PaginatedList<Review>.CreateAsync(results.AsNoTracking(), page ?? 1, pageSize ?? 10));
		}


		/// <summary>
		/// GET /Reviews/Details/Id
		/// </summary>
		/// <param name="id">Id of record to display</param>
		/// <param name="contributorId">Hold Filter on contributorId (null to not filter)</param>
		/// <param name="paperId">Hold Filter on paperId (null to not filter)</param>
		/// <param name="paperVersionId">Hold Filter on paperVersionId (null to not filter)</param>
		/// <param name="referer">The go-back url</param>
		/// <param name="sortOrder">Page Sort Order</param>
		/// <param name="searchString">Search String to limit results</param>
		/// <param name="page">Page number of results, starting at 0</param>
		/// <param name="pageSize">Page size to display</param>
		/// <param name="currentFilter">Last search string while paging</param>
		/// <returns>Task Action Result</returns>
		public async Task<IActionResult> Details(
			long? id,
			long? contributorId,
			long? paperId,
			long? paperVersionId,
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
			ViewData["contributorId"] = contributorId;
			ViewData["paperId"] = paperId;
			ViewData["paperVersionId"] = paperVersionId;
			if (id == null) return NotFound();
			var result = await _context.Reviews.SingleOrDefaultAsync(m => m.Id == id);
			if (result == null) return NotFound();
			return View(result);
		}


		/// <summary>
		/// GET /Reviews/Edit/Id
		/// </summary>
		/// <param name="id">Id of record to edit</param>
		/// <param name="contributorId">Hold Filter on contributorId (null to not filter)</param>
		/// <param name="paperId">Hold Filter on paperId (null to not filter)</param>
		/// <param name="paperVersionId">Hold Filter on paperVersionId (null to not filter)</param>
		/// <param name="referer">The go-back url</param>
		/// <param name="sortOrder">Page Sort Order</param>
		/// <param name="searchString">Search String to limit results</param>
		/// <param name="page">Page number of results, starting at 0</param>
		/// <param name="pageSize">Page size to display</param>
		/// <param name="currentFilter">Last search string while paging</param>
		/// <returns>Task Action Result</returns>
		public async Task<IActionResult> Edit(
			long? id,
			long? contributorId,
			long? paperId,
			long? paperVersionId,
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
			ViewData["contributorId"] = contributorId;
			ViewData["paperId"] = paperId;
			ViewData["paperVersionId"] = paperVersionId;
			if (id == null) return NotFound();
			var result = await _context.Reviews.SingleOrDefaultAsync(m => m.Id == id);
			if (result == null) return NotFound();
			if (contributorId != null) result.ContributorId = contributorId;
			if (paperId != null) result.PaperId = paperId;
			if (paperVersionId != null) result.PaperVersionId = paperVersionId;
			return View(result);
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.

		/// <summary>
		/// POST /Reviews/Edit/Id
		/// </summary>
		/// <param name="id">Id of record to edit</param>
		/// <param name="contributorId">Hold Filter on contributorId (null to not filter)</param>
		/// <param name="paperId">Hold Filter on paperId (null to not filter)</param>
		/// <param name="paperVersionId">Hold Filter on paperVersionId (null to not filter)</param>
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
			long? contributorId,
			long? paperId,
			long? paperVersionId,
			string referer,
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			[Bind("Id,Name,Description,ContributorId,PaperId,PaperVersionId,Comments,AuditEntered,AuditEnteredBy,AuditUpdated,AuditUpdatedBy")] Review item,
			string currentFilter = "")
		{
			// pass page configuration to Views
			ViewData["CurrentSort"] = sortOrder;
			ViewData["sortOrder"] = sortOrder;
			ViewData["searchString"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			ViewData["currentFilter"] = currentFilter;
			ViewData["contributorId"] = contributorId;
			ViewData["paperId"] = paperId;
			ViewData["paperVersionId"] = paperVersionId;
			if (id != item.Id) return NotFound();

			var oldItem = _context.Reviews.AsNoTracking().SingleOrDefault(m => m.Id == id);
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
					if (!ReviewExists(item.Id))
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

		private bool ReviewExists(long? id)
		{
		    return _context.Reviews.Any(e => e.Id == id);
		}


		/// <summary>
		/// GET /Reviews/Create
		/// </summary>
		/// <param name="contributorId">Hold Filter on contributorId (null to not filter)</param>
		/// <param name="paperId">Hold Filter on paperId (null to not filter)</param>
		/// <param name="paperVersionId">Hold Filter on paperVersionId (null to not filter)</param>
		/// <param name="referer">The go-back url</param>
		/// <param name="sortOrder">Page Sort Order</param>
		/// <param name="searchString">Search String to limit results</param>
		/// <param name="page">Page number of results, starting at 0</param>
		/// <param name="pageSize">Page size to display</param>
		/// <param name="currentFilter">Last search string while paging</param>
		/// <returns>Task Action Result</returns>
		public IActionResult Create(
			long? contributorId,
			long? paperId,
			long? paperVersionId,
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
			ViewData["contributorId"] = contributorId;
			ViewData["paperId"] = paperId;
			ViewData["paperVersionId"] = paperVersionId;
			var newItem = new Review();

			if (contributorId != null) newItem.ContributorId = contributorId;
			if (paperId != null) newItem.PaperId = paperId;
			if (paperVersionId != null) newItem.PaperVersionId = paperVersionId;

			return View(newItem);
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.

		/// <summary>
		/// POST /Reviews/Create/Id
		/// </summary>
		/// <param name="contributorId">Hold Filter on contributorId (null to not filter)</param>
		/// <param name="paperId">Hold Filter on paperId (null to not filter)</param>
		/// <param name="paperVersionId">Hold Filter on paperVersionId (null to not filter)</param>
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
			long? contributorId,
			long? paperId,
			long? paperVersionId,
			string referer,
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			[Bind("Id,Name,Description,ContributorId,PaperId,PaperVersionId,Comments,AuditEntered,AuditEnteredBy,AuditUpdated,AuditUpdatedBy")] Review item,
			string currentFilter = "")
		{
			// pass page configuration to Views
			ViewData["CurrentSort"] = sortOrder;
			ViewData["sortOrder"] = sortOrder;
			ViewData["searchString"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			ViewData["currentFilter"] = currentFilter;
			ViewData["contributorId"] = contributorId;
			ViewData["paperId"] = paperId;
			ViewData["paperVersionId"] = paperVersionId;
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
		/// GET /Reviews/Delete/Id
		/// </summary>
		/// <param name="id">Id of record to delete</param>
		/// <param name="contributorId">Hold Filter on contributorId (null to not filter)</param>
		/// <param name="paperId">Hold Filter on paperId (null to not filter)</param>
		/// <param name="paperVersionId">Hold Filter on paperVersionId (null to not filter)</param>
		/// <param name="referer">The go-back url</param>
		/// <param name="sortOrder">Page Sort Order</param>
		/// <param name="searchString">Search String to limit results</param>
		/// <param name="page">Page number of results, starting at 0</param>
		/// <param name="pageSize">Page size to display</param>
		/// <param name="currentFilter">Last search string while paging</param>
		/// <returns>Task Action Result</returns>
		public async Task<IActionResult> Delete(
			long? id,
			long? contributorId,
			long? paperId,
			long? paperVersionId,
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
			ViewData["contributorId"] = contributorId;
			ViewData["paperId"] = paperId;
			ViewData["paperVersionId"] = paperVersionId;
			if (id == null) return NotFound();
			var result = await _context.Reviews.SingleOrDefaultAsync(m => m.Id == id);
			if (result == null) return NotFound();
			return View(result);
		}

		/// <summary>
		/// POST /Reviews/Delete
		/// </summary>
		/// <param name="id"></param>
		/// <param name="referer">The go-back url</param>
		/// <returns></returns>
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(long? id, string referer)
		{
			var item = await _context.Reviews.SingleOrDefaultAsync(m => m.Id == id);
			_context.Reviews.Remove(item);
			await _context.SaveChangesAsync();
			if (referer == null) return RedirectToAction("Index");
			return new RedirectResult(referer);
		}

	} // End of Class ReviewsController
} // End of Namespace Academy.Mentors.Administration.Controllers
