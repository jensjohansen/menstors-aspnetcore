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
	/// Contributors Administration MVC Controller (ASP.NET Core 1.1)
	/// </summary>
	[Authorize]
	public class ContributorsController : Controller
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
		public ContributorsController(AdministrationContext context)
		{
			_context = context;
		}

        /// <summary>
        /// GET /Contributors/Index
        /// </summary>
        /// <param name="sortOrder">Page Sort Order</param>
        /// <param name="searchString">Search String to limit results</param>
        /// <param name="state">Serive State</param>
        /// <param name="page">Page number of results, starting at 0</param>
        /// <param name="pageSize">Page size to display</param>
        /// <param name="currentFilter">Last search string while paging</param>
        /// <returns>Task Action Result</returns>
        public async Task<IActionResult> Index(
            string sortOrder,
            string searchString,
            string state,
			int? page,
			int? pageSize,
			string currentFilter = "")
		{
            // Manage call stack
            var stack = new ServiceStack(state);
            var referer = stack.GetReturnUrl();
            var callstack = stack.AddReturnUrl(Request.Headers["referer"]);
            // pass page configuration to Views
            ViewData["referer"] = referer;
            ViewData["callstack"] = callstack;
			ViewData["CurrentSort"] = sortOrder;
			// Set up column sorting
			ViewData["IdSortParm"] = sortOrder == "Id" ? "Id_desc" : "Id";
			ViewData["NameSortParm"] = sortOrder == "Name" ? "Name_desc" : "Name";
			ViewData["DescriptionSortParm"] = sortOrder == "Description" ? "Description_desc" : "Description";
			ViewData["DegreeSortParm"] = sortOrder == "Degree" ? "Degree_desc" : "Degree";
			ViewData["AlmaMaterSortParm"] = sortOrder == "AlmaMater" ? "AlmaMater_desc" : "AlmaMater";
			ViewData["EmailSortParm"] = sortOrder == "Email" ? "Email_desc" : "Email";
			ViewData["EvaluationsSortParm"] = sortOrder == "Evaluations" ? "Evaluations_desc" : "Evaluations";
			ViewData["PasswordSortParm"] = sortOrder == "Password" ? "Password_desc" : "Password";
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
				var results = _context.Contributors.Where(b => b.Id > -1 
					&& (b.Id == testId ||
						b.Id.ToString().Contains(searchString) ||
						b.Name.Contains(searchString) ||
						b.Description.Contains(searchString) ||
						b.Degree.Contains(searchString) ||
						b.AlmaMater.Contains(searchString) ||
						b.Email.Contains(searchString) ||
						b.Evaluations.ToString().Contains(searchString) ||
						b.Password.Contains(searchString) ||
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
				case "Degree": results = results.OrderBy(a => a.Degree); break;
				case "Degree_desc": results = results.OrderByDescending(a => a.Degree); break;
				case "AlmaMater": results = results.OrderBy(a => a.AlmaMater); break;
				case "AlmaMater_desc": results = results.OrderByDescending(a => a.AlmaMater); break;
				case "Email": results = results.OrderBy(a => a.Email); break;
				case "Email_desc": results = results.OrderByDescending(a => a.Email); break;
				case "Evaluations": results = results.OrderBy(a => a.Evaluations); break;
				case "Evaluations_desc": results = results.OrderByDescending(a => a.Evaluations); break;
				case "Password": results = results.OrderBy(a => a.Password); break;
				case "Password_desc": results = results.OrderByDescending(a => a.Password); break;
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
			return View(await PaginatedList<Contributor>.CreateAsync(results.AsNoTracking(), page ?? 1, pageSize ?? 10));
		}


		/// <summary>
		/// GET /Contributors/Select
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
			string state,
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			string currentFilter = "")
		{
            // Manage call stack
            var stack = new ServiceStack(state);
            var referer = stack.GetReturnUrl();
            var callstack = stack.AddReturnUrl(Request.Headers["referer"]);
            // pass page configuration to Views
            ViewData["referer"] = referer;
            ViewData["callstack"] = callstack;
            if (searchString == null) searchString = ""; 
			// Pass paging settings
			ViewData["CurrentFilter"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			ViewData["selectedId"] = selectedId;
			ViewData["referred"] = referer;
			int testId = -1;
			int.TryParse(searchString,out testId);
			// Set up column sorting
			ViewData["IdSortParm"] = sortOrder == "Id" ? "Id_desc" : "Id";
			ViewData["NameSortParm"] = sortOrder == "Name" ? "Name_desc" : "Name";
			ViewData["DescriptionSortParm"] = sortOrder == "Description" ? "Description_desc" : "Description";
			ViewData["DegreeSortParm"] = sortOrder == "Degree" ? "Degree_desc" : "Degree";
			ViewData["AlmaMaterSortParm"] = sortOrder == "AlmaMater" ? "AlmaMater_desc" : "AlmaMater";
			ViewData["EmailSortParm"] = sortOrder == "Email" ? "Email_desc" : "Email";
			ViewData["EvaluationsSortParm"] = sortOrder == "Evaluations" ? "Evaluations_desc" : "Evaluations";
			ViewData["PasswordSortParm"] = sortOrder == "Password" ? "Password_desc" : "Password";
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
				var results = _context.Contributors.Where(b => b.Id > -1 
					&& (b.Id == testId ||
						b.Id.ToString().Contains(searchString) ||
						b.Name.Contains(searchString) ||
						b.Description.Contains(searchString) ||
						b.Degree.Contains(searchString) ||
						b.AlmaMater.Contains(searchString) ||
						b.Email.Contains(searchString) ||
						b.Evaluations.ToString().Contains(searchString) ||
						b.Password.Contains(searchString) ||
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
				case "Degree": results = results.OrderBy(a => a.Degree); break;
				case "Degree_desc": results = results.OrderByDescending(a => a.Degree); break;
				case "AlmaMater": results = results.OrderBy(a => a.AlmaMater); break;
				case "AlmaMater_desc": results = results.OrderByDescending(a => a.AlmaMater); break;
				case "Email": results = results.OrderBy(a => a.Email); break;
				case "Email_desc": results = results.OrderByDescending(a => a.Email); break;
				case "Evaluations": results = results.OrderBy(a => a.Evaluations); break;
				case "Evaluations_desc": results = results.OrderByDescending(a => a.Evaluations); break;
				case "Password": results = results.OrderBy(a => a.Password); break;
				case "Password_desc": results = results.OrderByDescending(a => a.Password); break;
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
			return View(await PaginatedList<Contributor>.CreateAsync(results.AsNoTracking(), page ?? 1, pageSize ?? 10));
		}


		/// <summary>
		/// GET /Contributors/Details/Id
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
			string state,
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			string currentFilter = "")
		{
            // Manage call stack
            var stack = new ServiceStack(state);
            var referer = stack.GetReturnUrl();
            var callstack = stack.AddReturnUrl(Request.Headers["referer"]);
            // pass page configuration to Views
            ViewData["referer"] = referer;
            ViewData["callstack"] = callstack;
            // pass page configuration to Views
            ViewData["CurrentSort"] = sortOrder;
			ViewData["sortOrder"] = sortOrder;
			ViewData["searchString"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			ViewData["currentFilter"] = currentFilter;
			if (id == null) return NotFound();
			var result = await _context.Contributors.SingleOrDefaultAsync(m => m.Id == id);
			if (result == null) return NotFound();
			return View(result);
		}


		/// <summary>
		/// GET /Contributors/Edit/Id
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
			string state,
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			string currentFilter = "")
		{
            // Manage call stack
            var stack = new ServiceStack(state);
            var referer = stack.GetReturnUrl();
            var callstack = stack.AddReturnUrl(Request.Headers["referer"]);
            // pass page configuration to Views
            ViewData["referer"] = referer;
            ViewData["callstack"] = callstack;
            // pass page configuration to Views
			ViewData["CurrentSort"] = sortOrder;
			ViewData["sortOrder"] = sortOrder;
			ViewData["searchString"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			ViewData["currentFilter"] = currentFilter;
			if (id == null) return NotFound();
			var result = await _context.Contributors.SingleOrDefaultAsync(m => m.Id == id);
			if (result == null) return NotFound();
			return View(result);
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.

		/// <summary>
		/// POST /Contributors/Edit/Id
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
			string state,
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			[Bind("Id,Name,Description,Degree,AlmaMater,Email,Evaluations,Password,Comments,AuditEntered,AuditEnteredBy,AuditUpdated,AuditUpdatedBy")] Contributor item,
			string currentFilter = "")
		{
            // Manage call stack
            var stack = new ServiceStack(state);
            var referer = stack.GetReturnUrl();
            var callstack = state;
            // pass page configuration to Views
            ViewData["referer"] = referer;
            ViewData["callstack"] = callstack;
            // pass page configuration to Views
            ViewData["CurrentSort"] = sortOrder;
			ViewData["sortOrder"] = sortOrder;
			ViewData["searchString"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			ViewData["currentFilter"] = currentFilter;
			if (id != item.Id) return NotFound();

			var oldItem = _context.Contributors.AsNoTracking().SingleOrDefault(m => m.Id == id);
			if (oldItem == null) return NotFound();

			if (item.Password != null && !item.Password.Equals(oldItem.Password)) 
				item.Password = _tokenizer.GetSHA1HashData(item.Password); 
			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(item);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ContributorExists(item.Id))
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

		private bool ContributorExists(long? id)
		{
		    return _context.Contributors.Any(e => e.Id == id);
		}


		/// <summary>
		/// GET /Contributors/Create
		/// </summary>
		/// <param name="referer">The go-back url</param>
		/// <param name="sortOrder">Page Sort Order</param>
		/// <param name="searchString">Search String to limit results</param>
		/// <param name="page">Page number of results, starting at 0</param>
		/// <param name="pageSize">Page size to display</param>
		/// <param name="currentFilter">Last search string while paging</param>
		/// <returns>Task Action Result</returns>
		public IActionResult Create(
			string state,
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			string currentFilter = "")
		{
            // Manage call stack
            var stack = new ServiceStack(state);
            var referer = stack.GetReturnUrl();
            var callstack = stack.AddReturnUrl(Request.Headers["referer"]);
            // pass page configuration to Views
            ViewData["referer"] = referer;
            ViewData["callstack"] = callstack;
			ViewData["CurrentSort"] = sortOrder;
			ViewData["sortOrder"] = sortOrder;
			ViewData["searchString"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			ViewData["currentFilter"] = currentFilter;
			var newItem = new Contributor();


			return View(newItem);
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.

		/// <summary>
		/// POST /Contributors/Create/Id
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
			string state,
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			[Bind("Id,Name,Description,Degree,AlmaMater,Email,Evaluations,Password,Comments,AuditEntered,AuditEnteredBy,AuditUpdated,AuditUpdatedBy")] Contributor item,
			string currentFilter = "")
		{
            // Manage call stack
            var stack = new ServiceStack(state);
            var referer = stack.GetReturnUrl();
            var callstack = state;
            // pass page configuration to Views
            ViewData["referer"] = referer;
            ViewData["callstack"] = callstack;
            ViewData["CurrentSort"] = sortOrder;
			ViewData["sortOrder"] = sortOrder;
			ViewData["searchString"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			ViewData["currentFilter"] = currentFilter;
			item.Password = _tokenizer.GetSHA1HashData(item.Password); 
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
		/// GET /Contributors/Delete/Id
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
			string state,
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			string currentFilter = "")
		{
            // Manage call stack
            var stack = new ServiceStack(state);
            var referer = stack.GetReturnUrl();
            var callstack = stack.AddReturnUrl(Request.Headers["referer"]);
            // pass page configuration to Views
            ViewData["referer"] = referer;
            ViewData["callstack"] = callstack;
            ViewData["CurrentSort"] = sortOrder;
			ViewData["sortOrder"] = sortOrder;
			ViewData["searchString"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			ViewData["currentFilter"] = currentFilter;
			if (id == null) return NotFound();
			var result = await _context.Contributors.SingleOrDefaultAsync(m => m.Id == id);
			if (result == null) return NotFound();
			return View(result);
		}

		/// <summary>
		/// POST /Contributors/Delete
		/// </summary>
		/// <param name="id"></param>
		/// <param name="referer">The go-back url</param>
		/// <returns></returns>
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(long? id, string state)
		{
            // Manage call stack
            var stack = new ServiceStack(state);
            var referer = stack.GetReturnUrl();
            var callstack = state;
            // pass page configuration to Views
            ViewData["referer"] = referer;
            ViewData["callstack"] = callstack;
            var item = await _context.Contributors.SingleOrDefaultAsync(m => m.Id == id);
			_context.Contributors.Remove(item);
			await _context.SaveChangesAsync();
			if (referer == null) return RedirectToAction("Index");
			return new RedirectResult(referer);
		}

	} // End of Class ContributorsController
} // End of Namespace Academy.Mentors.Administration.Controllers
