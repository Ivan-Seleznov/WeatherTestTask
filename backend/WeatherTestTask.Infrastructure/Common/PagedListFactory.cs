using Microsoft.EntityFrameworkCore;
using WeatherTestTask.Application.Common.PagedList;

namespace WeatherTestTask.Infrastructure.Common;

public static class PagedListFactory
{
    public static async Task<PagedList<T>> CreateWithQueryAsync<T>(IQueryable<T> query, int page, int pageSize, CancellationToken? cancellationToken = null)
    {
        var totalCount = await query.CountAsync(cancellationToken ?? CancellationToken.None);
        var items = await query.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken ?? CancellationToken.None);
        return PagedList<T>.Create(items, page, pageSize, totalCount);
    }
}