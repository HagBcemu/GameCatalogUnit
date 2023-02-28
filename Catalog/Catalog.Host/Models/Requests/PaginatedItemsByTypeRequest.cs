namespace Catalog.Host.Models.Requests
{
    public class PaginatedItemsByTypeRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TypeIdRequest { get; set; }
    }
}
