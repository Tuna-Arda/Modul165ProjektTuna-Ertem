using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JetstreamBackend.Models;

namespace JetstreamBackend.Tests
{
    class TestApi
    {
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000");

            // Login
            var loginResult = await client.PostAsJsonAsync("/api/Auth/login", new User { Username = "admin", Password = "admin123" });
            var loginResponse = await loginResult.Content.ReadFromJsonAsync<dynamic>();
            string token = loginResponse.token;
            Console.WriteLine("Token: " + token);

            client.DefaultRequestHeaders.Add("x-access-token", token);

            // GET Orders
            var orders = await client.GetFromJsonAsync<Order[]>("/api/Orders");
            Console.WriteLine("Orders count: " + orders.Length);
        }
    }
}
