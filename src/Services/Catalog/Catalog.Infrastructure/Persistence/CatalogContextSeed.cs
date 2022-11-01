using Catalog.Domain.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.Infrastructure.Persistence
{
    public static class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Category> categoryCollection, IMongoCollection<Product> productCollection)
        {
            bool existCategory = categoryCollection.Find(p => true).Any();

            if (!existCategory)
            {
                categoryCollection.InsertManyAsync(CreateCategories());
            }

            bool existProduct = categoryCollection.Find(p => true).Any();

            if (!existProduct)
            {
                productCollection.InsertManyAsync(CreateProducts());
            }
        }

        public static IEnumerable<Category> CreateCategories()
        {
            return new List<Category>()
            {
                new Category
                {
                    Id = "61880b38e053f45b3d978e68",
                    Name = "Türk Mutfağı",
                    Description = "Türk mutfağına ait lezzetler"
                }
            };
        }

        public static IEnumerable<Product> CreateProducts()
        {
            return new List<Product>()
            {
                new Product
                {
                    Id = "618884b1e053f45b3d978e70",
                    Name = "Döner",
                    Description = "1 Porsiyon yaprak döner",
                    CategoryId = "61880b38e053f45b3d978e68",
                    Price = 25.90M,
                    Currency = "TL"
                }
            };
        }
    }

}
