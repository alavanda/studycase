using Catalog.Domain.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Persistence
{
    public interface ICatalogContext
    {
        MongoClient MongoClient { get; }
        IMongoCollection<Category> Categories { get; }
        IMongoCollection<Product> Products { get; }
    }
}
