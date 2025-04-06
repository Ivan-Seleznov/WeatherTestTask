namespace WeatherTestTask.Application.Common.PagedList;
public class PagedList<T>
{
    private PagedList(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public List<T> Items { get;}

    public int Page { get; }
    public int PageSize { get; }
    public int TotalCount { get; }

    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;

    public static PagedList<T> Create(List<T> items, int page, int pageSize, int totalCount) =>
        new (items, page, pageSize, totalCount);
    
    public static PagedList<T> CreateEmpty(int page, int pageSize) =>
        new (new List<T>(), page, pageSize, 0);
}
