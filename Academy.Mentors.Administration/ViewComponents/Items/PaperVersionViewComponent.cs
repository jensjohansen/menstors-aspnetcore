/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/


/* Powered by Solution Zone (http://www.solution.zone)  2/14/2018 1:37:43 PM */


using Academy.Mentors.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Mentors.Administration.ViewComponents.Items
{
	/// <summary>
	/// PaperVersion Item View Component (ASP.NET Core 1.1)
	/// </summary>
	public class PaperVersionViewComponent : ViewComponent
	{
		private AdministrationContext _context;


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="context">MVC Version of DataContext</param>
		public PaperVersionViewComponent(AdministrationContext context)
		{
			_context = context;
		}

		/// <summary>
		/// View Component PaperVersion
		/// </summary>
		/// <param name="id">Id of record to display</param>
		/// <param name="selectedId">Id of already selected id</param>
		/// <param name="referred">referring callback URI</param>
		/// <param name="returnField">The parameter name to pass back for select view</param>
		/// <param name="viewType">The type of reference to render</param>
		/// <param name="contributorId">Hold Filter on contributorId (null to not filter)</param>
		/// <param name="paperId">Hold Filter on paperId (null to not filter)</param>
		/// <param name="auditEnteredBy">Hold Filter on auditEnteredBy (null to not filter)</param>
		/// <param name="auditUpdatedBy">Hold Filter on auditUpdatedBy (null to not filter)</param>
		/// <param name="sortOrder">Page Sort Order</param>
		/// <param name="searchString">Search String to limit results</param>
		/// <param name="page">Page number of results, starting at 0</param>
		/// <param name="pageSize">Page size to display</param>
		/// <param name="currentFilter">Last search string while paging</param>
		/// <returns>Task Action Result</returns>
		public async Task<IViewComponentResult> InvokeAsync(
			long? id,
			long? selectedId,
			string referred,
			string returnField,
			string viewType,
			long? contributorId,
			long? paperId,
			long? auditEnteredBy,
			long? auditUpdatedBy,
			string sortOrder,
			string searchString,
			int? page,
			int? pageSize,
			string currentFilter)
		{
			if (string.IsNullOrEmpty(viewType)) viewType = "Default";
			// pass page configuration to Views
			ViewData["id"] = id;
			ViewData["selectedId"] = selectedId;
			ViewData["referred"] = referred;
			if (returnField == null) returnField = "paperVersionId";
			ViewData["returnField"] = returnField;
			ViewData["viewType"] = viewType;
			ViewData["CurrentSort"] = sortOrder;
			ViewData["sortOrder"] = sortOrder;
			ViewData["searchString"] = searchString;
			ViewData["page"] = page;
			ViewData["pageSize"] = pageSize;
			ViewData["currentFilter"] = currentFilter;
			ViewData["contributorId"] = contributorId;
			ViewData["paperId"] = paperId;
			ViewData["auditEnteredBy"] = auditEnteredBy;
			ViewData["auditUpdatedBy"] = auditUpdatedBy;
			var item = await _context.PaperVersions.SingleOrDefaultAsync(m => m.Id == id);
			if (item == null) item = new PaperVersion();
			return View(viewType,item);
		}

	} // End of Class PaperVersionViewComponent
} // End of Namespace Academy.Mentors.Administration.ViewComponents.Items
