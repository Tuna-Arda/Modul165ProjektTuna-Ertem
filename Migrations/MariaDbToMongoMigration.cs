using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MongoDB.Driver;
using JetstreamBackend.Models;

namespace JetstreamBackend.Migrations
{
    public class MariaDbToMongoMigration
    {
        public static void Migrate(string mariaDbConnectionString, string mongoConnectionString, string mongoDatabaseName)
        {
            var orders = new List<Order>();
            
            // Daten aus MariaDB auslesen
            using (var mariaConnection = new MySqlConnection(mariaDbConnectionString))
            {
                mariaConnection.Open();
                var query = "SELECT kundenname, email, telefon, prioritaet, dienstleistung, abholzeit, status FROM service_orders";
                using (var command = new MySqlCommand(query, mariaConnection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            Kundenname = reader["kundenname"].ToString(),
                            Email = reader["email"].ToString(),
                            Telefon = reader["telefon"].ToString(),
                            Prioritaet = reader["prioritaet"].ToString(),
                            Dienstleistung = reader["dienstleistung"].ToString(),
                            Abholzeit = reader["abholzeit"].ToString(),
                            Status = reader["status"].ToString()
                        });
                    }
                }
            }
            
            // Daten in MongoDB einfügen
            var mongoClient = new MongoClient(mongoConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseName);
            var ordersCollection = mongoDatabase.GetCollection<Order>("Orders");
            
            if (orders.Count > 0)
            {
                ordersCollection.InsertMany(orders);
                Console.WriteLine($"Migrated {orders.Count} orders from MariaDB to MongoDB.");
            }
            else
            {
                Console.WriteLine("No orders found in MariaDB.");
            }
        }
    }
}
