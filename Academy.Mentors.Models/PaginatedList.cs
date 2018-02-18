/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
using Microsoft.EntityFrameworkCore; 

namespace Academy.Mentors.Models 
{ 
    /// <summary> 
    /// Paginated list of Model Objects 
    /// </summary> 
    /// <typeparam name="T"></typeparam> 
    public class PaginatedList<T> : List<T> 
    { 
        /// <summary> 
        /// The page to render 
        /// </summary> 
        public int PageIndex { get; private set; } 

        /// <summary> 
        /// Total Pages 
        /// </summary> 
        public int TotalPages { get; private set; } 

        /// <summary> 
        /// Collect one page 
        /// </summary> 
        /// <param name="items"></param> 
        /// <param name="count"></param> 
        /// <param name="pageIndex"></param> 
        /// <param name="pageSize"></param> 
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize) 
        { 
            PageIndex = pageIndex; 
            TotalPages = (int)Math.Ceiling(count / (double)pageSize); 

            this.AddRange(items); 
        } 

        /// <summary> 
        /// True if there is a page prior to the current page 
        /// </summary> 
        public bool HasPreviousPage 
        { 
            get 
            { 
                return (PageIndex > 1); 
            } 
        } 

        /// <summary> 
        /// True if this is not the last page 
        /// </summary> 
        public bool HasNextPage 
        { 
            get 
            { 
                return (PageIndex < TotalPages); 
            } 
        } 

        /// <summary> 
        /// Create static page 
        /// </summary> 
        /// <param name="source"></param> 
        /// <param name="pageIndex"></param> 
        /// <param name="pageSize"></param> 
        /// <returns></returns> 
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize) 
        { 
            var count = await source.CountAsync(); 
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(); 
            return new PaginatedList<T>(items, count, pageIndex, pageSize); 
        } 
    } 
}  
