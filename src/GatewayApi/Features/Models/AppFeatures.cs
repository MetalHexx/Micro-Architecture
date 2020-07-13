using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GatewayApi.Features.Models
{
    public class AppFeatures
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Feature CustomerOrdersView { get; set; }
        public Feature CustomerOrderDetails { get; set; }
    }
}
