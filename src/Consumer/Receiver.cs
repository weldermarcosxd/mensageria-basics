﻿using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer
{
    public class Receiver
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory{HostName = "localhost"};
            using var conncetion = factory.CreateConnection();
            using var channel = conncetion.CreateModel();
            channel.QueueDeclare("BasicTest", false, false, false, null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.Span;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received message {message}");
            };
            
            channel.BasicConsume("BasicTest", true, consumer);
            Console.WriteLine("Press [enter] to exit the consumer app...");
        }
    }
}