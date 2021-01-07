﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Pagination
{
    public class PagedList<T>: List<T>
    {
        public int CurrrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HastPrevious => CurrrentPage > 1;
        public bool HastNext => CurrrentPage < TotalPages;
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrrentPage = pageNumber;
            TotalPages   = (int)Math.Ceiling(count/(double)PageSize);
            AddRange(items);
        }
        public async static Task<PagedList<T>> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = await  source.Skip((pageNumber-1)* pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items,count,pageNumber,pageSize);
        }

    }
}
