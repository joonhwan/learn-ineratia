namespace AspApp.Models;

// Models/PaginatedResponse.cs
public class PaginatedResponse<T>
{
    public ICollection<T> Items { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
    
    public static PaginatedResponse<T> Create(PaginationParameters paginationParameters, IQueryable<T> query)
    {
        var data = new PaginatedResponse<T>
        {
            PageSize = paginationParameters.PageSize,
            PageNumber = paginationParameters.Page,
            TotalCount = query.Count(),
            Items = query
                .Skip(int.Max(paginationParameters.Page - 1, 0) * paginationParameters.PageSize)
                .Take(paginationParameters.PageSize)
                .ToList()
        };
        return data;
    }
}
