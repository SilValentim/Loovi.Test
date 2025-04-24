namespace Loovi.Test.Application.Common;

public class PaginatedResult<Model>
{
    public IEnumerable<Model>? Data { get; set; }
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}

