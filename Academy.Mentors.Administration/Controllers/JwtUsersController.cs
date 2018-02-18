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
	/// JwtUsers Administration MVC Controller (ASP.NET Core 1.1)
	/// </summary>
	[Authorize]
	public class JwtUsersController : Controller
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
		public JwtUsersController(AdministrationContext context)
		{
			_context = context;
		}

		/// <summary>
		/// GET /JwtUsers/Index
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
			ViewData["UserNameSortParm"] = sortOrder == "UserName" ? "UserName_desc" : "UserName";
			ViewData["PasswordSortParm"] = sortOrder == "Password" ? "Password_desc" : "Password";
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
				var results = _context.JwtUsers.Where(b => b.Id > -1 
					&& (b.Id == testId ||
						b.Id.ToString().Contains(searchString) ||
						b.UserName.Contains(searchString) ||
						b.Password.Contains(searchString) 
						)).Take(1000);
			//Set up column sorting
			switch (sortOrder)
			{
				case "Id": results = results.OrderBy(a => a.Id); break;
				case "Id_desc": results = results.OrderByDescending(a => a.Id); break;
				case "UserName": results = results.OrderBy(a => a.UserName); break;
				case "UserName_desc": results = results.OrderByDescending(a => a.UserName); break;
				case "Password": results = results.OrderBy(a => a.Password); break;
				case "Password_desc": results = results.OrderByDescending(a => a.Password); break;
				default: results = results.OrderBy(s => s.Id); break;
			}
			return View(await PaginatedList<JwtUser>.CreateAsync(results.AsNoTracking(), page ?? 1, pageSize ?? 10));
		}


		/// <summary>
		/// GET /JwtUsers/Select
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
			ViewData["UserNameSortParm"] = sortOrder == "UserName" ? "UserName_desc" : "UserName";
			ViewData["PasswordSortParm"] = sortOrder == "Password" ? "Password_desc" : "Password";
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
				var results = _context.JwtUsers.Where(b => b.Id > -1 
					&& (b.Id == testId ||
						b.Id.ToString().Contains(searchString) ||
						b.UserName.Contains(searchString) ||
						b.Password.Contains(searchString) 
						)).Take(1000);
			//Set up column sorting
			switch (sortOrder)
			{
				case "Id": results = results.OrderBy(a => a.Id); break;
				case "Id_desc": results = results.OrderByDescending(a => a.Id); break;
				case "UserName": results = results.OrderBy(a => a.UserName); break;
				case "UserName_desc": results = results.OrderByDescending(a => a.UserName); break;
				case "Password": results = results.OrderBy(a => a.Password); break;
				case "Password_desc": results = results.OrderByDescending(a => a.Password); break;
				default: results = results.OrderBy(s => s.Id); break;
			}
			return View(await PaginatedList<JwtUser>.CreateAsync(results.AsNoTracking(), page ?? 1, pageSize ?? 10));
		}


		/// <summary>
		/// GET /JwtUsers/Details/Id
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
			var result = await _context.JwtUsers.SingleOrDefaultAsync(m => m.Id == id);
			if (result == null) return NotFound();
			return View(result);
		}


		/// <summary>
		/// GET /JwtUsers/Edit/Id
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
			var result = await _context.JwtUsers.SingleOrDefaultAsync(m => m.Id == id);
			if (result == null) return NotFound();
			return View(result);
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.

		/// <summary>
		/// POST /JwtUsers/Edit/Id
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
			[Bind("Id,UserName,Password")] JwtUser item,
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

			var oldItem = _context.JwtUsers.AsNoTracking().SingleOrDefault(m => m.Id == id);
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
					if (!JwtUserExists(item.Id))
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

		private bool JwtUserExists(long? id)
		{
		    return _context.JwtUsers.Any(e => e.Id == id);
		}


		/// <summary>
		/// GET /JwtUsers/Create
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
			var newItem = new JwtUser();


			return View(newItem);
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.

		/// <summary>
		/// POST /JwtUsers/Create/Id
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
			[Bind("Id,UserName,Password")] JwtUser item,
			string currentFilter = "")
		{
			// pass page configuration to Views
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
		/// GET /JwtUsers/Delete/Id
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
			var result = await _context.JwtUsers.SingleOrDefaultAsync(m => m.Id == id);
			if (result == null) return NotFound();
			return View(result);
		}

		/// <summary>
		/// POST /JwtUsers/Delete
		/// </summary>
		/// <param name="id"></param>
		/// <param name="referer">The go-back url</param>
		/// <returns></returns>
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(long? id, string referer)
		{
			var item = await _context.JwtUsers.SingleOrDefaultAsync(m => m.Id == id);
			_context.JwtUsers.Remove(item);
			await _context.SaveChangesAsync();
			if (referer == null) return RedirectToAction("Index");
			return new RedirectResult(referer);
		}

	} // End of Class JwtUsersController
} // End of Namespace Academy.Mentors.Administration.Controllers
