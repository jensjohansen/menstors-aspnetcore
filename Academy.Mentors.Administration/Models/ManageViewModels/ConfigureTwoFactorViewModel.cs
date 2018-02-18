/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Academy.Mentors.Administration.Models.ManageViewModels
{
    /// <summary>
    /// Configure Two Factor View Model
    /// </summary>
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }

        public ICollection<SelectListItem> Providers { get; set; }
    }
}
