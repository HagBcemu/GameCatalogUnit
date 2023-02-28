namespace Catalog.Host.Models.Requests
{
    public class UpdateEntityNameRequest
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
