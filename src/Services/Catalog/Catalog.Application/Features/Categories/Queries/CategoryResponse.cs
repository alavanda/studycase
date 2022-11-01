namespace Catalog.Application.Features.Categories.Queries
{
    public record CategoryResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
