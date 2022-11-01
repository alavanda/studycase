using Catalog.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;

namespace Catalog.Infrastructure.Mapping
{
    public static class EntitiyMapping
    {
        public static void Map()
        {
            var decimalSerializer = new DecimalSerializer(BsonType.Decimal128, new
                RepresentationConverter(allowOverflow: false, allowTruncation:
                false));

            BsonSerializer.RegisterSerializer(decimalSerializer);

            BsonClassMap.RegisterClassMap<Category>(x =>
            {
                x.MapIdMember(x => x.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
                x.MapMember(x => x.Name).SetElementName("name").SetOrder(1);
                x.MapMember(x => x.Description).SetElementName("description").SetOrder(2);
            });

            BsonClassMap.RegisterClassMap<Product>(x =>
            {
                x.MapIdMember(p => p.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
                x.MapMember(p => p.Name).SetElementName("name").SetOrder(1);
                x.MapMember(p => p.Description).SetElementName("description").SetOrder(2);
                x.MapMember(p => p.CategoryId).SetElementName("categoryId").SetOrder(3);
                x.MapMember(p => p.Price).SetElementName("price").SetOrder(4);
                x.MapMember(p => p.Currency).SetElementName("currency").SetOrder(5);
            });
        }
    }
}
