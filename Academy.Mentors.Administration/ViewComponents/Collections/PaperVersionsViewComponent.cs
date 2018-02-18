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

namespace Academy.Mentors.Administration.ViewComponents.Collections
{
	/// <summary>
	/// PaperVersions Collection View Component (ASP.NET Core 1.1)
	/// </summary>
	public class PaperVersionsViewComponent : ViewComponent
	{
		private AdministrationContext _context;


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="context">MVC Version of DataContext</param>
		public PaperVersionsViewComponent(AdministrationContext context)
		{
			_context = context;
		}

		/// <summary>
		/// PaperVersions View Component (lists)
		/// </summary>
		/// <param name="viewType">Name of the list view to render</param>
		/// <param name="contributorId">Filter on contributorId (null to not filter)</param>
		/// <param name="paperId">Filter on paperId (null to not filter)</param>
		/// <param name="auditEnteredBy">Filter on auditEnteredBy (null to not filter)</param>
		/// <param name="auditUpdatedBy">Filter on auditUpdatedBy (null to not filter)</param>
		/// <param name="sortOrder">Page Sort Order</param>
		/// <param name="searchString">Search String to limit results</param>
		/// <param name="paperVersionsPage">Page number of results, starting at 0</param>
		/// <param name="pageSize">Page size to display</param>
		/// <param name="currentFilter">Last search string while paging</param>
		/// <returns>Task Action Result</returns>
		public async Task<IViewComponentResult> InvokeAsync(
			string viewType,
			long? contributorId,
			long? paperId,
			long? auditEnteredBy,
			long? auditUpdatedBy,
			string sortOrder,
			string paperVersionsPage,
			int? pageSize,
			string searchString,
			string currentSort = "",
			string currentFilter = "")
		{
			if (String.IsNullOrEmpty(viewType)) viewType = "Default";
			// pass page configuration to Views
			ViewData["viewType"] = viewType;
			ViewData["CurrentSort"] = sortOrder;

			if (string.IsNullOrEmpty(paperVersionsPage)) paperVersionsPage = "1";
			int page = 1;
			int.TryParse(paperVersionsPage, out page);
			ViewData["contributorId"] = contributorId;
			ViewData["paperId"] = paperId;
			ViewData["auditEnteredBy"] = auditEnteredBy;
			ViewData["auditUpdatedBy"] = auditUpdatedBy;
			// Set up column sorting
			ViewData["IdSortParm"] = sortOrder == "Id" ? "Id_desc" : "Id";
			ViewData["NameSortParm"] = sortOrder == "Name" ? "Name_desc" : "Name";
			ViewData["DescriptionSortParm"] = sortOrder == "Description" ? "Description_desc" : "Description";
			ViewData["ContentSortParm"] = sortOrder == "Content" ? "Content_desc" : "Content";
			ViewData["ContributorIdSortParm"] = sortOrder == "ContributorId" ? "ContributorId_desc" : "ContributorId";
			ViewData["PaperIdSortParm"] = sortOrder == "PaperId" ? "PaperId_desc" : "PaperId";
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
			ViewData["paperVersionsPage"] = page.ToString();
			ViewData["pageSize"] = pageSize;
			int testId = -1;
			int.TryParse(searchString, out testId);
			// Get data from context
				var results = _context.PaperVersions.Where(b => b.Id > -1 
					&& (contributorId == null || b.ContributorId.Equals(contributorId))
					&& (paperId == null || b.PaperId.Equals(paperId))
					&& (auditEnteredBy == null || b.AuditEnteredBy.Equals(auditEnteredBy))
					&& (auditUpdatedBy == null || b.AuditUpdatedBy.Equals(auditUpdatedBy))
					&& (b.Id == testId ||
						b.Id.ToString().Contains(searchString) ||
						b.Name.Contains(searchString) ||
						b.Description.Contains(searchString) ||
						b.Content.Contains(searchString) ||
						b.ContributorId.ToString().Contains(searchString) ||
						b.PaperId.ToString().Contains(searchString) ||
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
				case "Content": results = results.OrderBy(a => a.Content); break;
				case "Content_desc": results = results.OrderByDescending(a => a.Content); break;
				case "ContributorId": results = results.OrderBy(a => a.ContributorId); break;
				case "ContributorId_desc": results = results.OrderByDescending(a => a.ContributorId); break;
				case "PaperId": results = results.OrderBy(a => a.PaperId); break;
				case "PaperId_desc": results = results.OrderByDescending(a => a.PaperId); break;
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
			return View(await PaginatedList<PaperVersion>.CreateAsync(results.AsNoTracking(), page, pageSize ?? 4));
		}

	} // End of Class PaperVersionsViewComponent
} // End of Namespace Academy.Mentors.Administration.ViewComponents.Collections
