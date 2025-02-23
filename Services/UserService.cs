using JetstreamBackend.Data;
using JetstreamBackend.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace JetstreamBackend.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        
        public UserService(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>("Users");
            
            // Falls noch keine Benutzer vorhanden, füge Standardnutzer ein
            if (_users.Find(u => true).CountDocuments() == 0)
            {
                _users.InsertMany(new List<User>
                {
                    new User { Username = "admin", Password = "admin123", Role = "Admin" },
                    new User { Username = "user", Password = "user123", Role = "User" }
                });
            }
        }
        
        public User GetByUsername(string username) =>
            _users.Find(u => u.Username == username).FirstOrDefault();
    }
}
