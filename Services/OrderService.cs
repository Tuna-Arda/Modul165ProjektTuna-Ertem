using JetstreamBackend.Data;
using JetstreamBackend.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace JetstreamBackend.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _orders;
        
        public OrderService(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _orders = database.GetCollection<Order>("Orders");
            
            // Beispiel: Erstelle einen Index auf den Kundenname für schnellere Suchabfragen
            var indexKeys = Builders<Order>.IndexKeys.Ascending(o => o.Kundenname);
            _orders.Indexes.CreateOne(new CreateIndexModel<Order>(indexKeys));
        }
        
        public List<Order> Get() =>
            _orders.Find(o => true).ToList();
        
        public Order Get(string id) =>
            _orders.Find(o => o.Id == id).FirstOrDefault();
        
        public Order Create(Order order)
        {
            _orders.InsertOne(order);
            return order;
        }
        
        public void Update(string id, Order orderIn) =>
            _orders.ReplaceOne(o => o.Id == id, orderIn);
        
        public void Remove(string id) =>
            _orders.DeleteOne(o => o.Id == id);
    }
}
