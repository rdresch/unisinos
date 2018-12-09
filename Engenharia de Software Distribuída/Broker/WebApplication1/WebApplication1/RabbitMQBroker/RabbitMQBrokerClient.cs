using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WebApplication1.RabbitMQBroker
{
    public class RabbitMQBrokerClient
    {
        private const string CLIENT_EXCHANGE_NAME = "client_exchange";
        private const string SERVER_EXCHANGE_NAME = "server_exchange";
        private const string CLIENT_QUEUE_NAME = "client_queue";
        private const string SERVER_QUEUE_NAME = "server_queue";
        private const string ROUTING_KEY = "routing_key";
        private const byte DELIVERY_MODE_PERSISTENT = 2;
        private const int THREAD_SLEEP_TIME = 3000;
        private static List<string> messages = new List<string>();

        public static List<string> ReadMessages()
        {
            return messages;
        }
        
        public static void SendMessageToServer(string message)
        {
            using (var connection = CreateConnection())
            using (var channel = CreateChannel(connection, SERVER_QUEUE_NAME))
            {   
                channel.BasicPublish(
                    exchange: SERVER_EXCHANGE_NAME,
                    routingKey: ROUTING_KEY,
                    basicProperties: null,
                    body: Encoding.UTF8.GetBytes(message));

                AddMessage($"Message sent to server: {message}");
            }
        }
        
        public static void StartConsumer()
        {
            var connection = CreateConnection();
            var channel = CreateChannel(connection, CLIENT_QUEUE_NAME);
            channel.QueueBind(CLIENT_QUEUE_NAME, CLIENT_EXCHANGE_NAME, ROUTING_KEY, null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                Thread.Sleep(THREAD_SLEEP_TIME);

                AddMessage($"Message received from server: {message}");
                AddMessage($"Processing message {message}");
                AddMessage($"Message {message} processed");                
            };

            channel.BasicConsume(
                queue: CLIENT_QUEUE_NAME,
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

        private static IModel CreateChannel(IConnection connection, string queueName)
        {
            var channel = connection.CreateModel();
            channel.ExchangeDeclare(CLIENT_EXCHANGE_NAME, ExchangeType.Direct, true, true, null);
            channel.QueueDeclare(queueName, true, false, true, null);

            return channel;
        }

        private static void AddMessage(string message)
        {
            messages.Add($"{DateTime.Now.ToLocalTime()} - {message}");
            Thread.Sleep(THREAD_SLEEP_TIME);
        }
    }
}