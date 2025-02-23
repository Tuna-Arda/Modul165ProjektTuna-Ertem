using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JetstreamBackend.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("kundenname")]
        public string Kundenname { get; set; }
        
        [BsonElement("email")]
        public string Email { get; set; }
        
        [BsonElement("telefon")]
        public string Telefon { get; set; }
        
        [BsonElement("prioritaet")]
        public string Prioritaet { get; set; }
        
        [BsonElement("dienstleistung")]
        public string Dienstleistung { get; set; }
        
        [BsonElement("abholzeit")]
        public string Abholzeit { get; set; }
        
        [BsonElement("status")]
        public string Status { get; set; }
    }
}
