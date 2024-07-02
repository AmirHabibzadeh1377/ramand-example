using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;


const string UserQueueName = "userQueue";
const string ProcessedQueueName = "processed_queue";
const int TtlMilliseconds = 10000;
var factory = new ConnectionFactory()
{
    HostName = "localhost", 
    UserName = "guest",     
    Password = "guest"      
};

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: UserQueueName,
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    channel.QueueDeclare(queue: ProcessedQueueName,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: new Dictionary<string, object>
                                {
                                     {"x-message-ttl", TtlMilliseconds}
                                });

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine("Received message: {0}", message);

        channel.BasicPublish(exchange: "",
                             routingKey: ProcessedQueueName,
                             basicProperties: null,
                             body: body);

        Console.WriteLine("message forwarded to processed_queue with TTL.");

        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
    };

    channel.BasicConsume(queue: UserQueueName,
                         autoAck: false,
                         consumer: consumer);

    Console.WriteLine("please key enter to exit.");
    Console.ReadLine();
}