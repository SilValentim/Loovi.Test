namespace Loovi.Test.WebApi.Common;

public class PaginatedResponse<Model> //: ApiResponseWithData<IEnumerable<Model>>
{
    public IEnumerable<Model>? Data { get; set; }
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}