﻿using System;
using System.Collections.Generic;
using System.Linq;


namespace ShopWatch.BussinessLogicLayer
{
	public class PaginatedList<TEntity>:List<TEntity>
	{
        public PaginatedList(List<TEntity> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
            Counts = count;
        }

        public int PageIndex { get; }
        public int TotalPages { get; }
        public int Counts { get; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static PaginatedList<TEntity> CreateAsync(IQueryable<TEntity> source, int pageIndex,
            int pageSize)
        {
            var count =  source.Count();
            var entities = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<TEntity>(entities, count, pageIndex, pageSize);
        }
    }
}

