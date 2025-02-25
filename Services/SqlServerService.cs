using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using JetstreamBackend.Models;


namespace JetstreamBackend.Services
{
    public class SqlServerService
    {
        private readonly string _connectionString;
        
        public SqlServerService(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public List<Order> GetOrders()
        {
            var orders = new List<Order>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT id, kundenname, email, telefon, prioritaet, dienstleistung, abholzeit, status FROM service_orders";
                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            Id = reader["id"].ToString(),
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
            return orders;
        }
    }
}
