using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WebApplication1.RabbitMQBroker
{
    public class RabbitMQBroker
    {
        private const string EXCHANGE_NAME = "exchange_name";
        private const string QUEUE_NAME = "queue_name";
        private const string ROUTING_KEY = "routing_key";
        private const byte DELIVERY_MODE_PERSISTENT = 2;
        private static List<string> receivedMessages = new List<string>();
        
        public static List<string> ReadMessages()
        {
            return receivedMessages;
        }

        public static void SendMessage(string message)
        {
            using (var connection = CreateConnection())
            using (var channel = CreateModel(connection))
            {   
                channel.BasicPublish(
                    exchange: EXCHANGE_NAME,
                    routingKey: ROUTING_KEY,
                    basicProperties: null,
                    body: Encoding.UTF8.GetBytes(message));

                Debug.WriteLine($"Message sent: {message}");
            }
        }
        
        public static void StartConsumer()
        {
            var connection = CreateConnection();
            var channel = CreateModel(connection);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                receivedMessages.Add(message);

                Debug.WriteLine($"Message received: {message}");
            };

            channel.BasicConsume(
                queue: QUEUE_NAME,
                autoAck: true,
                consumer: consumer);
        }
        
        private static IConnection CreateConnection()
        {
            var factory = new ConnectionFactory
            {
                VirtualHost = "/",
                HostName = "localhost"
            };

            return factory.CreateConnection();
        }

        private static IModel CreateModel(IConnection connection)
        {
            var model = connection.CreateModel();
            model.ExchangeDeclare(EXCHANGE_NAME, ExchangeType.Direct, true, true, null);
            model.QueueDeclare(QUEUE_NAME, true, false, true, null);
            model.QueueBind(QUEUE_NAME, EXCHANGE_NAME, ROUTING_KEY, null);

            return model;
        }
    }
}