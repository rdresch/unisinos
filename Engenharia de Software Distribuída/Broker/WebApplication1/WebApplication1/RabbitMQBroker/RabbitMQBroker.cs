using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;

namespace WebApplication1.RabbitMQBroker
{
    public class RabbitMQBroker
    {
        private const string EXCHANGE_NAME = "exchange_name";
        private const string QUEUE_NAME = "queue_name";
        private const string ROUTING_KEY = "routing_key";
        private static List<string> messages = new List<string>();

        public static List<string> ReadMessages()
        {
            return messages;
        }

        public static void SendMessage(string message)
        {
            using (var connection = CreateConnection())
            {
                using (var model = connection.CreateModel())
                {
                    byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(message);
                    IBasicProperties props = model.CreateBasicProperties();
                    props.ContentType = "text/plain";
                    props.DeliveryMode = 2;
                    model.BasicPublish(EXCHANGE_NAME,
                                       ROUTING_KEY, props,
                                       messageBodyBytes);
                }
            }
        }

        public static void StartConsumer()
        {
            using (var connection = CreateConnection())
            {
                using (var model = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(model);
                    consumer.Received += (ch, ea) =>
                    {
                        var body = ea.Body;
                        // ... process the message
                        model.BasicAck(ea.DeliveryTag, false);
                    };
                    messages.Add(model.BasicConsume(QUEUE_NAME, false, consumer));
                }
            }
        }

        private static IModel CreateModel(IConnection connection)
        {
            var model = connection.CreateModel();
            model.ExchangeDeclare(EXCHANGE_NAME, ExchangeType.Direct);
            model.QueueDeclare(QUEUE_NAME, true, false, false, null);
            model.QueueBind(QUEUE_NAME, EXCHANGE_NAME, ROUTING_KEY, null);

            return connection.CreateModel();
        }

        private static IConnection CreateConnection()
        {
            var factory = new ConnectionFactory
            {
                UserName = "username",
                Password = "password",
                VirtualHost = "/",
                HostName = "localhost"
            };

            return factory.CreateConnection();
        }
    }
}