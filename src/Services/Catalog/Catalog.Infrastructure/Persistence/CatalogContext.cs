using Catalog.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Persistence
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            MongoClient = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);

            var database = MongoClient.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

            Categories = database.GetCollection<Category>(configuration["DatabaseSettings:CategoryCollectionName"]);
            Products = database.GetCollection<Product>(configuration["DatabaseSettings:ProductCollectionName"]);
        }

        public IMongoCollection<Category> Categories { get; }
        public IMongoCollection<Product> Products { get; }
        public MongoClient MongoClient { get; }
    }
}
