namespace Catalog.Host.Models.Requests
{
    public class PaginatedItemsByTypesRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<int> Data { get; init; } = null!;
    }
}
