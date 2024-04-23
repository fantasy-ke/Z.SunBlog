using Microsoft.EntityFrameworkCore;
using Z.Fantasy.Core.ResultResponse.Pager;

namespace Z.EntityFrameworkCore.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    /// 分页拓展
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public static async Task<PageResult<T>> ToPagedListAsync<T>(
        this IQueryable<T> queryable,
        IPagination input
    )
    {
        var items = await queryable
            .Skip((input.PageNo - 1) * input.PageSize)
            .Take(input.PageSize)
            .ToListAsync();
        var totalCount = await queryable.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)input.PageSize);
        return new PageResult<T>()
        {
            PageNo = input.PageNo,
            PageSize = input.PageSize,
            Rows = items,
            Total = (int)totalCount,
            Pages = totalPages,
        };
    }
    
}
