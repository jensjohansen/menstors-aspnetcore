/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using Academy.Mentors.Administration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Academy.Mentors.Administration.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
        private AdministrationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">MVC Version of DataContext</param>
        public AdministrationController(AdministrationContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
