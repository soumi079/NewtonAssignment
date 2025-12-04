using DLCGGameCore.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLCGGameCore.Application.Extensions
{
    public static class PagedResultExtensions
    {
        public static PagedResult<TDestination> MapTo<TSource, TDestination>(
            this PagedResult<TSource> source,
            Func<TSource, TDestination> selector)
        {
            return new PagedResult<TDestination>
            {
                CurrentPage = source.CurrentPage,
                PageSize = source.PageSize,
                TotalItemCount = source.TotalItemCount,
                Items = source.Items.Select(selector).ToList()
            };
        }
    }
}
