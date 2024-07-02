using Azure.Core;

using Dapper;

using Microsoft.Data.SqlClient;

using Newtonsoft.Json;
using RabbitMQ.Client;
using Ramand_Application.Model;
using Ramand_Application.ServiceContract.Persistence;
using Ramand_Domain.Models;

using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Ramand_Persistence.Repositories
{
    public class RabbitMqRepository : BaseConnectionString ,IRabbitMqService
    {
        public User GetUserById(int userId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var query = "SELECT * FROM [User] Where Id = @Id";
                var user = connection.QueryFirstOrDefault<User>(query, new { Id = userId });
                return user;
            }
        }

        public void SendUser(User user)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "userQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var json = JsonConvert.SerializeObject(user);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: "userQueue", basicProperties: null, body: body);
            }
        }
    }
}
